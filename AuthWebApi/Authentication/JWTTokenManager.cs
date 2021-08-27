using AuthWebApi.Authentication.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AuthWebApi.Authentication
{
    public class JWTTokenManager : IJWTTokenManager
    {
        private readonly string _key;
        private readonly Dictionary<string, string> credentialHub = new()
        {
            {"Pradeep","Pradeeptce@01" }
        };

        public JWTTokenManager(string key)
        {
            _key = key;
        }


        public string GetToken(string userName, string passWord)
        {
            if(!credentialHub.Any(cred => cred.Key == userName && cred.Value == passWord))
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.Name,userName) }
                 ),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)             
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    };
};

