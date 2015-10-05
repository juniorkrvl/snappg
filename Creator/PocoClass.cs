using Snappg.Core;
using Snappg.Providers.Base.Objects;
using Snappg.Providers.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snappg.Creator
{
    class PocoClass : ClassBase
    {
        public PocoClass()
        {
            this.MultRelationships = new List<string>();
            this.SingleRelationships = new List<string>();
        }

        public List<string> MultRelationships { get; set; }
        public List<string> SingleRelationships { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(BuildImports());

            if (this.Namespace != null && this.Namespace.Length > 0)
            {
                sb.Append(BuildNamespace(BuildClass()));
            }
            else
            {
                sb.Append(BuildClass());
            }

            return sb.ToString();
            //return base.ToString();
        }

        public string BuildImports()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                //sb.AppendLine("using System;");
                //sb.AppendLine("using System.Collections.Generic;");
                //sb.AppendLine("using System.ComponentModel.DataAnnotations;");
                //sb.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
                if (Configurator.Config.Pocos.Imports!= null && Configurator.Config.Pocos.Imports.Length>0)
                {
                    foreach (var import in Configurator.Config.Pocos.Imports)
                    {
                        if (import != "")
                        {
                            sb.AppendLine(string.Format("using {0};", import));
                        }
                    }
                }
                sb.AppendLine("");

                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string BuildNamespace(string content)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("namespace " + this.Namespace);
                sb.AppendLine("{");
                foreach (var line in content.Split(new char[] { '\n','\r' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sb.AppendLine("\t" + line);
                }
                sb.AppendLine("");
                sb.AppendLine("}");

                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string BuildClass()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                if (!this.Attributes.Trim().Equals(String.Empty))
                {
                    sb.Append(this.Attributes);
                }
                sb.AppendLine(string.Format("public class {0}", this.Name));
                sb.AppendLine("{");
                foreach (var line in BuildProperties().Split(new char[] { '\n' }, StringSplitOptions.None))
                {
                    sb.Append("\t" + line);
                }
                //sb.Append(BuildProperties());
                sb.AppendLine("");
                sb.AppendLine("}");

                return sb.ToString();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public string BuildProperties()
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                foreach (var property in this.Properties)
                {
                    if (property.Attributes != null)
                    {
                        foreach (var a in property.Attributes)
                        {
                            sb.Append(a.Attribute);
                        }
                        //sb.AppendLine(property.Attribute.ToString());
                    }
                    sb.AppendLine(string.Format("public {0} {1} {{ get; set; }}", MySqlTypes.GetType(property.Type, property.Nullable), property.Name));
                }

                sb.AppendLine("");
                int counter = 0;
                foreach (var item in this.SingleRelationships.Distinct())
                {
                    sb.AppendLine(string.Format("public {0} {0} {{ get; set; }}", item));
                }

                counter = 0;
                foreach (var item in this.MultRelationships.Distinct())
                {
                    sb.AppendLine(string.Format("public IList<{0}> {0} {{ get; set; }}", item));
                }

                // constructor for IList<Child>
                sb.AppendLine(BuildConstructor());
                // ---------------------------

                return sb.ToString();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public string BuildConstructor()
        {
            try
            {
                if (this.MultRelationships.Distinct().Count()==0)
                {
                    return "";
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("\n");
                sb.AppendLine(string.Format("public {0}()", this.Name));
                sb.AppendLine("{");
                foreach (var item in this.MultRelationships.Distinct())
                {
                    sb.AppendLine(string.Format("\t{0} = new List<{0}>();", item));
                }
                sb.AppendLine("}");

                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
