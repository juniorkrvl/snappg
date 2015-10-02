using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snappg.Providers.MySql
{
    public class MySqlTypes
    {
        public static string GetType(string type, bool nullable)
        {
            string normal = type;

            string nulltype = nullable ? "?" : "";
            type = type.ToLower();
            
            switch (type)
            {
                case "tinyint":
                    return "sbyte" + nulltype;
                case "tinyint unsigned":
                    return "byte" + nulltype;
                case "smallint":
                    return "short" + nulltype;
                case "smallint unsigned":
                    return "ushort" + nulltype;
                case "int":
                    return "int" + nulltype;
                case "bigint":
                    return "long" + nulltype;
                case "bigint unsigned":
                    return "ulong" + nulltype;
                case "decimal":
                    return "decimal" + nulltype;
                case "char":
                    return "string";
                case "varchar":
                    return "string";
                case "text":
                    return "string" ;
                case "datetime":
                    return "DateTime" + nulltype;
                case "timestamp":
                    return "DateTime" + nulltype;
                case "bit":
                    return "bool" + nulltype;
                case "blob":
                    return string.Format("byte{0}[]", nulltype);
                default:
                    return normal;
            }

        }
    }
}
