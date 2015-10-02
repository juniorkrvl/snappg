using Snappg.Core;
using Snappg.Providers.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snappg.Providers.MySql
{
    public class MySqlProvider : ProviderBase
    {
        public override DataTable GetColumns(string TableName)
        {
            string sql = string.Format("SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE FROM information_schema.COLUMNS WHERE TABLE_NAME = '{0}' AND TABLE_SCHEMA = '{1}' ORDER BY ORDINAL_POSITION;", TableName, Database.Name);
            return this.Query(sql);
        }

        public override DataTable GetForeingKey(string TableName, string ColumnName)
        {
            string sql = string.Format("SELECT * FROM information_schema.KEY_COLUMN_USAGE WHERE TABLE_NAME = '{0}' AND COLUMN_NAME = '{1}' AND CONSTRAINT_SCHEMA = '{2}' AND REFERENCED_TABLE_NAME IS NOT NULL;", TableName, ColumnName, Database.Name);
            return this.Query(sql);
        }

        public override DataTable GetKey(string TableName, string ColumnName)
        {
            string sql = string.Format("SELECT CONSTRAINT_NAME FROM information_schema.KEY_COLUMN_USAGE WHERE TABLE_NAME = '{0}' AND COLUMN_NAME = '{1}' AND CONSTRAINT_SCHEMA = '{2}' AND REFERENCED_TABLE_NAME IS null;", TableName, ColumnName, Database.Name);
            return this.Query(sql);
        }

        public override DataTable GetTables()
        {
            try
            {
                string notIn = "";
                if (Configurator.Config.Entities.Ignores != null && Configurator.Config.Entities.Ignores.Length > 0)
                {
                    notIn = string.Format("AND TABLE_NAME NOT IN ({0})", string.Concat("'", string.Join("','", Configurator.Config.Entities.Ignores.ToArray()), "'"));
                }
                string sql = string.Format("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{0}' {1};", Database.Name, notIn);
                return this.Query(sql);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override DataTable GetMultRelationship(string TableName)
        {
            try
            {
                string notIn = "";
                if (Configurator.Config.Entities.Ignores != null && Configurator.Config.Entities.Ignores.Length > 0)
                {
                    notIn = string.Format("AND u.table_name NOT IN ({0})", string.Concat("'", string.Join("','", Configurator.Config.Entities.Ignores.ToArray()), "'"));
                }


                string sql = @"SELECT  
                                u.referenced_table_name as 'Parent Table', 
                                u.table_name as 'Child Table' 
                                FROM information_schema.table_constraints AS c
                                JOIN information_schema.key_column_usage AS u USING(constraint_schema,constraint_name)
                                WHERE c.constraint_type = 'FOREIGN KEY'
                                AND u.referenced_table_schema = '{0}'
                                AND u.referenced_table_name = '{1}'
                                {2}
                                ORDER BY `Parent Table`;";

                //StringBuilder str = new StringBuilder();
                //str.AppendLine("SELECT  ");
                ////str.AppendLine("c.table_schema as 'Parent Schema',");
                //str.AppendLine("u.referenced_table_name as 'Parent Table', ");
                ////str.AppendLine("u.referenced_column_name as 'Parent Column',");
                ////str.AppendLine("u.table_schema as 'Child Schema',");
                //str.AppendLine("u.table_name as 'Child Table' ");
                ////str.AppendLine("u.column_name as 'Child Column'");
                //str.AppendLine("FROM information_schema.table_constraints AS c");
                //str.AppendLine("JOIN information_schema.key_column_usage AS u USING(constraint_schema,constraint_name)");
                //str.AppendLine("WHERE c.constraint_type = 'FOREIGN KEY'");
                //str.AppendLine(string.Format("AND u.referenced_table_schema = '{0}'", Database.Name));
                //str.AppendLine(string.Format("AND u.referenced_table_name = '{0}'", TableName));
                //str.AppendLine("ORDER BY `Parent Table`;");

                return this.Query(string.Format(sql, Database.Name, TableName, notIn));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override DataTable GetSingleRelationship(string TableName)
        {
            //StringBuilder str = new StringBuilder();
            //str.AppendLine("SELECT DISTINCT ");
            ////str.AppendLine("c.table_schema as 'Parent Schema',");
            //str.AppendLine("t.TABLE_NAME as 'Parent Table' ");
            ////str.AppendLine("u.referenced_column_name as 'Parent Column',");
            ////str.AppendLine("u.table_schema as 'Child Schema',");
            ////str.AppendLine("u.table_name as 'Child Table',");
            ////str.AppendLine("u.column_name as 'Child Column'");
            //str.AppendLine("FROM information_schema.table_constraints AS c");
            //str.AppendLine("JOIN information_schema.key_column_usage AS u USING(constraint_schema,constraint_name)");
            //str.AppendLine("JOIN information_schema.TABLES AS t ON u.referenced_table_name = t.TABLE_NAME");
            //str.AppendLine("WHERE c.constraint_type = 'FOREIGN KEY'");
            //str.AppendLine(string.Format("AND u.referenced_table_schema = '{0}'", Database.Name));
            //str.AppendLine(string.Format("AND u.TABLE_NAME = '{0}'", TableName));
            //str.AppendLine("ORDER BY `Parent Table`;");

            string notIn = "";
            if (Configurator.Config.Entities.Ignores != null && Configurator.Config.Entities.Ignores.Length > 0)
            {
                notIn = string.Format("AND t.TABLE_NAME NOT IN ({0})", string.Concat("'", string.Join("','", Configurator.Config.Entities.Ignores.ToArray()), "'"));
            }

            string sql = @"SELECT DISTINCT 
                            t.TABLE_NAME as 'Parent Table' 
                            FROM information_schema.table_constraints AS c
                            JOIN information_schema.key_column_usage AS u USING(constraint_schema,constraint_name)
                            JOIN information_schema.TABLES AS t ON u.referenced_table_name = t.TABLE_NAME
                            WHERE c.constraint_type = 'FOREIGN KEY'
                            AND u.referenced_table_schema = '{0}'
                            AND u.TABLE_NAME = '{1}'
                            {2}
                            ORDER BY `Parent Table`;";

            return this.Query(string.Format(sql, Database.Name, TableName,notIn));
        }



    }
}
