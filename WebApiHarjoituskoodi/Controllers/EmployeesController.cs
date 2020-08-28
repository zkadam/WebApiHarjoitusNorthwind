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
    public class EmployeesController : ControllerBase
    {
        northwindContext db = new northwindContext();

        //-------------------get all northwind employees
        [HttpGet]
        [Route("")]
        public List<Employees> GetAllEmployees()
        {
            northwindContext db = new northwindContext();
            List<Employees> employees = db.Employees.ToList();
            db.Dispose();
            return employees;
        }

        //-------------------get northwind employee by id
        [HttpGet]
        [Route("{id}")]
        public ActionResult GetOneEmployee(int id)
        {

            Employees pomo = db.Employees.Find(id);
            if (pomo != null)
            {
                db.Dispose();
                return Ok(pomo);
            }
            else
            {
                db.Dispose();
                return NotFound("Employee id:llä: " + id + " ei löyty");
            }
        }

        //-------------------get northwind employee by Region
        [HttpGet]
        [Route("Region/{id}")]
        public ActionResult GetEmployeesByCat(string id)
        {
            var pomoList = from p in db.Employees
                           where p.Region == id
                           select p;
            if (pomoList.Count() != 0)
            {
                //db.Dispose();
                return Ok(pomoList.ToList());
            }
            else
            {
                db.Dispose();
                return NotFound("Esimies regionissa: " + id + " ei löyty");
            }
        }

        //-------------------------POST new Employee

        [HttpPost]
        [Route("")]
        public ActionResult PostNewEmployee([FromBody] Employees pomo)
        {

            try
            {
                db.Employees.Add(pomo);
                db.SaveChanges();
                return Ok("Uusi pomo lisätty id:llä: " + pomo.EmployeeId);
            }
            catch (Exception)
            {

                return BadRequest("Esimiehen lisäystä epäonnistui.");
            }
            finally
            {
                db.Dispose();

            }
        }


        //------------------------------PUT update existing employee
        [HttpPut]
        [Route("{key}")]
        public ActionResult PutPomoEdit(int key, [FromBody] Employees pomo)
        {

            try
            {
                Employees empu = db.Employees.Find(key);
                if (empu != null)
                {
                    empu.FirstName = pomo.FirstName;
                    empu.LastName = pomo.LastName;
                    empu.Title = pomo.Title;
                    empu.TitleOfCourtesy = pomo.TitleOfCourtesy;
                    empu.Address = pomo.Address;
                    empu.City = pomo.City;
                    empu.Country = pomo.Country;
                    
                    db.SaveChanges();

                    return Ok(empu.FirstName +" "+ empu.LastName + "-in tiedot ovat päivitetty");
                }
                else
                {
                    return NotFound("Päivitettävää henkilö ei löytynyt");
                }
            }
            catch (Exception)
            {
                return BadRequest("Esimiehen tietojen päivitys ei onnistunut");
                throw;
            }
            finally
            {
                db.Dispose();
            }
        }

        //---------------------------------DELETE employee poisto
        [HttpDelete]
        [Route("{key}")]
        public ActionResult DeleteEmployee(int key)
        {
            Employees pomo = db.Employees.Find(key);
            if (pomo != null)
            {
                db.Employees.Remove(pomo);
                db.SaveChanges();
                db.Dispose();
                return Ok("Esimiehen id:llä: " + key + " poistettiin.");
            }
            else
            {
                db.Dispose();
                return NotFound("Esimies " + key + " ei löydy");
            }

        }

    }
}
