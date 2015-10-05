using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snappg.Providers.Base.Objects
{
    public class ClassBase
    {
        public string Namespace { get; set; }
        public string Name { get; set; }
        public string Attributes { get; set; }
        public List<PropertyBase> Properties { get; set; }

        public ClassBase()
        {
            this.Properties = new List<PropertyBase>();
        }

    }
}
