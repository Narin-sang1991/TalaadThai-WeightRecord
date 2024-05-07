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

namespace Cet.Core.Web
{
    public class ServerModuleBase : IServerModule
    {
        private const string UnityConfigurationSectionName = "moduleUnity";
        private IUnityContainer container;
        private string moduleName;

        public ServerModuleBase(IUnityContainer container, string moduleName)
        {
            this.container = container;
            this.moduleName = moduleName;
        }

        public void Initialize()
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration(@"~/" + moduleName);
            UnityConfigurationSection section = config.GetSection(UnityConfigurationSectionName) as UnityConfigurationSection;

            if (section != null)
            {
                section.Configure(container);
            }
        }
    }
}
