﻿using Cet.Core;
using Cet.SmartClient.Client;
using DemoTaladThaiWg.Shell.Events;
using DemoTaladThaiWg.Shell.ViewModels;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Windows;
using Telerik.Windows.Controls;

namespace DemoTaladThaiWg.Shell
{
    public class BootstrapperClient : Bootstrapper
    {
        private const int MINIMUM_SPLASH_TIME = 1500; // Miliseconds
        private const int SPLASH_FADE_TIME = 500;     // Miliseconds
        private IRegionManager regionManager;

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

            regionManager = Container.Resolve<IRegionManager>();
            //AddConfigPage();
            AddWeighingPage();
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
            foreach (RadPane pane in ((DemoTaladThaiWg.Shell.MainWindow)(sender)).radPanGroupMainContent.Items)
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

        private void AutoLogin()
        {
            var shell = this.Shell as MainWindow;
            var vm = shell.DataContext as MainViewModel;
            try
            {
                vm.ProfileName = "Demo";//this.Container.Resolve<UserProfile>("current") != null ? this.Container.Resolve<UserProfile>("current").Name : string.Empty;
            }
            catch { }
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

        void AddWeighingPage()
        {
            //IRegion region = regionManager.Regions["ContentRegion"];
            //string viewName = "PartTransfer_ReceiveEditorView";
            //object view = region.GetView(viewName);

            //if (view == null)
            //{
            //    var usercontrol = Container.Resolve<PartTransfer_ReceiveEditorView>();
            //    var viewModel = Container.Resolve<PartTransfer_ReceiveEditorVM>();
            //    usercontrol.DataContext = viewModel;
            //    view = region.AddView(usercontrol, viewName);
            //}
            //region.Activate(view);
            IEventAggregator eventAggregator = this.Container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<PartTransferReceiveOpen>().Publish(new GeneralOpenPayLoad<Guid, object>() { OpenMode = OpenModeType.New, });
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
                //IFormAuthenticationService service = Container.Resolve<IFormAuthenticationService>();
                //service.Logout();
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
