using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiHarjoituskoodi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        [HttpGet]
        [Route("{key}")]
        public string GetWeather(string key)
        {
            WebClient client = new WebClient();
            try
            {
                string data = client.DownloadString("https://ilmatieteenlaitos.fi/saa/" + key);
                int index = data.IndexOf("<span class=\"temperature-");
                if (index>0)
                {
                   string weather = data.Substring(index + 47, 3);
                    return weather;
                }
            }
            finally
            {

                client.Dispose();
            }
            return "(unknown)";

        }

    }
}
