using Measurement.AppServiceContract;
using Cet.SmartClient.Client;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Measuring.SmartClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFLocalizeExtension.Extensions;
using Measuring.SmartClient.Events;

namespace Measuring.SmartClient.Commands
{
    public class ProcessPlanSearchCommand : MenuCommand
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public ProcessPlanSearchCommand(IUnityContainer container, IRegionManager regionManager)
            : base(container, MenuResources.ProcessPlanSearchCommand)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public override void Execute(object parameter)
        {
            IRegion region = regionManager.Regions["ContentRegion"];
            string viewName = "ProcessPlanImportedSearchView";
            object view = region.GetView(viewName);

            if (view == null)
            {
                var usercontrol = this.container.Resolve<ProcessPlanImportedSearchView>();
                var viewModel = this.container.Resolve<ProcessPlanImportedSearchVM>();
                viewModel.Header = new LocTextExtension() { Key = "DISPLAY_HEADER", Dict = "ProcessPlanImportedSearchView", Assembly = "Measuring.SmartClient" };
                usercontrol.DataContext = viewModel;
                view = region.AddView(usercontrol, viewName);
            }
            region.Activate(view);
        }
    }
}