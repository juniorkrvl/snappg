using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using Snappg.Core;
using System.Text;

namespace Snappg.Run
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Snappg Commands:");
                Console.WriteLine("Init -> For create a config file in current directory.");
                Console.WriteLine("Update -> For create/update your POCO files.");
                Console.WriteLine("About -> View author and documentation site.");
                Console.WriteLine("Enjoy :)");
            }
            else
            {
                foreach (var arg in args)
                {
                    switch (arg)
                    {
                        case "init":
                            Application.Initialize();
                            break;
                        case "update":
                            Application.UpdatePocos();
                            break;
                        case "about":
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("Author: Junior Carvalho");
                            sb.AppendLine("Site: juniorkrvl.github.io/snappg");
                            Console.WriteLine(sb.ToString());
                            break;
                        default:
                            Console.WriteLine("Unrecognized command.");
                            break;
                    }

                }
            }

#if DEBUG
            Console.ReadKey();
#endif

        }



    }

}