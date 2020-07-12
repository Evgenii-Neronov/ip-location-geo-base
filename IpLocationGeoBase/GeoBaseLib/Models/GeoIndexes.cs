using System;
using System.Collections.Generic;
using System.Text;
using GeoBaseLib.Helpers;

namespace GeoBaseLib.Models
{
    public class GeoIndexes
    {
        public SortedDictionary<ulong, int> IpRanges_IpFrom { get; set; }
        public SortedDictionary<ulong, int> IpRanges_IpTo { get; set; }
        public SortedDictionary<ulong, int> IpRanges_IpLocationIndex { get; set; }
        public SortedDictionary<string, uint> City_IpLocationIndex { get; set; }

        public SortedDictionary<uint, int> Location_Range { get; set; }

        public void InitIpRangesIndex(IpRange[] ipRanges)
        {
            IpRanges_IpFrom = new SortedDictionary<ulong, int>();
            IpRanges_IpTo = new SortedDictionary<ulong, int>();
            IpRanges_IpLocationIndex = new SortedDictionary<ulong, int>();
            Location_Range = new SortedDictionary<uint, int>();

            for (var i = 0; i < ipRanges.Length; i++)
            {
                IpRanges_IpFrom[ipRanges[i].IpFrom] = i;
                IpRanges_IpTo[ipRanges[i].IpTo] = i;
                IpRanges_IpLocationIndex[ipRanges[i].LocationIndex] = i;
                Location_Range[ipRanges[i].LocationIndex] = i;
            }
        }

        public void InitLocationsIndex(Location[] locations)
        {
            City_IpLocationIndex = new SortedDictionary<string, uint>();

            for (uint i = 0; i < locations.Length; i++)
            {
                City_IpLocationIndex[locations[i].City.ConvertToString()] = i;
            }
        }
    }
}