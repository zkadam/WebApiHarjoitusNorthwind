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

        //-------------------get northwind product by id
        [HttpGet]
        [Route("{surname}")]
        public ActionResult GetLoginsBySurname(string surname)
        {

            List<Logins> logins = db.Logins.Where(l => l.Lastname.Contains(surname)).ToList();
            if (logins != null)
            {
                foreach (var login in logins)
                {
                    login.PassWord = "*****";
                }
                db.Dispose();
                return Ok(logins);
            }
            else
            {
                db.Dispose();
                return NotFound("Haettu sukunimi: " + surname + " ei löyty");
            }
        }
    }
}
