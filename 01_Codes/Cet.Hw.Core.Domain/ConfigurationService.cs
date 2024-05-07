using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Domain
{
    public class ConfigurationService : IConfiguration
    {
        public IUnityContainer Container { get; set; }
        public IConfigurationIntroductory ConfigurationRepository { get; set; }

        static private Dictionary<string, string> configurations;
        public Dictionary<string, string> Configurations
        {
            get
            {
                if (configurations == null)
                {
                    Refresh();
                }

                return configurations;
            }
        }

        public ConfigurationService(IUnityContainer container)
        {
            this.Container = container;
            this.ConfigurationRepository = this.Container.Resolve<IConfigurationIntroductory>();
        }

        /// <summary>
        /// Load configuration from DB 
        /// Use container sent from caller (this object is singleton and cannot hold a container.
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetConfiguration()
        {
            Dictionary<string, string> configData = new Dictionary<string, string>();

            var results = ConfigurationRepository.GetAll();
            foreach (var config in results)
            {
                var values = config.ConfigurationValue;
                if (!configData.ContainsKey(config.Code))
                    configData.Add(config.Code, values);
                else
                    configData[config.Code] = values;
            }
            return configData;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Refresh()
        {
            configurations = this.GetConfiguration();
        }
    }

}
