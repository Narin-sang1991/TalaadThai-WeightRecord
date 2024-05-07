using Cet.Core.Data;
using DemoTaladThaiWg.Shell.ViewModels;
using Cet.SmartClient.Client;
using Microsoft.Practices.Unity;
using System;
using System.Threading.Tasks;
using System.Windows;
using WPFLocalizeExtension.Extensions;
using System.Linq;
using System.IO.Ports;
using System.Collections.Generic;
using Cet.Core.Logging;
using System.Reflection;
using Microsoft.Reporting.WinForms;
using System.Windows.Forms;
using System.IO;
using Cet.Core.Utility;
using DemoTalaadThaiWg.AppServiceContract;
using DemoTalaadThai.AppService;
using Microsoft.Practices.Prism.Events;
using DemoTaladThaiWg.Shell.Events;
using IronOcr;

namespace DemoTaladThaiWg.Shell.ViewModels
{
    public class PartTransfer_ReceiveEditorVM : TransactionIOEditorVM<MeasuringData, MeasuringMoveItemData>
    {
        public PartTransfer_ReceiveEditorVM() : base() { }

        public PartTransfer_ReceiveEditorVM(IUnityContainer container)
                : base(container)
        {
            Type = MeasuringMoveType.Into;
            RS232Input = Container.Resolve<RS232SerialPortInput>();
            RS232Input.WeightAutoSave += NotifyWeightItemChanged;
            RS232Input.WeightZero += NotifyWeightZero;

            //RS232Output = Container.Resolve<RS232SerialPortOutput>();

            SearchVM = Container.Resolve<PartTransferReceiveSearchVM>();

            ReConnectAllPortCommand = new DelegateCommand(ReConnectAllPort);
            Sync2ExternalCommand = new DelegateCommand(Sync2External);
            ReferenceSelectorCommand = new DelegateCommand(ReferenceSelector);
            NewWeighCommand = new DelegateCommand(NewWeighDocument);
            SelectImageCommand = new DelegateCommand(SelectImage);
            PrepareChildVMs();

            SaveStateColor = System.ConsoleColor.White.ToString();
            OpenLampCommand = new DelegateCommand(OpenLamp);
            CloseLampCommand = new DelegateCommand(CloseLamp);
            TestSaveItemCommand = new DelegateCommand(TestSaveItem);
        }

        #region Properties
        [Dependency("DefaultUnitCode")]
        public string DefaultUnitCode { get; set; }
        public DelegateCommand ReConnectAllPortCommand { get; set; }
        public DelegateCommand Sync2ExternalCommand { get; set; }
        public DelegateCommand ReferenceSelectorCommand { get; set; }
        public DelegateCommand NewWeighCommand { get; set; }
        public DelegateCommand SelectImageCommand { get; set; }


        private PartTransferReceiveSearchVM searchVM;
        public PartTransferReceiveSearchVM SearchVM
        {
            get { return searchVM; }
            set
            {
                searchVM = value;
                OnPropertyChanged("SearchVM");
            }
        }

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

        public IList<Tuple<int, string>> ComportNames
        {
            get
            {
                var results = new List<Tuple<int, string>>();
                var comportNames = SerialPort.GetPortNames();
                int i = 0;
                //foreach (var comportName in comportNames.Where(t => t != RS232Output.PortName).ToList())
                foreach (var comportName in comportNames)
                    results.Add(new Tuple<int, string>(i + 1, comportName));
                return results;
            }
        }

        public string ComportName
        {
            get { return (RS232Input != null ? RS232Input.PortName.ToUpper() : Container.Resolve<string>("ComportNameInput")); }
            set
            {
                RS232Input.SetComport(value, (IsNotEditing && Id.HasValue));
                ReConnectAllPort();
                OnPropertyChanged("ComportName");
            }
        }


        //private RS232SerialPortOutput rs232Output;
        //public RS232SerialPortOutput RS232Output
        //{
        //    get { return rs232Output; }
        //    set
        //    {
        //        rs232Output = value;
        //    }
        //}
        #endregion


        #region Action
        protected void Sync2External()
        {
            this.Commit();
        }

        protected void ReferenceSelector()
        {
            var dialog = new MeasuringSearchLookupDilog(Container);
            var vm = dialog.DataContext as MeasuringSearchVM;
            vm.SetMeasuringType(MeasuringMoveType.Into);
            if (this.referenceId != Guid.Empty)
                vm.SetExceptionIds(new List<Guid?>() { referenceId });
            vm.Search();
            bool? dialogResult = dialog.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value)
            {
                var measuring = (MeasuringSearchData)dialog.SelectedItem;
                this.referenceId = measuring.Id;
                this.ReferenceNo = measuring.No;
                this.LicensePlateNo = measuring.LicensePlateNo;
            }
        }

        protected void NewWeighDocument()
        {
            IEventAggregator eventAggregator = this.Container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<PartTransferReceiveOpen>().Publish(new GeneralOpenPayLoad<Guid, object>() { OpenMode = OpenModeType.New, });
        }


        protected void SelectImage()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            var ex = Container.Resolve<string>("ExtensionContractFile");
            openFileDialog.Filter = string.Format("({0})|{0}", ex);

