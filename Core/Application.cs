using Snappg.Creator;
using Snappg.Providers.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
