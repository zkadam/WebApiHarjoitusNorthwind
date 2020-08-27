using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiHarjoituskoodi.Models;

namespace WebApiHarjoituskoodi.Controllers
{
    [Route("omareitti/[controller]")]
    [ApiController]
    public class HenkilotController : ControllerBase
    {
        //------------------------------tervehdys
        [HttpGet]
        [Route("merkkijono/{nimi}")]
        public string MerkkiJono(string nimi)
        {
            return "Päivää mailma!" + nimi;
        }
        //------------------------------paivamaara

        [HttpGet]
        [Route("paivamaara")]
        public  DateTime Pvm()
        {
            return DateTime.Now;
        }

        //------------------------------olio kokeilu
        [HttpGet]
        [Route("olio")]
        public Henkilo Olio()
        {
            return new Henkilo()
            {
                Nimi = "Paavo Pesusieni",
                Osoite = "Vesipolku 11",
                Ika = 11
            };
        }
        
        
        [HttpGet]
        [Route("oliolista")]
        public List<Henkilo> Oliolista()
        {
            List<Henkilo> henkilot =new List<Henkilo>()
            {
                new Henkilo()
                {
                Nimi = "Paavo Pesusieni",
                Osoite = "Metsätie 2",
                Ika = 22
                },
                new Henkilo()
                {
                Nimi = "Paavo Pesusieni",
                Osoite = "Vesipolku 11",
                Ika = 11
                },
               new Henkilo()
                {
                Nimi = "Paavo Pesusieni",
                Osoite = "Karhunkatu 14",
                Ika = 33
                }
            };
            return henkilot;
        }
    }
}
