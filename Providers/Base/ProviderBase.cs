using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Snappg.Providers.Base
{
    public abstract class ProviderBase : ConnectionBase
    {
        public abstract DataTable GetTables();
        public abstract DataTable GetColumns(string TableName);
        public abstract DataTable GetForeingKey(string TableName, string ColumnName);
        public abstract DataTable GetKey(string TableName, string ColumnName);
        public abstract DataTable GetMultRelationship(string TableName);
        public abstract DataTable GetSingleRelationship(string TableName);
    }
}
