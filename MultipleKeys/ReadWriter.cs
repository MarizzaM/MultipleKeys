using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleKeys
{
    class ReadWriter
    {
        public void Read()
        {
            lock (this)
            {
                // ...
            }
        }
        public void Write()
        {
            lock (this)
            {
                // ...
            }
        }
    }
}
