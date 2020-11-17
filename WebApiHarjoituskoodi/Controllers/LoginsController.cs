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
    public class LoginsController : ControllerBase
    {
        northwindContext db = new northwindContext();
        //-------------------get all northwind logins
        [HttpGet]
        [Route("")]
        public List<Logins> GetAllLogins()
        {
            List<Logins> logins = db.Logins.ToList();
            foreach (var login in logins)
            {
                login.PassWord = "*****";
            }
            db.Dispose();
            return logins;
        }

        //-------------------get northwind login by id
        [HttpGet]
        [Route("id/{id}")]
        public ActionResult GetLoginsById(int id)
        {

            var login = db.Logins.Find(id);
            if (login != null)
            {
               
                    login.PassWord = "*****";
               
                db.Dispose();
                return Ok(login);
            }
            else
            {
                db.Dispose();
                return NotFound("Haettu id: " + id + " ei löyty");
            }
        }
        
        //-------------------get northwind login by surname
        [HttpGet]
        [Route("{surname}")]
        public ActionResult GetLoginsBySurname(string surname)
        {

            List<Logins> logins = db.Logins.Where(l => l.Lastname.Contains(surname)).ToList();
            if (logins != null)
            {
                foreach (var login in logins)
                {
                    login.PassWord = "*****";
                }
                db.Dispose();
                return Ok(logins);
            }
            else
            {
                db.Dispose();
                return NotFound("Haettu sukunimi: " + surname + " ei löyty");
            }
        }

        //------------------------------PUT update existing Login
        [HttpPut]
        [Route("{key}")]
        public ActionResult PutKayttajaEdit(int key, [FromBody] Logins login)
        {

            try
            {
                Logins Kayttaja = db.Logins.Find(key);
                if (Kayttaja != null)
                {
                    Kayttaja.LoginId = login.LoginId;
                    Kayttaja.Firstname = login.Firstname;
                    Kayttaja.Lastname = login.Lastname;
                    Kayttaja.Email = login.Email;
                    Kayttaja.UserName = login.UserName;
                    //Kayttaja.PassWord = login.PassWord;
                    Kayttaja.AccesslevelId = login.AccesslevelId;
                   
                    db.SaveChanges();

                    return Ok(Kayttaja.UserName + " käyttäjän tiedot ovat päivitetty");
                }
                else
                {
                    return NotFound("Päivitettävää käyttäjä ei löytynyt");
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

        //-------------------------POST new login

        [HttpPost]
        [Route("")]
        public ActionResult PostNewUser([FromBody] Logins login)
        {
            if (!db.Logins.Any(l => l.UserName == login.UserName))
            {
                try
                {
                    db.Logins.Add(login);
                    db.SaveChanges();
                    return Ok("Uusi käyttäjä lisätty käyttäjänimellä: " + login.UserName);
                }
                catch (Exception)
                {

                    return BadRequest("Käytttäjän lisäystä epäonnistui.");
                }
                finally
                {
                    db.Dispose();

                }
            }
            else
            {
                return BadRequest("Käytttäjän lisäystä epäonnistui. Käyttäjänimi on jo olemassa");
            }
        }
    }
}
