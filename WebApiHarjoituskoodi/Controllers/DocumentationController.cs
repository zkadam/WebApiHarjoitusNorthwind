using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiHarjoituskoodi.Models;

namespace WebApiHarjoituskoodi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentationController : ControllerBase
    {
        //-------------------get documentation by keycode
        [HttpGet]
        [Route("{key}")]
        //----------------------------------------------------OPTION ONE
        //    public List<Documentation> GetDocumentation(string key)
        //    {
        //        northwindContext db = new northwindContext();

        //        List<Documentation> privateDocList = (from d in db.Documentation
        //                                              where d.Keycode == key
        //                                              select d).ToList();

        ////jos key ei löytyy laitetaan listalle virheilmoituksen
        //        if (privateDocList.Count==0)
        //        {

        //            privateDocList.Add(new Documentation()
        //            {
        //                DocumentationId = 0,
        //                Description = "Document not found"
        //            });
        //    }
        //    return privateDocList;
        //}
        public ActionResult GetDocs(string key)
        {
            northwindContext db = new northwindContext();

            List<Documentation> privateDocList = (from d in db.Documentation
                                                  where d.Keycode == key
                                                  select d).ToList();
            if (privateDocList.Count>0)
            {
            return Ok(privateDocList);

            }
            else
            {
                return BadRequest("Antamallasi koodilla ei löydy dokumentaatioita " + DateTime.Now.ToString());
            }
        }
    }
}
