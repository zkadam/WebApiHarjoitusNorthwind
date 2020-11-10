using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiHarjoituskoodi.Models;
using System.Text;

namespace WebApiHarjoituskoodi.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly AppSettings _appSettings;
        public AuthenticateService(IOptions<AppSettings>appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public Logins Authenticate(string UserName, string PassWord)
        {
            throw new NotImplementedException();
        }
    }
}
