using Cet.SmartClient.Client;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using TalaadThaiWg.Shell.ViewModels;
using Telerik.Windows.Controls;
using System.ServiceModel;
using Cet.Core;
using Microsoft.Practices.Unity.Configuration;
using Cet.Hw.Core.SmartClient;
using Cet.Hw.Core.SmartClient.ViewModels;
using System.Data.Linq;
using Cet.Hw.Core.AppServiceContract;
using Cet.Hw.Core;
using WPFLocalizeExtension.Extensions;
using Measuring.SmartClient;
using Measuring.SmartClient.IOModels;

namespace TalaadThaiWg.Shell
{
    public class BootstrapperClient : Bootstrapper
    {
        private const int MINIMUM_SPLASH_TIME = 1500; // Miliseconds
        private const int SPLASH_FADE_TIME = 500;     // Miliseconds
        private IRegionManager regionManager;

        static DocumentOpenListener documentOpenListener;

        protected override DependencyObject CreateShell()
        {
            //check first application run
            if (!ExistingDatabase())
                Application.Current.Shutdown();

            // Step 1 - Load the splash screen
            SplashScreen splash = new SplashScreen("Images/LOGO.png");
            splash.Show(false);


            // Step 2 - Start a stop watch
            Stopwatch timer = new Stopwatch();
            timer.Start();

            //// Step 3 - Load your windows but don't show it yet
            MainWindow shell = this.Container.TryResolve<MainWindow>();
            shell.DataContext = this.Container.TryResolve<MainViewModel>();
            //((MainViewModel)shell.DataContext).InitializeApplicationLang();
            //((MainViewModel)shell.DataContext).GetImage();
            shell.Title = this.Container.Resolve<string>("AppTitle");

            // Step 4 - Make sure that the splash screen lasts at least two seconds
            timer.Stop();
            int remainingTimeToShowSplash = MINIMUM_SPLASH_TIME - (int)timer.ElapsedMilliseconds;
            if (remainingTimeToShowSplash > 0)
                Thread.Sleep(remainingTimeToShowSplash);

            // Step 5 - show the page
            splash.Close(TimeSpan.FromMilliseconds(SPLASH_FADE_TIME));
            shell.Closing += new System.ComponentModel.CancelEventHandler(shell_Closing);
            return shell;
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();
            AutoLogin();

            documentOpenListener = this.Container.Resolve<DocumentOpenListener>();
            documentOpenListener.Initialize();
            regionManager = Container.Resolve<IRegionManager>();
            AddHomePage();
            //AddConfigPage();
        }

        protected override void app_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception is FaultException<UserNotLoginException>)
            {
                e.Handled = true;
                Logout();
            }
            else
                base.app_DispatcherUnhandledException(sender, e);
        }

        private void shell_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (RadPane pane in ((TalaadThaiWg.Shell.MainWindow)(sender)).radPanGroupMainContent.Items)
            {
                EditableContainerBase ecb = pane.DataContext as EditableContainerBase;
                if (ecb != null)
                    ecb.Dispose();
            }

            IRegionManager regionManager = this.Container.Resolve<IRegionManager>();
            IRegion region = regionManager.Regions["ContentRegion"];
            int countViews = region.Views.Count();
            IViewsCollection views = region.Views;
            for (int i = 0; i < countViews; i++)
            {
                region.Remove(views.First());
            }
        }


        private void DefaultLogin()
        {
            var loginVM = Container.Resolve<LoginVM>();
            loginVM.LoginName = "admin";
            loginVM.PasswordInput = "p@ssw0rd1";
            loginVM.CheckLogin();
        }

        private void AutoLogin()
        {
            bool loginSuccess = false;
            LoginVM loginVM = this.Container.Resolve<LoginVM>();

            //if (!loginSuccess)
            //{
            //    LoginView loginView = this.Container.Resolve<LoginView>();
            //    loginView.DataContext = loginVM;
            //    loginSuccess = (bool)loginView.ShowDialog();
            //}
            var shell = this.Shell as MainWindow;
            var vm = shell.DataContext as MainViewModel;
            try
            {
                vm.ProfileName = "Demo";//this.Container.Resolve<UserProfile>("current") != null ? this.Container.Resolve<UserProfile>("current").Name : string.Empty;
            }
            catch { }
        }

        void AddHomePage()
        {
            IRegion region = regionManager.Regions["ContentRegion"];
            string viewName = "HomePageView";
            object view = region.GetView(viewName);

            if (view == null)
            {
                var usercontrol = Container.Resolve<HomePageView>();
                var viewModel = Container.Resolve<HomePageVM>();
                viewModel.Header = new LocTextExtension() { Key = "HomePage_Title", Dict = "MainWindow", Assembly = "TalaadThaiWg.Shell" };
                usercontrol.DataContext = viewModel;
                view = region.AddView(usercontrol, viewName);
                viewModel.LoadMenu();
            }
            region.Activate(view);
        }

        void AddConfigPage()
        {
            IRegion region = regionManager.Regions["ContentRegion"];
            string viewName = "SerialPortInfoDetectorView";
            object view = region.GetView(viewName);

            if (view == null)
            {
                var usercontrol = Container.Resolve<SerialPortInfoDetectorView>();
                var viewModel = Container.Resolve<SerialPortInfoDetectorVM>();
                viewModel.Header = "SerialPort Setting";
                usercontrol.DataContext = viewModel;
                view = region.AddView(usercontrol, viewName);
            }
            region.Activate(view);
        }

        private void Logout()
        {
            shell_Closing(this, null);
            AutoLogin();
        }


        protected override void app_Exit(object sender, ExitEventArgs e)
        {
            try
            {
                IFormAuthenticationService service = Container.Resolve<IFormAuthenticationService>();
                service.Logout();
                Container.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                base.app_Exit(sender, e);
            }
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            var mappings = base.ConfigureRegionAdapterMappings();

            mappings.RegisterMapping(typeof(RadPaneGroup), this.Container.Resolve<RadPaneGroupRegionAdapter>());

            return mappings;
        }

        public bool ExistingDatabase()
        {
            return true;
        }

    }
}
