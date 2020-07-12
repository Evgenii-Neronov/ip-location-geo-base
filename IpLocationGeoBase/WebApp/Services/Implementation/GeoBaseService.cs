using GeoBaseLib.Models;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace WebApp.Services.Implementation
{
    public class GeoBaseService : IGeoBaseService
    {
        //private readonly string _geoBaseFileName = @"Content\geobase.dat";
        private readonly GeoBase _geoBase;

        public GeoBaseService()
        {
            var geoBaseFileName = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("AppSettings")["geoBaseFileName"];

            var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
            using var reader = embeddedProvider.GetFileInfo(geoBaseFileName).CreateReadStream();
            using var br = new BinaryReader(reader);

            this._geoBase = new GeoBase(br);
            this._geoBase.Init();
        }

        public GeoBase GetGeoBase()
        {
            return this._geoBase;
        }
    }
}
