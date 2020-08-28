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

        [HttpGet]
        [Route("")]
        public List<Products> GetAllProducts()
        {
            northwindContext db = new northwindContext();
            List<Products> products = db.Products.ToList();
            db.Dispose();
            return products;
        }
    }
}
