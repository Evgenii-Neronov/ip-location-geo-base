using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using GeoBaseLib.Models;

namespace GeoBaseLib.Services.Implementation
{
    public class GeoBaseReaderV1 
    {
        private HashSet<int> hashSet = new HashSet<int>();

        public Header ReadHeader(BinaryReader br)
        {
            return new Header()
            {
                Version = br.ReadInt32(),
                Name = br.ReadBytes(32),
                TimeStamp = br.ReadUInt64(),
                Records = br.ReadInt32(),

                OffsetRanges = br.ReadUInt32(),
                OffsetCities = br.ReadUInt32(),
                OffsetLocations = br.ReadUInt32(),
            };
        }

        public void InitIpRanges(byte[] bytes, ulong size, ref IpRange[] ipRanges)
        {
            hashSet.Add(Thread.CurrentThread.ManagedThreadId);

            using (var stream = new MemoryStream(bytes))
            using (var br = new BinaryReader(stream))
            {
                for (ulong i = 0; i < size; i++)
                {
                    ipRanges[i].IpFrom = br.ReadUInt32();
                    ipRanges[i].IpTo = br.ReadUInt32();
                    ipRanges[i].LocationIndex = br.ReadUInt32();
                }
            }
        }


        public void InitLocations(byte[] bytes, ulong size, ref Location[] locations)
        {
            hashSet.Add(Thread.CurrentThread.ManagedThreadId);

            using (var stream = new MemoryStream(bytes))
            using (var br = new BinaryReader(stream))
            {
                for (ulong i = 0; i < size; i++)
                {
                    locations[i].Country = br.ReadBytes(8);
                    locations[i].Region = br.ReadBytes(12);
                    locations[i].Postal = br.ReadBytes(12);
                    locations[i].City = br.ReadBytes(24);
                    locations[i].Organization = br.ReadBytes(32);
                    locations[i].Latitude = BitConverter.ToSingle(br.ReadBytes(4), 0);
                    locations[i].Longitude = BitConverter.ToSingle(br.ReadBytes(4), 0);
                }
            }
        }

        public void PrintThreads()
        {
            foreach (var t in hashSet)
            {
                Console.Write($"{t}, ");
            }

            Console.WriteLine();
        }
    }
}