﻿using System;
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
                db.Dispose();
                return Ok(tuote);
            }
            else
            {
                db.Dispose();
                return NotFound("Product id:llä: " + id + " ei löyty");
            }
        }
        
//-------------------get northwind product by categoryid
        [HttpGet]
        [Route("Category/{id}")]
        public ActionResult GetProductsByCat(int id)
        {
            var prodList = from p in db.Products
                                where p.CategoryId == id
                                select p;
            if (prodList.Count()!=0)
            {
                db.Dispose();
                return Ok(prodList.ToList());
            }
            else
            {
                db.Dispose();
                return NotFound("Tuotteet category id:llä: " + id + " ei löyty");
            }
        }

 //------------------------------PUT update existing customer
        [HttpPut]
        [Route("{key}")]
        public ActionResult PutProdEdit(int key, [FromBody] Products tuote)
        {
            northwindContext db = new northwindContext();
            try
            {
                Products prod = db.Products.Find(key);
                if (prod != null)
                {
                    prod.ProductName = tuote.ProductName;
                    prod.CategoryId = tuote.CategoryId;
                    prod.SupplierId = tuote.SupplierId;
                    prod.QuantityPerUnit = tuote.QuantityPerUnit;
                    prod.UnitPrice = tuote.UnitPrice;
                    prod.UnitsInStock = tuote.UnitsInStock;
                    prod.UnitsOnOrder = tuote.UnitsOnOrder;
                    prod.ImageLink = tuote.ImageLink;
                    db.SaveChanges();

                    return Ok(prod.ProductName + " tuoteen tiedot ovat päivitetty");
                }
                else
                {
                    return NotFound("Päivitettävää tuotetta ei löytynyt");
                }
            }
            catch (Exception)
            {
                return BadRequest("Tuotteen tietojen päivitys ei onnistunut");
                throw;
            }
            finally
            {
                db.Dispose();
            }
        }


    }
}
