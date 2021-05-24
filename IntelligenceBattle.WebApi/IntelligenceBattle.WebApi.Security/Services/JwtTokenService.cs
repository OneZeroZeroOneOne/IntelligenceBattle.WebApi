using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IntelligenceBattle.WebApi.Security.Configs;
using Microsoft.IdentityModel.Tokens;
using IntelligenceBattle.WebApi.Utilities.Exceptions;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace IntelligenceBattle.WebApi.Security.Services
{
    public class JwtTokenService
    {
        public AuthOptions _authOptions;
        public JwtTokenService(AuthOptions authOptions)
        {
            _authOptions = authOptions;
        }
        public ClaimsIdentity GetIdentity(int userId)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", Convert.ToString(userId)),
            };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims);
            return claimsIdentity;
        }

        public string GenerateToken(ClaimsIdentity claims)
        {
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: _authOptions.Issuer,
                audience: _authOptions.Audience,
                notBefore: now,
                claims: claims.Claims,
                expires: now.Add(TimeSpan.FromMinutes(_authOptions.Lifetime)),
                signingCredentials: new SigningCredentials(_authOptions.SecurityKey, SecurityAlgorithms.HmacSha256));
            return "Bearer " + new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public JwtSecurityToken ParseToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKeyBytes = Encoding.ASCII.GetBytes(_authOptions.Key);

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
                RequireSignedTokens = false,
                ClockSkew = TimeSpan.Zero,
                SignatureValidator = SignatureValidator,
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            return jwtToken;
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
