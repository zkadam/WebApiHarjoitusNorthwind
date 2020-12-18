using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiHarjoituskoodi.Models;

namespace WebApiHarjoituskoodi.Controllers
{
    [Route("nw/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        northwindContext db = new northwindContext();

        //-------------------get all northwind Categories
        [HttpGet]
        [Route("")]
        public List<Suppliers> GetAllCategories()
        {
            northwindContext db = new northwindContext();
            List<Suppliers> suppliers = db.Suppliers.ToList();
            db.Dispose();
            return suppliers;
        }
    }
}
