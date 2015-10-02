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
    }

    public class ForeignKey : AttributeBase
    {
        public override string ToString()
        {
            return string.Format("[ForeignKey(\"{0}.{1}\")]", Table, Column);
        }
    }

    public class PrimaryKey : AttributeBase
    {
        public override string ToString()
        {
            return "[Key]";
        }
    }

    public class GenericAttribute: AttributeBase
    {

    }

}
