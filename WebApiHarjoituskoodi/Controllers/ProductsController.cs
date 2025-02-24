﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiHarjoituskoodi.Models;

namespace WebApiHarjoituskoodi.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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



        [HttpGet]
        [Route("R")]
        //-------------------------------------------------------FILTERING RESULTS AND PAGINATING
        //https://localhost:5001/nw/Products/R?offset=10&limit=2
        public ActionResult GetSomeProducts(int? offset, int? limit, string prodname)
        {
            int offset2 = offset ?? 0;
            int limit2 = limit ?? 0;
            northwindContext db = new northwindContext();

            //jos annettu prodname parameter
            if (prodname != null)
            {
                //jos ei ole sivurajoituksia ja valittu sivunumeroita
                if (offset2 == 0 && limit2 == 0)
                {
                    List<Products> tuotteet = db.Products.Where(d => d.ProductName.Contains(prodname)).ToList();
                    return Ok(tuotteet);
                }
                //jos ei ole sivurajoituksia 

                else if (offset2 == 0)
                {
                    List<Products> tuotteet = db.Products.Where(d => d.ProductName.Contains(prodname)).Take(limit2).ToList();
                    return Ok(tuotteet);
                }
                //jos ei ole  valittu sivunumero

                else if (limit2 == 0)
                {
                    List<Products> tuotteet = db.Products.Where(d => d.ProductName.Contains(prodname)).Take(limit2).ToList();
                    return Ok(tuotteet);
                }

                //jos on sivurajoituksia ja valittu sivunumeroita
                else
                {
                    List<Products> tuotteet = db.Products.Where(d => d.ProductName.Contains(prodname)).Skip(offset2).Take(limit2).ToList();
                    return Ok(tuotteet);

                }
            }
            //-------------EI OLE ANNETTU prodname PARAMETER
            else
            {
                //jos ei ole sivurajoituksia ja valittu sivunumeroita
                if (offset2 == 0 && limit2 == 0)
                {
                    List<Products> tuotteet = db.Products.ToList();
                    return Ok(tuotteet);
                }
                //jos ei ole sivurajoituksia 

                else if (offset2 == 0)
                {
                    List<Products> tuotteet = db.Products.Take(limit2).ToList();
                    return Ok(tuotteet);
                }
                //jos ei ole  valittu sivunumero

                else if (limit2 == 0)
                {
                    List<Products> tuotteet = db.Products.Take(limit2).ToList();
                    return Ok(tuotteet);
                }

                //jos on sivurajoituksia ja valittu sivunumeroita
                else
                {
                    List<Products> tuotteet = db.Products.Skip(offset2).Take(limit2).ToList();
                    return Ok(tuotteet);

                }
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
                //db.Dispose();
                return Ok(prodList.ToList());
            }
            else
            {
                db.Dispose();
                return NotFound("Tuotteet category id:llä: " + id + " ei löyty");
            }
        }

//-------------------------POST new Product

        [HttpPost]
        [Route("")]
        public ActionResult PostNewProduct([FromBody] Products tuote)
        {

            try
            {
                db.Products.Add(tuote);
                db.SaveChanges();
                return Ok("Uusi tuote lisätty id:llä: " + tuote.ProductId);
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


        //------------------------------PUT update existing product
        [HttpPut]
        [Route("{key}")]
        public ActionResult PutProdEdit(int key, [FromBody] Products tuote)
        {
            
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
                    prod.Discontinued = tuote.Discontinued;
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

 //---------------------------------DELETE product poisto
        [HttpDelete]
        [Route("{key}")]
        public ActionResult DeleteProduct(int key)
        {
            Products tuote = db.Products.Find(key);
            if (tuote != null)
            {
                db.Products.Remove(tuote);
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
