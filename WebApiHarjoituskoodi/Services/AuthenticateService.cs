using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiHarjoituskoodi.ViewModels;
using WebApiHarjoituskoodi.Models;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace WebApiHarjoituskoodi.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly AppSettings _appSettings;
        public AuthenticateService(IOptions<AppSettings>appSettings)
        {
            _appSettings = appSettings.Value;
        }

        private northwindContext context = new northwindContext();
        public LoginsWithToken Authenticate(string UserName, string PassWord)
        {
            var user = context.Logins.SingleOrDefault(x => x.UserName == UserName && x.PassWord == PassWord);
            
            //jos ei ole käyttäjä palautetaan, null
            if (user==null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.LoginId.ToString()),
                    new Claim(ClaimTypes.Role,"Admin"),
                    new Claim(ClaimTypes.Version,"V3.1")
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //viewmodelin kautta lisätty token
            var userWithToken = new LoginsWithToken() {
                UserName = user.UserName,
                Firstname=user.Firstname,
                Lastname=user.Lastname,
                Email=user.Lastname,
                PassWord=null,
                AccesslevelId=user.AccesslevelId,
                Token= tokenHandler.WriteToken(token)
               };

            return userWithToken;
        }
    }
}
