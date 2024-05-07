using Cet.SmartClient.Client;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measuring.SmartClient
{
    public class RS232SerialPortOutput : RS232SerialPortControlBase
    {

        public RS232SerialPortOutput() : base() { }

        public RS232SerialPortOutput(IUnityContainer iContainer)
            : base(iContainer)
        {
            #region Require on shell unity.tranform.config
            var confComportName = Container.Resolve<string>("ComportNameOutput");
            #endregion

            SetComport(confComportName, null, null, 0, 0);
            //ConnectPort();
        }



    }
}
