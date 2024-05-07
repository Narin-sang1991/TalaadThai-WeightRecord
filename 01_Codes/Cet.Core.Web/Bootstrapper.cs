using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Core.Web
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        const string UnityConfigurationSectionName = "unity";

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            UnityConfigurationSection section = ConfigurationManager.GetSection(UnityConfigurationSectionName) as UnityConfigurationSection;
            if (section == null) return;
            //configure container using main container (with empty name)
            section.Configure(Container);
        }
    }
}
