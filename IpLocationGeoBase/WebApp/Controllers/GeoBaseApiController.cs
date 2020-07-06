using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoBaseApiController : ControllerBase
    {
        /// <summary>
        /// Get telemetry states
        /// </summary>
        [HttpGet]
        [Route("v1/test/")]
        public string Test()
        {
            string s = "";
            var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());

            using (var reader = embeddedProvider.GetFileInfo(@"Content\geobase.dat").CreateReadStream())
            {
                var b = reader.ReadByte();
                s = b.ToString();
            }

            return "test " + DateTime.Now.ToString() + " " + s;
        }
    }
}