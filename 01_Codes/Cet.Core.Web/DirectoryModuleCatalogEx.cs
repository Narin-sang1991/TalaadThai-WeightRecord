
using Microsoft.Practices.Prism.Modularity;
using System.IO;

namespace Cet.Core.Web
{
    public class DirectoryModuleCatalogEx : DirectoryModuleCatalog
    {
        protected override void InnerLoad()
        {
            foreach (string childDirectory in Directory.GetDirectories(ModulePath))
            {
                string path = childDirectory + "\\bin";
                if (Directory.Exists(path))
                    ModulePath = path;
                base.InnerLoad();
            }
        }
    }
}
