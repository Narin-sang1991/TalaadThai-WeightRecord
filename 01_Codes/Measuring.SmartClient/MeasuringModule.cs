using Measuring.SmartClient.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measuring.SmartClient
{
    public class MeasuringModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        static PartTransferReceiveOpenListener PartTransferReceiveOpenner;
        static ProcessPlanImportedOpenListener ProcessPlanImportedOpenner;
        static SplitUpOpenListener SplitUpOpenner;


        public MeasuringModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            PartTransferReceiveOpenner = this.container.Resolve<PartTransferReceiveOpenListener>();
            PartTransferReceiveOpenner.Initialize();

            ProcessPlanImportedOpenner = this.container.Resolve<ProcessPlanImportedOpenListener>();
            ProcessPlanImportedOpenner.Initialize();

            SplitUpOpenner = this.container.Resolve<SplitUpOpenListener>();
            SplitUpOpenner.Initialize();

        }
    }
}