            var dialogResult = openFileDialog.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value)
            {
                IronOcr.OcrResult results = OCRExtension.RaedTextFromFile(openFileDialog.FileName);
                this.LicensePlateNo = results.Text;
            }
        }

        protected override void Commit()
        {
            if (Id.HasValue)
            {
                var service = Container.Resolve<MeasuringService>();
                service.Commit(Id.Value);
                Load(Id.Value);
            }
        }

        protected override void Cancel()
        {
            if (Id.HasValue)
            {
                var service = Container.Resolve<MeasuringService>();
                service.Cancel(Id.Value);
                Load(Id.Value);
            }
        }

        protected override void Rollback()
        {
            if (Id.HasValue)
            {
                var service = Container.Resolve<MeasuringService>();
                service.Rollback(Id.Value);
                Load(Id.Value);
            }
        }
        #endregion

        #region Method
        public override void PrepareChildVMs()
        {
            SearchVM.Header = new LocTextExtension() { Key = "DISPLAY_HEADER", Dict = "PartTransferReceiveSearchView", Assembly = "DemoTaladThaiWg.Shell" };
            AddChildNode(SearchVM);
        }


        protected override string GetErrorInfo(PropertyInfo prop)
        {
            string err = string.Empty;

            if (prop.Name == "LicensePlateNo" && string.IsNullOrWhiteSpace(LicensePlateNo) && IsEditing)
                err = "จำเป็นต้องระบุหมายเลขทะเบียนรถ";

            return err;
        }

        public override void RefreshHeader(OpenModeType mode)
        {
            Header = new LocTextExtension()
            {
                Key = "DISPLAY_HEADER",
                Dict = "PartTransfer_ReceiveEditorView",
                Assembly = "DemoTaladThaiWg.Shell",
                FormatSegment1 = (mode == OpenModeType.New) ? Cet.SmartClient.Client.Resources.Messages.MODE_NEW : (string.IsNullOrEmpty(this.DocNo) ? Cet.SmartClient.Client.Resources.Messages.EMPTY_CODE : this.DocNo)
            };

            if (mode == OpenModeType.Ready)
            {
                SaveStateColor = System.ConsoleColor.Yellow.ToString();
                ReConnectAllPort();
            }

            base.RefreshHeader(mode);
        }

        protected override MeasuringData LoadInternal(Guid iId)
        {
            var service = this.Container.Resolve<MeasuringService>();
            return service.GetWithoutItem(iId);
        }


        protected override void SaveInternal()
        {
            var SaveDocumentData = new MeasuringData();
            SaveOriginalSource(SaveDocumentData);
            var service = this.Container.Resolve<MeasuringService>();
            this.Id = SaveDocumentData.Id = service.Save(SaveDocumentData);
            System.Windows.Application.Current.Dispatcher.InvokeAsync((Action)(() =>
            {
                Load(SaveDocumentData.Id);
                RefreshHeader(OpenModeType.Ready);
                SearchVM.Search();
            }));
        }

        protected override void SaveOriginalSource(MeasuringData originalSource)
        {
            base.SaveOriginalSource(originalSource);

            var items = originalSource.MeasuringMoveItems;
            items.Add(new MeasuringMoveItemData()
            {
                SeqNo = originalSource.MeasuringMoveItems.Count() + 1,
                NetWeight = this.RS232Input.Weight,
                WeightUnitCode = this.DefaultUnitCode,

            });
            originalSource.MeasuringMoveItems = items;
        }

        protected void ResponseResult(bool isSaveCompleted)
        {
            if (isSaveCompleted)
            {
                if (SaveStateColor != System.ConsoleColor.Green.ToString())
                    SaveStateColor = System.ConsoleColor.Green.ToString();
                //RS232Output.SendCommand(RS232SaveStatus.Pass);
                NotifyPartItemChanged();
            }
            else
            {
                if (SaveStateColor != System.ConsoleColor.Red.ToString())
                    SaveStateColor = System.ConsoleColor.Red.ToString();
                Reset();
            }
        }

        protected void NotifyWeightZero(object sender, EventArgs args)
        {
            //RS232Output.SendCommand(RS232SaveStatus.Closed);
        }

        protected void NotifyWeightItemChanged(object sender, EventArgs args)
        {
            if (this.Id.HasValue)
                TriggerItemSearchVM(RS232Input.Weight);
        }

        protected void ReConnectAllPort()
        {
            RS232Input.CloseConnect();
            //RS232Output.CloseConnect();

            RS232Input.ConnectPort();
            //RS232Output.ConnectPort();
        }

        public async void Reset()
        {
            await Task.Delay(delayReset);
            RS232Input.ResetWeight();
            if (SaveStateColor != System.ConsoleColor.Yellow.ToString())
                SaveStateColor = System.ConsoleColor.Yellow.ToString();
            LicensePlateNo = string.Empty;
            ReferenceNo = string.Empty;
        }

        public override void Dispose()
        {
            //RS232Output.SendCommand(RS232SaveStatus.Closed);
            RS232Input.CloseConnect();
            //RS232Output.CloseConnect();
        }
        #endregion


        #region Test

        public DelegateCommand OpenLampCommand { get; set; }
        public DelegateCommand CloseLampCommand { get; set; }
        public DelegateCommand TestSaveItemCommand { get; set; }


        protected void OpenLamp()
        {
            //RS232Output.SendCommand(RS232SaveStatus.Pass);
        }

        protected void CloseLamp()
        {
            //RS232Output.SendCommand(RS232SaveStatus.Closed);
        }

        protected void TestSaveItem()
        {
            if (this.Id.HasValue)
                TriggerItemSearchVM(999.99m);
        }
        #endregion

    }
}
