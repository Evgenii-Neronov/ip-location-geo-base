using Microsoft.AspNetCore.Mvc;
using WebApp.Services;

namespace WebApp.ApiControllers
{
    [ApiController]
    public class GeoBaseController : ControllerBase
    {
        private readonly IGeoBaseService _geoBaseService;

        public GeoBaseController(IGeoBaseService geoBaseService)
        {
            this._geoBaseService = geoBaseService;
        }

        /// <summary>
        /// Get location by ip
        /// </summary>
        [HttpGet]
        [Route("ip/location")]
        public string GetLocationByIp(string ip)
        {
            var s = this._geoBaseService.GetGeoBase().Locations.Length.ToString();

            return $"{s} {ip}";
        }

        /// <summary>
        /// Get locations by city
        /// </summary>
        [HttpGet]
        [Route("city/locations")]
        public string GetLocationByCity(string city)
        {
            var s = this._geoBaseService.GetGeoBase().Locations.Length.ToString();

            return $"{s} {city}";
        }

    }
}