using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snappg.Core
{
    public class Configurator
    {

        private static string ApplicationFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        private static string CurrentFolder = Environment.CurrentDirectory;
        private static string ConfigPath { get; set; }

        public static Config Config = new Config();

        public static void LoadConfig()
        {
            try
            {
                ConfigPath = System.IO.Path.Combine(CurrentFolder, "snappg-config.json");

                if (!File.Exists(ConfigPath))
                {
                    throw new ApplicationException("snappg-config.json file not found!");
                }

                using (StreamReader r = new StreamReader(ConfigPath))
                {
                    string json = r.ReadToEnd();
                    Config = JsonConvert.DeserializeObject<Config>(json);
                    ValidateConfig();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void ValidateConfig()
        {
            try
            {
                if (Config == null)
                {
                    throw new ApplicationException("Config file is null!");
                }

                if (Config.Database == null)
                {
                    throw new ApplicationException("Error in Database section. Please verify how to configure in docs.");
                }
                else
                {
                    if (Config.Database.Name == null | Config.Database.Name == "")
                    {
                        throw new ApplicationException("Error in Database.Name section. Please insert a database name.");
                    }
                    if (Config.Database.Password == null | Config.Database.Password == "")
                    {
                        throw new ApplicationException("Error in Database.Password section. Please insert a database password.");
                    }
                    if (Config.Database.Provider == null | Config.Database.Provider == "")
                    {
                        throw new ApplicationException("Error in Database.Provider section. Please insert a database provider.");
                    }
                    if (Config.Database.Server == null | Config.Database.Server == "")
                    {
                        throw new ApplicationException("Error in Database.Server section. Please insert a database server.");
                    }
                    if (Config.Database.User == null | Config.Database.User == "")
                    {
                        throw new ApplicationException("Error in Database.User section. Please insert a database user.");
                    }
                }

                if (Config.Entities == null)
                {
                    throw new ApplicationException("Error in Entities section. Please verify how to configure in docs.");
                }
                else
                {
                    if (Config.Entities.Path.Equals(string.Empty))
                    {
                        throw new ApplicationException("Error in Entities.Path section. Output folder for generated files is missing.");
                    }
                    if (Config.Entities.Project.Trim().Length > 0 && !Config.Entities.Project.EndsWith(".csproj"))
                    {
                        throw new ApplicationException("Error in Entities.Project section. File must be a porject file (.csproj,.vbproj).");
                    }
                }

                if (Config.Pocos == null)
                {
                    throw new ApplicationException("Error in Pocos section. Please verify how to configure in docs.");
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
