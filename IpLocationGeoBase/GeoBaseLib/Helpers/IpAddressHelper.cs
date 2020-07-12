using System.Net;

namespace GeoBaseLib.Helpers
{
    public static class IpAddressHelper
    {
        public static uint IpToUInt(this string address)
        {
            return (uint)IPAddress.NetworkToHostOrder(
                (int)IPAddress.Parse(address).Address);
        }

        public static string IpToString(this uint address)
        {
            return IPAddress.Parse(address.ToString()).ToString();
        }
    }
}