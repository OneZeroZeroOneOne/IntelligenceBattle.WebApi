using IntelligenceBattle.WebApi.Dal.Contexts;
using IntelligenceBattle.WebApi.Security.Abstractions;
using IntelligenceBattle.WebApi.Security.Configs;
using IntelligenceBattle.WebApi.Security.Services;
using IntelligenceBattle.WebApi.Utilities.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IntelligenceBattle.WebApi.Security.ProviderResolvers
{
    public class UIProviderResolver : IProviderResolver    
    {
        private JwtTokenService _jwtTokenService;
        private string token;
        private AuthOptions authOptions;
        public UIProviderResolver(string t, JwtTokenService jwtTokenService, AuthOptions au)
        {
            token = t;
            authOptions = au;
        }

        public List<Claim> GetClaims()
        {
            if (string.IsNullOrEmpty(authOptions.Key) || string.IsNullOrEmpty(token))
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.SecurityKeyIsNull, "Invalid security key");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKeyBytes = Encoding.ASCII.GetBytes(authOptions.Key);

            SecurityToken SignatureValidator(string encodedToken, TokenValidationParameters parameters)
            {
                var jwt = new JwtSecurityToken(encodedToken);

                var hmac = new HMACSHA256(securityKeyBytes);

                var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(hmac.Key), SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);

                var signKey = signingCredentials.Key as SymmetricSecurityKey;

                var encodedData = jwt.EncodedHeader + "." + jwt.EncodedPayload;
                var compiledSignature = Encode(encodedData, signKey.Key);

                if (compiledSignature != jwt.RawSignature)
                {
                    throw new Exception("Token signature validation failed.");
                }
                return jwt;
            }

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(securityKeyBytes),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireSignedTokens = false, //погугли
                ClockSkew = TimeSpan.Zero,
                SignatureValidator = SignatureValidator,
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            return jwtToken.Claims.ToList();

        }
        private static string Encode(string input, byte[] key)
        {
            HMACSHA256 sha = new HMACSHA256(key);
            byte[] byteArray = Encoding.UTF8.GetBytes(input);
            MemoryStream stream = new MemoryStream(byteArray);
            byte[] hashValue = sha.ComputeHash(stream);
            return Base64UrlEncoder.Encode(hashValue);
        }
    }
}
