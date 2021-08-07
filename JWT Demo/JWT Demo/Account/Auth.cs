using JWT_Demo.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWT_Demo.Account
{
    public class Auth : IJwtAuth
    {
        private readonly string username = "akshay";
        private readonly string password = "DemoPassword";
        private readonly string key, issuer;
        public Auth(string key, string issuer)
        {
            this.key = key;
            this.issuer = issuer;
        }
        public string Authentication(string username, string password)
        {
            if (!(username.Equals(this.username) || password.Equals(this.password)))
            {
                return null;
            }

            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypted
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            //3. Create JETdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = issuer,
                Audience = issuer,
                SigningCredentials =new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token from method
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
