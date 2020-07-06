using System;
using System.Runtime.InteropServices;

namespace GeoBaseLib.Helpers
{
    internal static class BinaryReaderHelper 
    {

        public static void RawDeserializerList<T>(byte[] bytes, uint size, int structSize, ref T[] array) where T : struct
        {
            var unmanagedPointer = Marshal.AllocHGlobal(bytes.Length);
            Marshal.Copy(bytes, 0, unmanagedPointer, bytes.Length);
            
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            try
            {
                var seekBase = unmanagedPointer.ToInt64();

                for (uint i = 0; i < size; i++)
                {
                    var seek = seekBase + (structSize * i);

                    array[i] = (T) Marshal.PtrToStructure(new IntPtr(seek), typeof(T));
                }

            }
            finally
            {
                handle.Free();
            }

            Marshal.FreeHGlobal(unmanagedPointer);
        }
    }
}