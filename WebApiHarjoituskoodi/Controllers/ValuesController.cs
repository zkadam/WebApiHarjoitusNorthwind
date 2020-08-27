using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApiHarjoituskoodi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2", "Jotain", "miks ei" };
        }

        // GET api/values/Adam
        [HttpGet("{nimi}")]
        public ActionResult<string> Get(string nimi)
        {
            return "Moi " + nimi + "!";
        }

        // GET api/values/Adam/Zakar
        [HttpGet("{etunimi}/{sukunimi}")]
        public ActionResult<string> Get(string etunimi, string sukunimi)
        {
            return "Moi " + etunimi + " " + sukunimi + "!";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
