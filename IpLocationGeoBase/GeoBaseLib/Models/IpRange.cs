using System.Runtime.InteropServices;

namespace GeoBaseLib.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct IpRange
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint IpFrom;

        [MarshalAs(UnmanagedType.U4)]
        public uint IpTo;

        [MarshalAs(UnmanagedType.U4)]
        public uint LocationIndex;
    }
}
