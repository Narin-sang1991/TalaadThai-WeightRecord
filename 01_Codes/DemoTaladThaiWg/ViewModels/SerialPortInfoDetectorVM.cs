using Cet.SmartClient.Client;
using DemoTalaadThaiWg.AppServiceContract;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTaladThaiWg.Shell.ViewModels
{
    public class SerialPortInfoDetectorVM : EditableContainerBase
    {
        public SerialPortInfoDetectorVM(IUnityContainer container)
        {
            Container = container;
            RS232Input = Container.Resolve<RS232SerialPortInput>();
            RS232Input.InfoChanged += InfoChanged;
            SaveStateColor = System.ConsoleColor.White.ToString();

            StartText = RS232Input.PortName;
            StartText = RS232Input.SetStartText;
            EndText = RS232Input.SetEndText;
            StartSubStr = RS232Input.SetSubStrPoint1;
            EndSubStr = RS232Input.SetSubStrPoint2;


            StartCommand = new DelegateCommand(Start);
            StopCommand = new DelegateCommand(Stop);
        }

        #region Properties
        public DelegateCommand StartCommand { get; set; }
        public DelegateCommand StopCommand { get; set; }


        private RS232SerialPortInput rs232Input;
        public RS232SerialPortInput RS232Input
        {
            get { return rs232Input; }
            set
            {
                rs232Input = value;
                OnPropertyChanged("ComportName");
                OnPropertyChanged("RS232Input");
            }
        }

        private string saveStateColor;
        public string SaveStateColor
        {
            get { return saveStateColor; }
            protected set
            {
                if (saveStateColor == value) return;

                saveStateColor = value;
                OnPropertyChanged("SaveStateColor");
            }
        }

        public IList<Tuple<int, string>> ComportNames
        {
            get
            {
                var results = new List<Tuple<int, string>>();
                var comportNames = SerialPort.GetPortNames();
                int i = 0;
                foreach (var comportName in comportNames)
                    results.Add(new Tuple<int, string>(i + 1, comportName));
                return results;
            }
        }

        public string ComportName
        {
            get { return string.Format("PORT [{0}]", (RS232Input != null && !string.IsNullOrWhiteSpace(RS232Input.PortName) ? RS232Input.PortName.ToUpper() : Container.Resolve<string>("ComportNameInput"))); }
            set
            {
                RS232Input.SetComport(value, true);
                OnPropertyChanged("ComportName");
            }
        }

        private string startText;
        public string StartText
        {
            get { return startText; }
            set
            {
                startText = value;
                OnPropertyChanged("StartText");
            }
        }

        private string endText;
        public string EndText
        {
            get { return endText; }
            set
            {
                endText = value;
                OnPropertyChanged("EndText");
            }
        }

        private int startSubStr;
        public int StartSubStr
        {
            get { return startSubStr; }
            set
            {
                startSubStr = value;
                OnPropertyChanged("StartSubStr");
            }
        }


        private int endSubStr;
        public int EndSubStr
        {
            get { return endSubStr; }
            set
            {
                endSubStr = value;
                OnPropertyChanged("EndSubStr");
            }
        }

        private String strInput;
        public String StrInput
        {
            get { return strInput; }
            private set
            {
                strInput = value;
                OnPropertyChanged("StrInput");
            }
        }
        #endregion

        protected void InfoChanged(object sender, EventArgs args)
        {
            var strLine = args as InfoData;
            StrInput = (strLine.Text + System.Environment.NewLine + StrInput);
        }

        protected void Start()
        {
            RS232Input.CloseConnect();
            StrInput = string.Empty;
            RS232Input.SetComport(RS232Input.PortName, StartText, EndText, StartSubStr, EndSubStr);
            RS232Input.ConnectPort();
        }
        protected void Stop()
        {
            RS232Input.CloseConnect();
        }

    }
}
