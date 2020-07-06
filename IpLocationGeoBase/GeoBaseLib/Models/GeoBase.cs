using System;
using System.Runtime.InteropServices;
using GeoBaseLib.Helpers;

namespace GeoBaseLib.Models
{
    public class GeoBase : GeoFile
    {
        public IpRange[] IpRanges { get; set; }

        public Location[] Locations { get; set; }

        public RawData RawData { get; set; }

        public GeoIndexes GeoIndexes { get; set; }

        public GeoBase(string geoBaseFileDb) : base(geoBaseFileDb)
        {

        }

        public Action OnIndexesCreated;
        
        public void Init()
        {
            this.RawData = this.ReadFile();

            this.IpRanges = this.GetEmptyRangesIps(this.RawData.IpRangesCount);
            this.Locations = this.GetEmptyLocations(this.RawData.LocationsCount);

            var ipRanges = this.IpRanges;
            var locations = this.Locations;

            BinaryReaderHelper.RawDeserializerList<IpRange>(
                this.RawData.IpRangesBytes,
                this.RawData.IpRangesCount,
                Marshal.SizeOf<IpRange>(),
                ref ipRanges);

            BinaryReaderHelper.RawDeserializerList<Location>(
                this.RawData.LocationsBytes,
                this.RawData.LocationsCount,
                Marshal.SizeOf<Location>(), 
                ref locations);

            IpRanges = ipRanges;
            Locations = locations;

            InitIndexes();

            OnIndexesCreated?.Invoke();
        }

        public IpRange[] GetEmptyRangesIps(uint ipRangesCount)
        {
            var ipRanges = new IpRange[ipRangesCount];
            for (var i = 0; i < ipRangesCount; i++)
                ipRanges[i] = new IpRange();

            return ipRanges;

        }

        public Location[] GetEmptyLocations(uint locationsCount)
        {
            var locations = new Location[locationsCount];

            for (var i = 0; i < locationsCount; i++)
                locations[i] = new Location();

            return locations;
        }

        public void InitIndexes()
        {
            this.GeoIndexes = new GeoIndexes();

            this.GeoIndexes.InitIpRangesIndex(this.IpRanges);
            this.GeoIndexes.InitLocationsIndex(this.Locations);
        }
    }
}
