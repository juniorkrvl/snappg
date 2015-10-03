using Snappg.Creator;
using Snappg.Providers.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Snappg.Core
{
    public class Application
    {
        public static void UpdatePocos()
        {
            try
            {
                Configurator.LoadConfig();

                MySqlProvider provider = new MySqlProvider();
                MySqlCreator creator = new MySqlCreator(provider);

                FileCreator file = new FileCreator();
                file.CleanIgnoredFiles();
                var list = creator.GetObjects();
                foreach (var item in list)
                {
                    Console.WriteLine("Writing " + item.Name + "...");
                    file.Create(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Initialize()
        {
            try
            {
                string exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                string initPath = Environment.CurrentDirectory;

                if (File.Exists(Path.Combine(initPath, "snappg-config.json")))
                {
                    Console.WriteLine("There is already a configuration file in this repository.");
                }
                else
                {
                    if (File.Exists(Path.Combine(exePath, "Templates\\snappg-config.json")))
                    {
                        File.Copy(Path.Combine(exePath, "Templates\\snappg-config.json"), Path.Combine(initPath, "snappg-config.json"), true);
                        Console.WriteLine("Configuration file successfully created");
                    }
                    else
                    {
                        Console.WriteLine("File to create not found in the source.");
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
