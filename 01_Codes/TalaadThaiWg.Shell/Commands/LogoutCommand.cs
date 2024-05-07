using Cet.Hw.Core.AppServiceContract;
using Cet.Hw.Core.SmartClient;
using Cet.Hw.Core.SmartClient.ViewModels;
using Cet.SmartClient.Client;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System.Linq;
using System.Windows;

namespace TalaadThaiWg.Shell.Commands
{
    public class LogoutCommand : MenuCommand
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public LogoutCommand(IUnityContainer container, IRegionManager regionManager)
        : base(container, MenuResources.Logout)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public override void Execute(object parameter)
        {
            Logout();
        }

        private void Logout()
        {
            if (MessageBox.Show(Cet.Hw.Core.SmartClient.Resources.Messages.MSG_AREYOUSURE, Cet.SmartClient.Client.Resources.Messages.MSG_CONFIRM_CAPTION, MessageBoxButton.YesNo, MessageBoxImage.Question)
                    != MessageBoxResult.Yes) return;

            IRegion region = regionManager.Regions["ContentRegion"];
            int countViews = region.Views.Count();
            IViewsCollection views = region.Views;
            for (int i = 0; i < countViews; i++)
            {
                region.Remove(views.First());
            }
            try
            {
                IFormAuthenticationService service = container.Resolve<IFormAuthenticationService>();
                service.Logout();
            }
            catch { }
            LoginView loginView = this.container.Resolve<LoginView>();
            LoginVM vm = this.container.Resolve<LoginVM>();
            vm.IsLoadedMenu = true;
            loginView.DataContext = vm;
            loginView.ShowDialog();
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
