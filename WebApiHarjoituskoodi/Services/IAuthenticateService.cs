using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiHarjoituskoodi.ViewModels;

namespace WebApiHarjoituskoodi.Services
{
  public  interface IAuthenticateService
    {
        LoginsWithToken Authenticate(string UserName, string PassWord);
    }
}
