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
    public class PartTransferReceiveSearchCommand : MenuCommand
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public PartTransferReceiveSearchCommand(IUnityContainer container, IRegionManager regionManager)
            : base(container, MenuResources.PartReceiveSearchCommand)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public override void Execute(object parameter)
        {
            IRegion region = regionManager.Regions["ContentRegion"];
            string viewName = "PartTransferReceiveSearchView";
            object view = region.GetView(viewName);

            if (view == null)
            {
                var usercontrol = this.container.Resolve<PartTransferReceiveSearchView>();
                var viewModel = this.container.Resolve<PartTransferReceiveSearchVM>();
                viewModel.Header = new LocTextExtension() { Key = "DISPLAY_HEADER", Dict = "PartTransferReceiveSearchView", Assembly = "Measuring.SmartClient" };
                usercontrol.DataContext = viewModel;
                view = region.AddView(usercontrol, viewName);
            }
            region.Activate(view);
        }
    }
}