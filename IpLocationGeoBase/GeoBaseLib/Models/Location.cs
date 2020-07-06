using System.Runtime.InteropServices;

namespace GeoBaseLib.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Location
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] Country;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public byte[] Region;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public byte[] Postal;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
        public byte[] City;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] Organization;

        [MarshalAs(UnmanagedType.R4)]
        public float Latitude;

        [MarshalAs(UnmanagedType.R4)]
        public float Longitude;
    }
}