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
        public JwtTokenService jwtTokenService;
        public string token;
        public UIProviderResolver(string t, JwtTokenService jTService)
        {
            jwtTokenService = jTService;
            token = t;
        }

        public async Task<List<Claim>> GetClaims()
        {
            var jwtToken = jwtTokenService.ParseToken(token);
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
