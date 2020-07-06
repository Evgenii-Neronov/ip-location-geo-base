using System.Runtime.InteropServices;

namespace GeoBaseLib.Models
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct Header
    {
        [MarshalAs(UnmanagedType.I4)]
        public int Version;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] Name;
        
        [MarshalAs(UnmanagedType.U8)]
        public ulong TimeStamp;

        [MarshalAs(UnmanagedType.I4)]
        public int Records;

        [MarshalAs(UnmanagedType.U4)]
        public uint OffsetRanges;

        [MarshalAs(UnmanagedType.U4)]
        public uint OffsetCities;

        [MarshalAs(UnmanagedType.U4)]
        public uint OffsetLocations;
    }
}
