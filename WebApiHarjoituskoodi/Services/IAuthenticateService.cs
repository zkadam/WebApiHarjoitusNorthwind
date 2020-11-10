using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiHarjoituskoodi.Models;

namespace WebApiHarjoituskoodi.Services
{
  public  interface IAuthenticateService
    {
        Logins Authenticate(string UserName, string PassWord);
    }
}
