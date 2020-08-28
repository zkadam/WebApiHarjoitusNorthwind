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
    public class OrdersController : ControllerBase
    {
        northwindContext db = new northwindContext();

        //-------------------get all northwind orders
        [HttpGet]
        [Route("")]
        public List<Orders> GetAllOrders()
        {
            northwindContext db = new northwindContext();
            List<Orders> orders = db.Orders.ToList();
            db.Dispose();
            return orders;
        }

        //-------------------get northwind orduct by id
        [HttpGet]
        [Route("{id}")]
        public ActionResult GetOneOrder(int id)
        {

            Orders tilaus = db.Orders.Find(id);
            if (tilaus != null)
            {
                db.Dispose();
                return Ok(tilaus);
            }
            else
            {
                db.Dispose();
                return NotFound("Order id:llä: " + id + " ei löyty");
            }
        }

        //-------------------get northwind order by ship city
        [HttpGet]
        [Route("Shipcity/{id}")]
        public ActionResult GetOrdersByShipCity(string id)
        {
            var ordList = from p in db.Orders
                           where p.ShipCity == id
                           select p;
            if (ordList.Count() != 0)
            {
                //db.Dispose();
                return Ok(ordList.ToList());
            }
            else
            {
                db.Dispose();
                return NotFound("Kaupungissa : " + id + " olevielle laivalla ei löyty tilauksia");
            }
        }

        //-------------------------POST new Order

        [HttpPost]
        [Route("")]
        public ActionResult PostNewOrder([FromBody] Orders tilaus)
        {

            try
            {
                db.Orders.Add(tilaus);
                db.SaveChanges();
                return Ok("Uusi tilaus lisätty id:llä: " + tilaus.OrderId);
            }
            catch (Exception)
            {

                return BadRequest("Tilauksen lisäystä epäonnistui.");
            }
            finally
            {
                db.Dispose();

            }
        }


        //------------------------------PUT update existing orduct
        [HttpPut]
        [Route("{key}")]
        public ActionResult PutOrdEdit(int key, [FromBody] Orders tilaus)
        {

            try
            {
                Orders ord = db.Orders.Find(key);
                if (ord != null)
                {
                    ord.ShipCity = tilaus.ShipCity;
                    ord.ShipRegion = tilaus.ShipRegion;
                    ord.ShipCountry = tilaus.ShipCountry;
                    ord.CustomerId = tilaus.CustomerId;
                    ord.RequiredDate = tilaus.RequiredDate;
                    ord.ShippedDate = tilaus.ShippedDate;
                   
                    db.SaveChanges();

                    return Ok(ord.OrderId + " tilauksen tiedot ovat päivitetty");
                }
                else
                {
                    return NotFound("Päivitettävää tilausta ei löytynyt");
                }
            }
            catch (Exception)
            {
                return BadRequest("Tilauksen tietojen päivitys ei onnistunut");
                throw;
            }
            finally
            {
                db.Dispose();
            }
        }

        //---------------------------------DELETE orduct poisto
        [HttpDelete]
        [Route("{key}")]
        public ActionResult DeleteOrder(int key)
        {
            Orders tilaus = db.Orders.Find(key);
            if (tilaus != null)
            {
                db.Orders.Remove(tilaus);
                db.SaveChanges();
                db.Dispose();
                return Ok("Tilaus " + key + " poistettiin.");
            }
            else
            {
                db.Dispose();
                return NotFound("Tilaus " + key + " ei löydy");
            }

        }

    }
}
