using Microsoft.Build.Evaluation;
using Snappg.Core;
using Snappg.Providers.Base.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snappg.Creator
{
    class FileCreator
    {
        private string Path { get; set; }
        private Project Project { get; set; }

        public FileCreator()
        {
            this.Path = Configurator.Config.Entities.Path;

            if (Configurator.Config.Entities.Project != null && Configurator.Config.Entities.Project.Trim().Length > 0)
            {
                Project = new Project(Configurator.Config.Entities.Project);
            }
        }

        public void Create(ClassBase obj)
        {
            try
            {

                if (Project != null)
                {
                    if (Project.GetItems("Compile")
                        .Where(x => x.UnevaluatedInclude == System.IO.Path.Combine
                        (Path.Replace(Configurator.Config.Entities.Project.Substring(0, Configurator.Config.Entities.Project.LastIndexOf("\\")) + "\\", ""), obj.Name + ".cs")).Count() == 0)
                    {

                        Project.AddItem(
                            "Compile",
                            System.IO.Path.Combine(Path.Replace(Configurator.Config.Entities.Project.Substring(0, Configurator.Config.Entities.Project.LastIndexOf("\\")) + "\\", ""), obj.Name + ".cs")
                        );

                        Project.Save();
                    }
                }

                string filePath = string.Concat(Path, "\\" + obj.Name + ".cs");
                if (!System.IO.Directory.Exists(Path))
                {
                    System.IO.Directory.CreateDirectory(Path);
                }

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                System.IO.StreamWriter file = new System.IO.StreamWriter(filePath);
                file.WriteLine(obj.ToString());
                file.Flush();
                file.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CleanIgnoredFiles()
        {
            try
            {
                if (Configurator.Config.Entities.Ignores != null && Configurator.Config.Entities.Ignores.Length > 0)
                {
                    foreach (var ignored in Configurator.Config.Entities.Ignores)
                    {

                        if (Project != null)
                        {
                            ProjectItem item = Project.GetItems("Compile")
                                .Where(x => x.UnevaluatedInclude == System.IO.Path.Combine
                                (Path.Replace(Configurator.Config.Entities.Project.Substring(0, Configurator.Config.Entities.Project.LastIndexOf("\\"))+"\\", ""), ignored + ".cs")).FirstOrDefault();

                            if (item!=null)
                            {
                                Project.RemoveItem(item);
                            }

                            Project.Save();
                        }


                        string path = string.Concat(Path, "\\" + ignored + ".cs");
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
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
