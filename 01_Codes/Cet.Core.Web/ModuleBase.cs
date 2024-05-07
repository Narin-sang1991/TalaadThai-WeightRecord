using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.IO;
using System.Reflection;
using Cet.Core;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Web.Configuration;
using System.Configuration;
using Microsoft.Practices.Prism.Modularity;


namespace Cet.Core.Web
{
    public class ModuleBase : IModule
    {
        //private const string UnityConfigurationSectionName = "moduleUnity";
        private IUnityContainer container;
        //private string moduleName;

        public ModuleBase(IUnityContainer container)
        {
            this.container = container;
            //var v = (ModuleAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof(ModuleAttribute));
            //this.moduleName = v.ModuleName;
        }

        public virtual void Initialize()
        {
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(@"~/Modules/" + moduleName);
            //if (config == null) return;

            //UnityConfigurationSection section = config.GetSection(UnityConfigurationSectionName) as UnityConfigurationSection;
            //if (section == null) return;
            //foreach (ContainerElement containerElement in section.Containers)
            //    section.Configure(container, containerElement.Name);


        }
    }
}
