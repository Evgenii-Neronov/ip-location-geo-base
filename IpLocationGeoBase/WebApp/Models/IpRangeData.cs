using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class IpRangeData
    {
        public string IpFrom { get; set; }

        public string IpTo { get; set; }

        public LocationData Location { get; set; }
    }
}
