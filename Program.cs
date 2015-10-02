using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using Snappg.Core;

namespace KeenOrm.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            foreach (var arg in args)
            {
                switch (arg)
                {
                    case "update":
                        Application.UpdatePocos();
                        break;
                    case "clean":
                        break;
                    default:
                        break;
                }

            }

            Console.ReadKey();
           
        }

    

    }

}