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
    public class WgRecordsGroupingReportCommand : MenuCommand
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public WgRecordsGroupingReportCommand(IUnityContainer container, IRegionManager regionManager)
            : base(container, MenuResources.WgRecordsGroupingReportCommand)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public override void Execute(object parameter)
        {
            IRegion region = regionManager.Regions["ContentRegion"];
            string viewName = "PartMoveItemGroupingReportView";
            object view = region.GetView(viewName);

            if (view == null)
            {
                var usercontrol = this.container.Resolve<PartMoveItemGroupingReportView>();
                var viewModel = this.container.Resolve<PartMoveItemGroupingReportVM>();
                viewModel.Header = new LocTextExtension() { Key = "DISPLAY_HEADER", Dict = "PartMoveItemGroupingReportView", Assembly = "Measuring.SmartClient" };
                usercontrol.DataContext = viewModel;
                view = region.AddView(usercontrol, viewName);
            }
            region.Activate(view);
        }
    }
}