using Snappg.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snappg.Providers.Base
{
    public class ConnectionBase
    {
        protected DbProviderFactory factory;
        protected DbDataAdapter myAdapter;
        protected DbConnection conn;

        protected Database Database;

        public ConnectionBase()
        {
            try
            {
                this.Database = Configurator.Config.Database;
                DbProviderFactories.GetFactoryClasses();
                this.conn = GetConnection();
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }

        protected DbConnection GetConnection()
        {
            try
            {
                factory = DbProviderFactories.GetFactory(Database.Provider);
                DbConnection conn = factory.CreateConnection();
                conn.ConnectionString = string.Format("server={0};user id={1};password={2};database={3}",Database.Server,Database.User, Database.Password, Database.Name);
                return conn;
            }
            catch (DbException myerro)
            {
                throw myerro;
            }
        }

        protected DataTable Query(string sql)
        {
            try
            {
                DataTable dt = new DataTable();
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;

                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                        reader.Close();
                    }
                }

                return dt;

            }
            catch (DbException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        protected int Execute(string sql)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                using (DbCommand cmd = conn.CreateCommand())
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (DbException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        protected void Execute(string sql, out int affected)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                using (DbCommand cmd = conn.CreateCommand())
                {
                    affected = Convert.ToInt32(cmd.ExecuteNonQuery());
                }
            }
            catch (DbException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
