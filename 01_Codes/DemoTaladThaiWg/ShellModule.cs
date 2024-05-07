using DemoTaladThaiWg.Shell.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTaladThaiWg.Shell
{
    public class ShellModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        static PartTransferReceiveOpenListener PartTransferReceiveOpenner;

        public ShellModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            PartTransferReceiveOpenner = this.container.Resolve<PartTransferReceiveOpenListener>();
            PartTransferReceiveOpenner.Initialize();

        }
    }
}