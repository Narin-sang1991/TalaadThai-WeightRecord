using Cet.Core.Logging;
using Cet.SmartClient.Client;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Measuring.SmartClient
{
    public class RS232SerialPortInputWgWithItemCode : RS232SerialPortControlBase
    {

        public DelegateCommand ReConnectCommand { get; set; }

        public RS232SerialPortInputWgWithItemCode() : base() { }

        public RS232SerialPortInputWgWithItemCode(IUnityContainer iContainer)
            : base(iContainer)
        {
            #region Require on shell unity.tranform.config
            var confComportName = Container.Resolve<string>("ComportNameInputWgWithItem");
            var confStartText = Container.Resolve<string>("ComportDataWgWithItemStartText");
            var confEndText = Container.Resolve<string>("ComportDataWgWithItemEndText");
            var confDataStartPoint = Container.Resolve<byte>("ComportDataWgWithItemStartPoint");
            var confDataLength = Container.Resolve<byte>("ComportDataWgWithItemLength");
            this.ItemCodeStartText = Container.Resolve<string>("ComportDataItemCodeStartText");
            this.ErrorValue = Container.Resolve<decimal>("ErrorValueReceive");

            Point1 = Container.Resolve<byte>("ComportDataItemStartPoint");
            Point2 = Container.Resolve<byte>("ComportDataItemEndPoint");

            #endregion
            ResetWeight();
            ReConnectCommand = new DelegateCommand(ConnectPort);
            SetComport(confComportName, confStartText, confEndText, confDataStartPoint, confDataLength);
            //ConnectPort();
        }

        int Point1;
        int Point2;

        public string ItemCodeStartText { get; private set; }

        public string GetDataCode01 { get; set; }

        //public event EventHandler GetDataCodeChanged;

        protected override void Read()
        {
            while (_continue)
            {
                try
                {
                    string message = serialPort.ReadLine();
                    //Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(message);
                    if (!string.IsNullOrEmpty(message))
                    {
                        string inputData = message.Replace("\r", string.Empty).Replace("\n", string.Empty);
                        Console.WriteLine(inputData);
                        //WeightDisplay = inputData;
                        if (inputData.IndexOf(SetStartText) == 0)
                        {
                            inputData = inputData.Replace(SetEndText, string.Empty);
                            WeightDisplay = inputData.Substring(SetSubStrPoint1, SetSubStrPoint2).Trim();
                            Weight = Decimal.Parse(WeightDisplay);
                            VerifyWeightChange();

                            if (inputData.StartsWith(SetStartText))
                                Weight = -1 * Weight;
                        }
                        else if (inputData.IndexOf(ItemCodeStartText) >= 0 && Weight == 0)
                        {
                            //if (inputData.EndsWith(SetEndText))
                            //{
                            //    Weight = Decimal.Parse(inputData.Substring(0, inputData.IndexOf(SetEndText)).Trim());
                            //}
                            //else
                            //{
                            GetDataCode01 = inputData.Substring(inputData.IndexOf(ItemCodeStartText) + Point1, Point2).Trim();
                            //if (!string.IsNullOrWhiteSpace(GetDataCode01))
                            //    GetDataCodeChanged(this, null);
                            //}
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.ToString(), ApplicationLogCategory.General, (int)Priority.High, (int)GeneralApplicationLogEvent.DispatcherUnhandledException, System.Diagnostics.TraceEventType.Error);
                }
            }
        }

        public override void ResetWeight()
        {
            this.Weight = 0;
            this.WeightDisplay = "0";
        }

    }
}