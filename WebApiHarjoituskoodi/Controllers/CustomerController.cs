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

        //-------------------------POST new customer

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
//------------------------------PUT update existing customer
        [HttpPut]
        [Route("{key}")]
        public  ActionResult PutEdit(string key, [FromBody] Customers asiakas)
        {
            northwindContext db = new northwindContext();
            try
            {
                Customers customer = db.Customers.Find(key);
                if (customer !=null)
                {
                    customer.CompanyName = asiakas.CompanyName;
                    customer.ContactName = asiakas.ContactName;
                    customer.ContactTitle = asiakas.ContactTitle;
                    customer.Country = customer.Country;
                    customer.City = asiakas.City;
                    customer.City = asiakas.City;
                    customer.PostalCode = asiakas.PostalCode;
                    customer.Phone = asiakas.Phone;

                    db.SaveChanges();
                    return Ok(customer.CustomerId + " asiakkaan tiedot ovat päivitetty");
                }
                else
                {
                    return NotFound("Päivitettävää asiakasta ei löytynyt");
                }
            }
            catch (Exception)
            {
                return BadRequest("Asiakkaan tietojen päivitys ei onnistunut");
                throw;
            }
            finally
            {
                db.Dispose();
            }
        }
//---------------------------------DELETE customers poisto
        [HttpDelete]
        [Route("{key}")]
        public  ActionResult DeleteCustomer(string key)
        {
            northwindContext db = new northwindContext();
            Customers asiakas = db.Customers.Find(key);
            if (asiakas != null)
            {
                db.Customers.Remove(asiakas);
                db.SaveChanges();
                db.Dispose();
                return Ok("Asiakas " + key + " poistettiin.");
            }
            else
            {
                db.Dispose();
                return NotFound("Asiakasta " + key + " ei löydy");
            }
            
        }
    }
}
