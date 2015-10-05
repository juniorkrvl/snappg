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

                    if (Configurator.Config.Pocos.Relations.OneToMany != null && Configurator.Config.Pocos.Relations.OneToMany == true)
                    {
                        DataTable mult = this._base.GetMultRelationship(cBase.Name);
                        foreach (DataRow item in mult.Rows)
                        {
                            cBase.MultRelationships.Add(item["Child Table"].ToString());
                        }
                    }

                    if (Configurator.Config.Pocos.Relations.OneToOne != null && Configurator.Config.Pocos.Relations.OneToOne == true)
                    {
                        DataTable singl = this._base.GetSingleRelationship(cBase.Name);
                        foreach (DataRow item in singl.Rows)
                        {
                            cBase.SingleRelationships.Add(item["Parent Table"].ToString());
                        }
                    }

                    List<string> PrimaryKeys = new List<string>();
                    List<string> ForeignKeys = new List<string>();

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
                            PrimaryKeys.Add(pBase.Name);
                            string attribute = "";
                            if (Configurator.Config.Pocos.Attributes.PK != null)
                            {
                                foreach (var pk in Configurator.Config.Pocos.Attributes.PK)
                                {
                                    if (!pk.Trim().Equals(String.Empty))
                                    {
                                        string compiled = "";
                                        compiled = pk.Replace("$database$", Configurator.Config.Database.Name);
                                        compiled = compiled.Replace("$table$", cBase.Name);
                                        compiled = compiled.Replace("$pk$", pBase.Name);
                                        attribute += compiled + "\r\n";
                                    }
                                }
                            }
                            if (attribute.Length > 0)
                            {
                                AttributeBase pkAttr = new AttributeBase();
                                pkAttr.Attribute = attribute;
                                pBase.Attributes.Add(pkAttr);
                            }
                        }

                        DataTable fkeys = _base.GetForeingKey(cBase.Name, pBase.Name);
                        if (fkeys.Rows.Count > 0)
                        {
                            ForeignKeys.Add(pBase.Name);
                            string attribute = "";
                            if (Configurator.Config.Pocos.Attributes.FK != null)
                            {
                                foreach (var fk in Configurator.Config.Pocos.Attributes.FK)
                                {
                                    if (!fk.Trim().Equals(String.Empty))
                                    {
                                        string compiled = "";
                                        compiled = fk.Replace("$database$", Configurator.Config.Database.Name);
                                        compiled = compiled.Replace("$table$", cBase.Name);
                                        compiled = compiled.Replace("$fk$", pBase.Name);
                                        attribute += compiled + "\r\n";
                                    }
                                }
                            }
                            if (attribute.Length > 0)
                            {
                                AttributeBase fkAttr = new AttributeBase();
                                fkAttr.Attribute = attribute;
                                pBase.Attributes.Add(fkAttr);
                            }
                        }

                        if (Configurator.Config.Pocos.Attributes.Columns != null)
                        {
                            string attrCols = "";
                            foreach (var col in Configurator.Config.Pocos.Attributes.Columns)
                            {
                                if (!col.Trim().Equals(String.Empty))
                                {
                                    string compiled = "";
                                    compiled = col.Replace("$database$", Configurator.Config.Database.Name);
                                    compiled = compiled.Replace("$table$", cBase.Name);
                                    compiled = compiled.Replace("$column$", pBase.Name);
                                    attrCols += compiled + "\r\n";
                                }
                            }
                            if (attrCols.Length > 0)
                            {
                                AttributeBase pkAttr = new AttributeBase();
                                pkAttr.Attribute = attrCols;
                                pBase.Attributes.Add(pkAttr);
                            }
                        }

                        cBase.Properties.Add(pBase);
                    }

                    string tableAttr = "";
                    if (Configurator.Config.Pocos.Attributes.Table != null)
                    {
                        foreach (var t in Configurator.Config.Pocos.Attributes.Table)
                        {
                            string compiled = "";
                            compiled = t.Replace("$database$", Configurator.Config.Database.Name);
                            compiled = compiled.Replace("$table$", cBase.Name);
                            compiled = compiled.Replace("$fks$", string.Join(",", ForeignKeys.ToArray()));
                            compiled = compiled.Replace("$pks$", string.Join(",", PrimaryKeys.ToArray()));

                            tableAttr += compiled + "\r\n";
                        }
                        cBase.Attributes = tableAttr;
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
