using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace ConsoleApp
{
    public class Logger : StreamWriter
    {
        public void LogInfo(string str)
        {
            this.WriteLine($"[{Thread.CurrentThread.ManagedThreadId} Info {DateTime.Now.ToLongTimeString()}]\t{str}");
        }

        public void LogError(string str, string stackTrace = null)
        {
            this.WriteLine($"[{Thread.CurrentThread.ManagedThreadId} Error {DateTime.Now.ToLongTimeString()}]\t{str}");

            if(stackTrace != null)
                this.WriteLine(stackTrace);
        }

        public Logger(Stream stream) : base(stream)
        {
            this.AutoFlush = true;
        }

        public Logger(Stream stream, Encoding encoding) : base(stream, encoding)
        {
        }

        public Logger(Stream stream, Encoding encoding, int bufferSize) : base(stream, encoding, bufferSize)
        {
        }

        public Logger(Stream stream, Encoding encoding, int bufferSize, bool leaveOpen) : base(stream, encoding, bufferSize, leaveOpen)
        {
        }

        public Logger(string path) : base(path)
        {
        }

        public Logger(string path, bool append) : base(path, append)
        {
        }

        public Logger(string path, bool append, Encoding encoding) : base(path, append, encoding)
        {
        }

        public Logger(string path, bool append, Encoding encoding, int bufferSize) : base(path, append, encoding, bufferSize)
        {
        }
    }
}
