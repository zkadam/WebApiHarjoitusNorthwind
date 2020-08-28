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
    public class OrderDetailsController : ControllerBase
    {
        northwindContext db = new northwindContext();

        //-------------------get all northwind orderDetails
        [HttpGet]
        [Route("")]
        public List<OrderDetails> GetAllOrderDetails()
        {
           List<OrderDetails> orderDetails = db.OrderDetails.ToList();
            db.Dispose();
            return orderDetails;
        }

        //-------------------get northwind orderDetail by id
        [HttpGet("{orderid}/{tuoteid}")]
        [Route("{orderid}/{tuoteid}")]
        public ActionResult GetOneOrderDetail(int orderid, int tuoteid)
        {

            var tilausDetail = from p in db.OrderDetails
                               where p.OrderId == orderid
                               where p.ProductId==tuoteid
                               select p;
            if (tilausDetail != null)
            {
                //db.Dispose();
                return Ok(tilausDetail);
            }
            else
            {
                db.Dispose();
                return NotFound("Order Detail id:llä: "+ orderid + " / " + tuoteid + " ei löyty");
            }
        }

        //-------------------get northwind orderDetail by categoryid
        [HttpGet]
        [Route("{id}")]
        public ActionResult GetOrderDetailsByOrdId(int id)
        {
            var ordList = from p in db.OrderDetails
                           where p.OrderId == id
                           select p;
            if (ordList.Count() != 0)
            {
                //db.Dispose();
                return Ok(ordList.ToList());
            }
            else
            {
                db.Dispose();
                return NotFound("Tilaus order id:llä: " + id + " ei löyty");
            }
        }

        //-------------------------POST new OrderDetail

        [HttpPost]
        [Route("")]
        public ActionResult PostNewOrderDetail([FromBody] OrderDetails tilausDetail)
        {

            try
            {
                db.OrderDetails.Add(tilausDetail);
                db.SaveChanges();
                return Ok("Uusi order detail lisätty id:llä: " + tilausDetail.OrderId + ", tilatun tuoten id on:" +tilausDetail.ProductId);
            }
            catch (Exception)
            {

                return BadRequest("Tuoten lisäystä epäonnistui.");
            }
            finally
            {
                db.Dispose();

            }
        }


        //------------------------------PUT update existing orderDetail
        [HttpPut]
        [Route("{ordid}/{tuoteid}")]
        public ActionResult PutOrdDetailEdit(int ordid, int tuoteid, [FromBody] OrderDetails tilausDetail)
        {

            try
            {
                OrderDetails ord = db.OrderDetails.Find(ordid, tuoteid);
                if (ord != null)
                {
                    ord.OrderId = tilausDetail.OrderId;
                    ord.ProductId = tilausDetail.ProductId;
                    ord.UnitPrice = tilausDetail.UnitPrice;
                    ord.Quantity = tilausDetail.Quantity;
                    ord.Discount = tilausDetail.Discount;
                    db.SaveChanges();

                    return Ok(ord.OrderId + " tilausDetailin tiedot ovat päivitetty");
                }
                else
                {
                    return NotFound("Päivitettävää tilausDetail ei löytynyt");
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

        //---------------------------------DELETE orderDetail poisto
        [HttpDelete]
        [Route("{OrderId}/{id}")]
        public ActionResult DeleteOrderDetail(int key)
        {
            OrderDetails tilausDetail = db.OrderDetails.Find(key);
            if (tilausDetail != null)
            {
                db.OrderDetails.Remove(tilausDetail);
                db.SaveChanges();
                db.Dispose();
                return Ok("Tuote " + key + " poistettiin.");
            }
            else
            {
                db.Dispose();
                return NotFound("Tuote " + key + " ei löydy");
            }

        }

    }
}
