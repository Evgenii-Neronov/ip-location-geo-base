using System;
using System.Collections.Generic;
using System.Text;

namespace GeoBaseLib.Models
{
    public class RawData
    {
        public byte[] IpRangesBytes { get; set; }
        public byte[] LocationsBytes { get; set; }
        public uint IpRangesCount { get; set; }
        public uint LocationsCount { get; set; }
    }
}