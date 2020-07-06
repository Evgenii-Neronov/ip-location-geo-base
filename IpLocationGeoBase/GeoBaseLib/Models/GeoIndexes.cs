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
        public SortedDictionary<string, int> Locations_City { get; set; }
        
        public void InitIpRangesIndex(IpRange[] ipRanges)
        {
            IpRanges_IpFrom = new SortedDictionary<ulong, int>();
            IpRanges_IpTo = new SortedDictionary<ulong, int>();
            IpRanges_IpLocationIndex = new SortedDictionary<ulong, int>();

            for (var i = 0; i < ipRanges.Length; i++)
            {
                IpRanges_IpFrom[ipRanges[i].IpFrom] = i;
                IpRanges_IpTo[ipRanges[i].IpTo] = i;
                IpRanges_IpLocationIndex[ipRanges[i].LocationIndex] = i;
            }
        }

        public void InitLocationsIndex(Location[] locations)
        {
            Locations_City = new SortedDictionary<string, int>();

            for (var i = 0; i < locations.Length; i++)
            {
                Locations_City[locations[i].City.ConvertToString()] = i;
            }
        }
    }
}