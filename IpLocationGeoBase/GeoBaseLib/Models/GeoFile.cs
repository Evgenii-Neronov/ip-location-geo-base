using System.IO;
using System.Runtime.InteropServices;
using GeoBaseLib.Services.Implementation;

namespace GeoBaseLib.Models
{
    public class GeoFile
    {
        private readonly string geoBaseFileDb;

        public Header Header { get; set; }

        public delegate void FileLoaded(Header header);

        public FileLoaded OnFileLoaded;

        public GeoFile(string geoBaseFileDb)
        {
            this.geoBaseFileDb = geoBaseFileDb;
        }

        public RawData ReadFile()
        {
            using (var br = new BinaryReader(File.Open(geoBaseFileDb,
                FileMode.Open)))
            {
                var reader1 = new GeoBaseReaderV1();

                this.Header = reader1.ReadHeader(br);

                var ipRangesCount = (this.Header.OffsetLocations - 60) / 12;
                var locationsCount = (this.Header.OffsetCities - 60 - this.Header.OffsetLocations) / 96;

                ReadBytesCollections(br,
                    out var ipRangesBytes,
                    ipRangesCount,
                    out var locationsBytes,
                    locationsCount);

                OnFileLoaded?.Invoke(this.Header);

                return new RawData()
                {
                    IpRangesBytes = ipRangesBytes,
                    IpRangesCount = ipRangesCount,
                    LocationsBytes = locationsBytes,
                    LocationsCount = locationsCount,
                };
            }
        }

        private void ReadBytesCollections(BinaryReader br,
            out byte[] ipRangesBytes,
            uint ipRangesLength,
            out byte[] locationsBytes,
            uint locationsLength)
        {
            var ipRangesSize = Marshal.SizeOf<IpRange>();
            var locationsSize = Marshal.SizeOf<Location>();

            ipRangesBytes = br.ReadBytes(ipRangesSize * (int)ipRangesLength);
            locationsBytes = br.ReadBytes(locationsSize * (int)locationsLength);
        }
    }
}
