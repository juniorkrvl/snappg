using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snappg.Providers.Base.Objects
{
    public class PropertyBase
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public bool Nullable { get; set; }
        public AttributeBase Attribute { get; set; }
    }
}
