using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snappg.Providers.Base.Objects
{
    public enum AttributeType
    {
        ForeingKey,
        PrimaryKey,
        None
    }

    public class AttributeBase
    {
        public string Table { get; set; }
        public string Column { get; set; }
        public string Attribute { get; set; }
    }

}
