using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using GeoBaseLib.Helpers;
using GeoBaseLib.Models;

namespace ConsoleApp
{
    public class Program
    {
        private static readonly Stopwatch sw = new Stopwatch();

        private static readonly string fileName = @"C:\test-dot-net-geobase\geobase.dat";

        public static Logger Logger = new Logger(Console.OpenStandardOutput());

        static void Main(string[] args)
        {
            Logger.LogInfo("Load geo base stared...");

            sw.Start();

            try
            {
                LoadGeoBase();
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e.StackTrace);
            }

            Logger.LogInfo($"Finished: {sw.GetFormattedElapsed()}");
        }

        public static void LoadGeoBase()
        {
            using (var binaryReader = new BinaryReader(File.Open(fileName,
                FileMode.Open)))
            {
                var geoBase = new GeoBase(binaryReader);

                geoBase.OnFileLoaded = (header) =>
                {
                    Logger.LogInfo($"File loaded: {sw.GetFormattedElapsed()}");

                    LogHeader(header);
                };

                geoBase.OnIndexesCreated = () =>
                {
                    Logger.LogInfo($"Indexes created: {sw.GetFormattedElapsed()}");
                };

                geoBase.Init();

                foreach (var l in geoBase.Locations)
                {
                    Logger.LogInfo(l.City.ConvertToString());
                    break;
                }

                var groups = geoBase.Locations.GroupBy(x => x.City);

                var counter = 0;
                foreach (var g in groups.OrderBy(x=>x.Count()))
                {
                    Logger.LogInfo($"{counter++} {g.Key.ConvertToString()} - {g.Count()}");
                }

                var index = geoBase.GeoIndexes.City_IpLocationIndex["cit_Anetositoz"];


                // check indexes for myself
                Logger.LogInfo(geoBase.IpRanges[index].IpTo.IpToString());
                Logger.LogInfo(geoBase.IpRanges[index].LocationIndex);
                Logger.LogInfo(index);


                Logger.LogInfo($" count of Ranges: {geoBase.IpRanges.Length}, count of locations: {geoBase.Locations.Length}");
            }

        }

        private static void LogHeader(Header header)
        {
            Logger.LogInfo($"");
            Logger.LogInfo($"[header]");
            Logger.LogInfo($"Version: 1");
            Logger.LogInfo($"Name: {header.Name}");
            Logger.LogInfo($"Time: {GetDateTimeFromUInt(header.TimeStamp)}");
            Logger.LogInfo($"Records: {header.Records}");
            Logger.LogInfo($"offsetRanges: {header.OffsetRanges}");
            Logger.LogInfo($"offsetCities: {header.OffsetCities}");
            Logger.LogInfo($"offsetLocations: {header.OffsetLocations}");
            Logger.LogInfo($"");
        }

        public static DateTime GetDateTimeFromUInt(ulong seconds)
        {
            var dtDateTime = new DateTime(1970, 1, 1);
            dtDateTime = dtDateTime.AddSeconds(seconds).ToLocalTime();
            return dtDateTime;
        }
    }
}
