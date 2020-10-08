using AuthService.Domain.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AuthService.Domain.Entities
{
    public class Token
    {
        public string AccessToken { get; private set;  }

        public string TokenType { get; } = "Bearer";

        public double ExpiresIn { get; private set; }

        public Token(string accessToken, double expiresIn)
        {
            AccessToken = accessToken;
            ExpiresIn = expiresIn;
        }

        public static Token GenerateToken(User user, IAccessTokenSettings accessTokenSettings)
        {
            List<System.Security.Claims.Claim> claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(ClaimTypes.NameIdentifier, user.Id),
                new System.Security.Claims.Claim(ClaimTypes.Name, user.FullName)
            };

            if (user.Claims != null && user.Claims.Count > 0)
            {
                user.Claims.ForEach((claim) =>
                {
                    claims.Add(new System.Security.Claims.Claim(claim.Type, claim.Value));
                });
            }

            using RSA rsa = RSA.Create();
            rsa.ImportRSAPrivateKey( // Convert the loaded key from base64 to bytes.
                source: Convert.FromBase64String(accessTokenSettings.PrivateKey), // Use the private key to sign tokens
                bytesRead: out int _); // Discard the out variable 

            var signingCredentials = new SigningCredentials(
                key: new RsaSecurityKey(rsa),
                algorithm: SecurityAlgorithms.RsaSha256 // Important to use RSA version of the SHA algo 
            );

            var jwt = new JwtSecurityToken(
                issuer: accessTokenSettings.Issuer,
                audience: accessTokenSettings.Audience,
                claims: claims,
                notBefore: accessTokenSettings.NotBefore,
                expires: accessTokenSettings.Expires,
                signingCredentials: signingCredentials
            );

            return new Token(
                accessToken: new JwtSecurityTokenHandler().WriteToken(jwt),
                expiresIn: accessTokenSettings.ExpiresIn);
        }
    }
}
