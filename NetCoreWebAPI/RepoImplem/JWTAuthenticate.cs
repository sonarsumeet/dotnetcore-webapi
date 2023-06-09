﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NetCoreWebAPI.Models;
using NetCoreWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebAPI.RepoImplem
{
    public class JWTAuthenticate : IJWTAuthenticate
    {
        private readonly IConfiguration _configuration;

        public JWTAuthenticate(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public JWTAuthToken Token()
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWTKeys:Key"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, "LEARNCORE")
                    }),
                    Expires = DateTime.Now.AddMinutes(2),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature
                    )
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new JWTAuthToken { Token = tokenHandler.WriteToken(token) };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
