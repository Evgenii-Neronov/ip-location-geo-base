using System;
using System.Diagnostics;
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
            var geoBase = new GeoBase(fileName);

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

        public static void Print(object str)
        {
            Console.WriteLine(str);
        }

        public static DateTime GetDateTimeFromUInt(ulong seconds)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1);
            dtDateTime = dtDateTime.AddSeconds(seconds).ToLocalTime();
            return dtDateTime;
        }
    }
}
