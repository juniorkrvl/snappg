using Snappg.Core;
using Snappg.Creator;
using Snappg.Providers.Base.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snappg.Providers.MySql
{
    public class MySqlCreator
    {
        private MySqlProvider _base;
        private string Namespace;

        public MySqlCreator(MySqlProvider _base)
        {
            this._base = _base;
            this.Namespace = Configurator.Config.Entities.Namespace;
        }

        public List<ClassBase> GetObjects()
        {
            try
            {
                List<ClassBase> list = new List<ClassBase>();
                DataTable dt = this._base.GetTables();
                foreach (DataRow row in dt.Rows)
                {
                    PocoClass cBase = new PocoClass();
                    cBase.Namespace = this.Namespace.Length > 0 ? this.Namespace : "";
                    cBase.Name = row["TABLE_NAME"].ToString();
                    Console.WriteLine("Retrieving " + cBase.Name + "...");

                    DataTable mult = this._base.GetMultRelationship(cBase.Name);
                    foreach (DataRow item in mult.Rows)
                    {
                        cBase.MultRelationships.Add(item["Child Table"].ToString());
                    }

                    DataTable singl = this._base.GetSingleRelationship(cBase.Name);
                    foreach (DataRow item in singl.Rows)
                    {
                        cBase.SingleRelationships.Add(item["Parent Table"].ToString());
                    }

                    DataTable columns = this._base.GetColumns(cBase.Name);
                    foreach (DataRow column in columns.Rows)
                    {
                        PropertyBase pBase = new PropertyBase();
                        pBase.Name = column["COLUMN_NAME"].ToString();
                        pBase.Type = column["DATA_TYPE"].ToString();
                        pBase.Nullable = column["IS_NULLABLE"].ToString() == "YES" ? true : false;

                        DataTable keys = this._base.GetKey(cBase.Name, pBase.Name);
                        if (keys.Rows.Count > 0)
                        {
                            DataRow fk = keys.Rows[0];
                            PrimaryKey attr = new PrimaryKey();
                            pBase.Attribute = attr;
                        }

                        cBase.Properties.Add(pBase);
                    }

                    list.Add(cBase);
                }

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
