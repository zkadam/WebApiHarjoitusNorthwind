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
    public class ProductsController : ControllerBase
    {
        northwindContext db = new northwindContext();

        //-------------------get all northwind products
        [HttpGet]
        [Route("")]
        public List<Products> GetAllProducts()
        {
            northwindContext db = new northwindContext();
            List<Products> products = db.Products.ToList();
            db.Dispose();
            return products;
        }

        //-------------------get northwind product by id
        [HttpGet]
        [Route("{id}")]
        public ActionResult GetOneProduct(int id)
        {

            Products tuote = db.Products.Find(id);
            if (tuote != null)
            {
                return Ok(tuote);
            }
            else
            {
                return NotFound("Product id:llä: " + id + " ei löyty");
            }
        }
        
        //-------------------get northwind product by categoryid
        [HttpGet]
        [Route("Category/{id}")]
        public ActionResult GetProductsByCat(int id)
        {
            //List<Products> productList = new List<Products>();
            //productList.Add
            //Products tuote = db.Products.Find(id);
            var prodList = from p in db.Products
                                where p.CategoryId == id
                                select p;
            if (prodList.Count()!=0)
            {
                return Ok(prodList.ToList());
            }
            else
            {
                return NotFound("Tuotteet category id:llä: " + id + " ei löyty");
            }
        }

    }
}
