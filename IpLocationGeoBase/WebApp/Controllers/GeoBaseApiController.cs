using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GeoBaseLib.Models;
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
                using (var br = new BinaryReader(reader))
                {
                    var geoBase = new GeoBase(br);

                    geoBase.Init();

                    s = geoBase.Locations.Length.ToString();
                }
            }

            return "test " + DateTime.Now.ToString() + " " + s;
        }
    }
}