using TalaadThaiWg.Shell.ViewModels;
using Cet.Hw.Core.AppServiceContract;
using Cet.Hw.Core.SmartClient;
using Cet.Hw.Core.SmartClient.ViewModels;
using Cet.SmartClient.Client;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System.Linq;
using System.Windows;
using WPFLocalizeExtension.Extensions;

namespace TalaadThaiWg.Shell.Commands
{
    public class HomeCommand : MenuCommand
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public HomeCommand(IUnityContainer container, IRegionManager regionManager)
        : base(container, MenuResources.HomePage)
        {
            this.container = container;
            this.regionManager = regionManager;
        }


        public override void Execute(object parameter)
        {
            IRegion region = regionManager.Regions["ContentRegion"];
            string viewName = "HomePageView";
            object view = region.GetView(viewName);

            if (view == null)
            {
                var usercontrol = this.container.Resolve<HomePageView>();
                var viewModel = this.container.Resolve<HomePageVM>();
                viewModel.Header = new LocTextExtension() { Key = "HomePage_Title", Dict = "MainWindow", Assembly = "TalaadThaiWg.Shell" };
                usercontrol.DataContext = viewModel;
                view = region.AddView(usercontrol, viewName);
            }
            region.Activate(view);
        }

    }
}
