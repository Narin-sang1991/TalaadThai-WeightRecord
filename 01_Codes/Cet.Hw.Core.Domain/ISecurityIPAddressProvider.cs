using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cet.Hw.Core.Domain
{
    public interface ISecurityIPAddressProvider
    {
        string GetIPAddress();
    }

    public class WcfSecurityIPAddressProvider : ISecurityIPAddressProvider
    {
        public string GetIPAddress()
        {
            if (OperationContext.Current == null)
                return string.Empty;

            OperationContext context = OperationContext.Current;
            MessageProperties prop = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            return endpoint.Address;
        }
    }

    public class WebSecurityIPAddressProvider : ISecurityIPAddressProvider
    {
        public string GetIPAddress()
        {
            if (HttpContext.Current == null || HttpContext.Current.Request == null)
                return string.Empty;

            var request = HttpContext.Current.Request;
            string ipList = request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {
                //return ipList.Split(',')[0];

                return ipList;
            }

            return request.ServerVariables["REMOTE_ADDR"];
        }
    }
}
