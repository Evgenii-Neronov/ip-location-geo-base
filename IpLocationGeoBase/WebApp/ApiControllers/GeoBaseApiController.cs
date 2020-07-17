using System;
using System.Linq;
using GeoBaseLib.Helpers;
using GeoBaseLib.Models;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.ApiControllers
{
    [ApiController]
    public class GeoBaseApiController : ControllerBase
    {
        private readonly IGeoBaseService _geoBaseService;

        private GeoBase GeoBase => _geoBaseService.GetGeoBase();

        public GeoBaseApiController(IGeoBaseService geoBaseService)
        {
            this._geoBaseService = geoBaseService;
        }

        /// <summary>
        /// Get location by ip
        /// </summary>
        [HttpGet]
        [Route("ip/location")]
        public LocationData GetLocationByIp(string ip)
        {
            var ipAddress = ip.IpToUInt();

            var ipRange = this.GeoBase.IpRanges
                .FirstOrDefault(x => 
                    x.IpFrom <= ipAddress 
                    && x.IpTo >= ipAddress);

            var locationId = this.GeoBase.GeoIndexes.IpRanges_IpFrom[ipRange.IpFrom];
            var location = this.GeoBase.Locations[locationId];

            return new LocationData()
            {
                City = location.City.ConvertToString(),
                Country = location.Country.ConvertToString(),
                Organization = location.Organization.ConvertToString(),
                Postal = location.Postal.ConvertToString(),
                Region = location.Region.ConvertToString(),
                Latitude = location.Latitude,
                Longitude = location.Longitude,
            };
        }

        /// <summary>
        /// Get locations by city
        /// </summary>
        [HttpGet]
        [Route("city/locations")]
        public IpRangeData GetLocationByCity(string city)
        {
            //city = "cit_Anetositoz";

            var ipLocationIndex = this.GeoBase.GeoIndexes.City_IpLocationIndex[city];

            var location = this.GeoBase.Locations[ipLocationIndex];

            var rangeId = this.GeoBase.GeoIndexes.Location_Range[ipLocationIndex];

            var ipRange = this.GeoBase.IpRanges[rangeId];

            return new IpRangeData()
            {
                IpFrom = ipRange.IpFrom.IpToString(),
                IpTo = ipRange.IpTo.IpToString(),
                Location = new LocationData()
                {
                    City = location.City.ConvertToString(),
                    Country = location.Country.ConvertToString(),
                    Organization = location.Organization.ConvertToString(),
                    Postal = location.Postal.ConvertToString(),
                    Region = location.Region.ConvertToString(),
                    Latitude = location.Latitude,
                    Longitude = location.Longitude,
                }
            };
        }

    }
}