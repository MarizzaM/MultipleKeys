using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultipleKeys
{
    public class DbConnection { }
    public class DualPool
    {
        const int MAX_CONNECTIONS = 40;
        private static object key1 = new object();
        private static object key2 = new object();
        Queue<DbConnection> m_connections_pool1 = new Queue<DbConnection>();

        Queue<DbConnection> m_connections_pool2 = new Queue<DbConnection>();

        public DualPool()
        {
            for (int i = 0; i < MAX_CONNECTIONS; i++)
            {
                m_connections_pool1.Enqueue(new DbConnection());
                m_connections_pool2.Enqueue(new DbConnection());
            }

        }

        public DbConnection GetConnection1()
        {
            try
            {
                Monitor.Enter(key1);
                while (m_connections_pool1.Count == 0)
                {
                    Monitor.Wait(key1);
                }
                var conn = m_connections_pool1.Dequeue();
                return conn;
            }
            finally
            {
                Monitor.Exit(key1);
            }
        }

        public DbConnection GetConnection2()
        {
            try
            {
                Monitor.Enter(key2);
                while (m_connections_pool2.Count == 0)
                {
                    Monitor.Wait(key2);
                }
                var conn = m_connections_pool2.Dequeue();
                return conn;
            }
            finally
            {
                Monitor.Exit(key2);
            }
        }

        public void RestoreConnection1(DbConnection conn)
        {
            try
            {
                Monitor.Enter(key1);
                m_connections_pool1.Enqueue(conn);
            }
            finally
            {
                Monitor.Exit(key1);
            }
        }

        public void RestoreConnection2(DbConnection conn)
        {
            try
            {
                Monitor.Enter(key2);
                m_connections_pool1.Enqueue(conn);
            }
            finally
            {
                Monitor.Exit(key2);
            }
        }

        public void ResetAllResources1(DbConnection conn)
        {
            try
            {
                Monitor.Enter(key1);
                for (int i = 0; i < MAX_CONNECTIONS; i++)
                {
                    m_connections_pool1.Enqueue(new DbConnection());
                }
                Monitor.PulseAll(key1);
            }
            finally
            {
                Monitor.Exit(key1);
            }
        }

        public void ResetAllResources2(DbConnection conn)
        {
            try
            {
                Monitor.Enter(key2);
                for (int i = 0; i < MAX_CONNECTIONS; i++)
                {
                    m_connections_pool1.Enqueue(new DbConnection());
                }
                Monitor.PulseAll(key2);
            }
            finally
            {
                Monitor.Exit(key2);
            }
        }
    }
}
