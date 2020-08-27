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
    public class CustomerController : ControllerBase
    {
        //-------------------get all northwind customers
        [HttpGet]
        [Route("")]
        public List<Customers> GetAllCustomers()
        {
            northwindContext db = new northwindContext();
            List<Customers> asiakkaat = db.Customers.ToList();
            return asiakkaat;
        }
        
        //-------------------get northwind customer by id
        [HttpGet]
        [Route("{id}")]
        public Customers GetOneCustomer(string id)
        {
            northwindContext db = new northwindContext();
            Customers asiakas = db.Customers.Find(id);
           
            return asiakas;
        }

        //--------------------GET northwind customers of one country

        [HttpGet]
        [Route("country/{key}")]
        public List<Customers> GetCountryCustomers(string key)
        {
            northwindContext db = new northwindContext();

            var someCustomers = from c in db.Customers
                                where c.Country == key
                                select c;
            return someCustomers.ToList();

        }

        //-------------------------POST

        [HttpPost]
        [Route("")]
        public ActionResult PostCreateNew([FromBody] Customers asiakas)
        {

                northwindContext db = new northwindContext();
            try
            {
                db.Customers.Add(asiakas);
                db.SaveChanges();
                return Ok(asiakas.CustomerId);
            }
            catch (Exception)
            {

                return BadRequest("Jokin meni pieleen asiakasta lisäättäessä");
            }
            finally
            {
                db.Dispose();

            }



        }


    }
}
