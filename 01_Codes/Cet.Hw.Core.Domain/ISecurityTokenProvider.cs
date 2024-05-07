using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Web;
using System.ServiceModel.Web;
using Microsoft.Practices.Unity;

namespace Cet.Hw.Core.Domain
{
    public interface ISecurityTokenProvider
    {
        string GetToken();
        void SaveToken(string token);
        void ClearToken();
    }

    public class WcfSecurityTokenProvider : ISecurityTokenProvider
    {
        string Name = "token";
        string Ns = "ns";

        protected IUnityContainer Container { get; private set; }

        public WcfSecurityTokenProvider(IUnityContainer container)
        {
            this.Container = container;

        }

        public string GetToken()
        {
            string token = string.Empty;

            try
            {
                string bindingName = OperationContext.Current.EndpointDispatcher.ChannelDispatcher.BindingName;
                if (bindingName.Contains("WebHttpBinding"))
                    token = WebOperationContext.Current.IncomingRequest.Headers["token"];
                else
                {
                    if (OperationContext.Current.IncomingMessageHeaders.FindHeader(Name, Ns) != -1)
                        token = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>(Name, Ns);
                }
            }
            catch
            {
            }
            return token;
        }

        public void SaveToken(string token)
        {
            try
            {
                //string bindingName = OperationContext.Current.EndpointDispatcher.ChannelDispatcher.BindingName;
                //if (bindingName.Contains("WebHttpBinding"))
                //    WebOperationContext.Current.IncomingRequest.Headers["token"] = token;
            }
            catch
            {
            }
        }

        public void ClearToken()
        {
        }
    }

    public class WebSecurityTokenProvider : ISecurityTokenProvider
    {
        public string GetToken()
        {
            string token = string.Empty;
            try
            {
                token = HttpContext.Current.Session["token"].ToString();
            }
            catch
            { }
            return token;
        }

        public void SaveToken(string token)
        {
            HttpContext.Current.Session["token"] = token;
        }

        public void ClearToken()
        {
            HttpContext.Current.Session.Remove("token");
        }
    }

    public class WebApiSecurityTokenProvider : ISecurityTokenProvider
    {
        public string GetToken()
        {
            string token = string.Empty;
            try
            {
                token = HttpContext.Current.Request.Headers["token"] ?? "";
            }
            catch
            { }
            return token;
        }

        public void SaveToken(string token)
        {

        }

        public void ClearToken()
        {

        }
    }

    public class StandAloneSecurityTokenProvider : ISecurityTokenProvider
    {
        public static string Token;
        public string GetToken()
        {
            string token = string.Empty;
            try
            {
                token = Token;
            }
            catch
            { }
            return token;
        }

        public void SaveToken(string token)
        {
            Token = token;
        }

        public void ClearToken()
        {
            Token = null;
        }
    }

}
