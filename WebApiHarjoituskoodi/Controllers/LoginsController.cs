using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiHarjoituskoodi.Models;

namespace WebApiHarjoituskoodi.Controllers
{
    [Route("nw/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        northwindContext db = new northwindContext();
        //-------------------get all northwind employees
        [HttpGet]
        [Route("")]
        public List<Logins> GetAllLogins()
        {
            List<Logins> logins = db.Logins.ToList();
            foreach (var login in logins)
            {
                login.PassWord = "*****";
            }
            db.Dispose();
            return logins;
        }
    }
}
