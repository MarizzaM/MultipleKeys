using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleKeys
{
    static class ReaderWriterStatic
    {
        // if you are really tired (or lazy...)
        public static void Read()
        {
            lock (typeof(ReaderWriterStatic)) // reflection
            {
                // ...
            }
        }
        // if you are really tired (or lazy...)
        public static void Write()
        {
            lock (typeof(ReaderWriterStatic)) // reflection
            {
                // ...
            }
        }

        static object key = new object();
        // if you are really tired (or lazy...)
        public static void ReadWithKey()
        {
            lock (key) // reflection
            {
                // ...
            }
        }

        // if you are really tired (or lazy...)
        public static void WriteWthKey()
        {
            lock (key) // reflection
            {
                // ...
            }
        }
    }
}

