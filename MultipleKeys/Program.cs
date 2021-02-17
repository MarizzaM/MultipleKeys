using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleKeys
{
    class Program
    {
        static void Main(string[] args)
        {
            DualPool du1 = new DualPool();
            DualPool du2 = new DualPool();
            du1.GetConnection1(); // thread1 DualPool.key1 --
            du1.GetConnection2(); // thread2 du1.key2
            du2.GetConnection1(); // thread3 DualPool.key1 --
            du2.GetConnection2(); // thread4 du2.key2

        }
    }
}
