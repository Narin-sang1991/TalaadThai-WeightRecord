using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using Microsoft.Practices.ServiceLocation;
using Cet.Core.Logging;

namespace Cet.Core.Web
{
    public class WebApplication : HttpApplication
    {
        public static IUnityContainer WebContainer = null;

        protected virtual void Application_Start(object sender, EventArgs e)
        {
            CreateBootstrapper().Run();

            WebContainer = ServiceLocator.Current.GetInstance<IUnityContainer>().CreateChildContainer();

            UnityConfigurationSection section = ConfigurationManager.GetSection("unity") as UnityConfigurationSection;
            if (section == null) return;
            try
            {
                section.Configure(WebContainer, "Web");
            }
            catch { }
        }

        protected virtual Bootstrapper CreateBootstrapper()
        {
            return new Bootstrapper();
        }

        protected virtual void Session_Start(object sender, EventArgs e)
        {

        }

        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Items.Add("UnityContainer",
                WebContainer.CreateChildContainer());
        }

        protected virtual void Application_EndRequest(object sender, EventArgs e)
        {
            (HttpContext.Current.Items["UnityContainer"] as IUnityContainer).Dispose();
        }

        protected virtual void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected virtual void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.ToString(), ApplicationLogCategory.General, (int)Priority.High, (int)GeneralApplicationLogEvent.DispatcherUnhandledException, System.Diagnostics.TraceEventType.Error);
        }

        protected virtual void Session_End(object sender, EventArgs e)
        {

        }

        protected virtual void Application_End(object sender, EventArgs e)
        {

        }
    }
}
