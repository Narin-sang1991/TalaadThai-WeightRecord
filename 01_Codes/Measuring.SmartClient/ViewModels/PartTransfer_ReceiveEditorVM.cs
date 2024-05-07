using Cet.Core.Data;
using Measuring.SmartClient;
using Measuring.SmartClient.ViewModels;
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
using Measurement;
using Measurement.AppServiceContract;
using Measuring.SmartClient.Reports;
using Microsoft.Reporting.WinForms;
using System.Windows.Forms;
using System.IO;
using Cet.Core.Utility;

namespace Measuring.SmartClient.ViewModels
{
    public class PartTransfer_ReceiveEditorVM : TransactionIOEditorVM<MeasuringData, MeasuringMoveItemData>
    {
        public PartTransfer_ReceiveEditorVM() : base() { }

        public PartTransfer_ReceiveEditorVM(IUnityContainer container)
                : base(container)
        {
            Type = MeasuringMoveType.Records;
            RS232Input = Container.Resolve<RS232SerialPortInput>();
            RS232Input.WeightAutoSave += NotifyWeightItemChanged;
            RS232Input.WeightZero += NotifyWeightZero;
            
            RS232Output = Container.Resolve<RS232SerialPortOutput>();
            ItemSearchVM = Container.Resolve<PartTransferItemSearchVM>();
            ItemSearchVM.RefreshParent += NotifyRefresh;

            PreviewDocumentCommand = new DelegateCommand(PreviewDocument);
            ExportDocumentCommand = new DelegateCommand(ExportDocument);
            ReConnectAllPortCommand = new DelegateCommand(ReConnectAllPort);
            PrepareChildVMs();
            SaveStateColor = System.ConsoleColor.White.ToString();

            OpenLampCommand = new DelegateCommand(OpenLamp);
            CloseLampCommand = new DelegateCommand(CloseLamp);
            TestSaveItemCommand = new DelegateCommand(TestSaveItem);
        }

        #region Properties
        public DelegateCommand PreviewDocumentCommand { get; set; }
        public DelegateCommand ExportDocumentCommand { get; set; }
        public DelegateCommand ReConnectAllPortCommand { get; set; }



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
                foreach (var comportName in comportNames.Where(t => t != RS232Output.PortName).ToList())
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

        private PartTransferItemSearchVM itemSearchVM;
        public PartTransferItemSearchVM ItemSearchVM
        {
            get { return itemSearchVM; }
            set
            {
                itemSearchVM = value;
                OnPropertyChanged("ItemSearchVM");
                OnPropertyChanged("TotalNetWeight");
                OnPropertyChanged("TotalTareWeight");
                OnPropertyChanged("TotalGrossWeight");
            }
        }

        private RS232SerialPortOutput rs232Output;
        public RS232SerialPortOutput RS232Output
        {
            get { return rs232Output; }
            set
            {
                rs232Output = value;
            }
        }

        private string productName;
        public string ProductName
        {
            get { return productName; }
            set
            {
                productName = value;
                OnPropertyChanged("ProductName");
            }
        }

        private string productBarcode;
        public string ProductBarcode
        {
            get { return productBarcode; }
            set
            {
                productBarcode = value;
                OnPropertyChanged("ProductBarcode");
            }
        }

        #endregion


        #region Action
        protected void ExportDocument()
        {
            if (this.Id.HasValue)
            {
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;
                string filename;

                try
                {
                    var service = this.Container.Resolve<IMeasuringReportService>();
                    var results = service.GetMeasuringMoveItems(this.Id.Value);
                    var viewer = new PartTransferPrintForm(this, results);
                    ReportViewer _reportViewer = new ReportViewer();
                    viewer.RenderReportParams(_reportViewer);

                    byte[] bytes = _reportViewer.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                    filename = string.Format(Container.Resolve<string>("ExportFileNameFormat"), DateTime.Now.ToString(Container.Resolve<string>("FileDateTimeFormat")), "xls");
                    var fileFullPath = System.IO.Path.Combine(this.Container.Resolve<string>("ExportFilePath"), filename);
                    var result = this.Container.Resolve<FileInfoExtension>().WriteFile(bytes, fileFullPath);
                    var msg = string.Format(Cet.Hw.Core.SmartClient.Resources.Messages.MSG_ExportDataCompleted, result);
                    System.Windows.Forms.MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    viewer.Close();
                }
                catch (Exception ex)
                {
                    Cet.SmartClient.Client.ReportViewerHelper.ShowError(ex);
                }
            }
        }

        protected void PreviewDocument()
        {
            if (this.Id.HasValue)
            {
                var service = this.Container.Resolve<IMeasuringReportService>();
                var results = service.GetMeasuringMoveItems(this.Id.Value);
                var viewer = new PartTransferPrintForm(this, results);
                viewer.Show();
            }
        }

        protected override void Commit()
        {
            if (Id.HasValue)
            {
                var service = Container.Resolve<IMeasuringService>();
                service.Commit(Id.Value);
                Load(Id.Value);
            }
        }

        protected override void Cancel()
        {
            if (Id.HasValue)
            {
                var service = Container.Resolve<IMeasuringService>();
                service.Cancel(Id.Value);
                Load(Id.Value);
            }
        }

        protected override void Rollback()
        {
            if (Id.HasValue)
            {
                var service = Container.Resolve<IMeasuringService>();
                service.Rollback(Id.Value);
                Load(Id.Value);
            }
        }
        #endregion

        #region Method
        protected override string GetErrorInfo(PropertyInfo prop)
        {
            string err = string.Empty;

            if (prop.Name == "ReferenceNo" && string.IsNullOrWhiteSpace(ReferenceNo) && IsEditing)
                err = "จำเป็นต้องระบุเลขที่อ้างอิง";

            return err;
        }

        public override void RefreshHeader(OpenModeType mode)
        {
            Header = new LocTextExtension()
            {
                Key = "DISPLAY_HEADER",
                Dict = "PartTransfer_ReceiveEditorView",
                Assembly = "Measuring.SmartClient",
                FormatSegment1 = (mode == OpenModeType.New) ? Cet.SmartClient.Client.Resources.Messages.MODE_NEW : (string.IsNullOrEmpty(this.DocNo) ? Cet.SmartClient.Client.Resources.Messages.EMPTY_CODE : this.DocNo)
            };

            if (mode == OpenModeType.Ready)
            {
                SaveStateColor = System.ConsoleColor.Yellow.ToString();
                ReConnectAllPort();
            }
            else if (mode == OpenModeType.Update)
            {
                ItemSearchVM.SetParentData(this.Id.Value, this.Type);
                ItemSearchVM.SearchRefresh();
            }
            base.RefreshHeader(mode);
        }

        protected override MeasuringData LoadInternal(Guid iId)
        {
            var service = this.Container.Resolve<IMeasuringService>();
            return service.GetWithoutItem(iId);
        }

        protected override void LoadOriginalSource(MeasuringData originalSource)
        {
            base.LoadOriginalSource(originalSource);
            ItemSearchVM.SetParentData(this.Id.Value, this.Type);
        }

        protected override void SaveInternal()
        {
            var SaveDocumentData = new MeasuringData();
            SaveOriginalSource(SaveDocumentData);
            var service = this.Container.Resolve<IMeasuringService>();
            this.Id = SaveDocumentData.Id = service.Save(SaveDocumentData);
            System.Windows.Application.Current.Dispatcher.InvokeAsync((Action)(() =>
            {
                Load(SaveDocumentData.Id);
                RefreshHeader(OpenModeType.Ready);
            }));
        }

        public override void PrepareChildVMs()
        {
            ItemSearchVM.Header = new LocTextExtension() { Key = "DISPLAY_HEADER", Dict = "PartTransferItemSearchView", Assembly = "Measuring.SmartClient" };
            AddChildNode(ItemSearchVM);
        }

        protected override void TriggerItemSearchVM(decimal iWeight)
        {
            if (this.Id.HasValue)
            {
                var isSaveCompleted = false;
                try
                {
                    ItemSearchVM.SearchCriteria.MeasuringId = this.Id.Value;

                    var data = new WeightData();
                    data.NetWeight = iWeight - this.DefaultTare;
                    data.TareWeight = this.DefaultTare;
                    data.UnitPerRatio = 1;
                    data.ProductName = this.ProductName;
                    data.ProductBarcode = this.ProductBarcode;

                    isSaveCompleted = ItemSearchVM.SetWeight(data);
                    ResponseResult(isSaveCompleted);
                }
                catch (Exception ex)
                {
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(ex.ToString() + "[" + RS232Input.WeightDisplay + "]", ApplicationLogCategory.General, (int)Priority.High, (int)GeneralApplicationLogEvent.DispatcherUnhandledException, System.Diagnostics.TraceEventType.Error);
                }
                System.Windows.Application.Current.Dispatcher.InvokeAsync((Action)(() =>
                {
                    ResponseResult(isSaveCompleted);
                }));
            }
            else
                System.Windows.MessageBox.Show("กรูณาบันทึกเอกสารรับชิ้นส่วน เพื่อให้มีเลขที่การรับก่อน");
        }

        protected void ResponseResult(bool isSaveCompleted)
        {
            if (isSaveCompleted)
            {
                if (SaveStateColor != System.ConsoleColor.Green.ToString())
                    SaveStateColor = System.ConsoleColor.Green.ToString();
                RS232Output.SendCommand(RS232SaveStatus.Pass);
                NotifyPartItemChanged();
            }
            else
            {
                if (SaveStateColor != System.ConsoleColor.Red.ToString())
                    SaveStateColor = System.ConsoleColor.Red.ToString();
                Reset();
            }
        }

        protected override void NotifyPartItemChanged()
        {
            ItemSearchVM.SetParentData(this.Id.Value, this.Type);
            ItemSearchVM.SearchRefresh();
        }

        protected void NotifyWeightZero(object sender, EventArgs args)
        {
            RS232Output.SendCommand(RS232SaveStatus.Closed);
        }

        protected void NotifyWeightItemChanged(object sender, EventArgs args)
        {
            if (this.Id.HasValue)
                TriggerItemSearchVM(RS232Input.Weight);
        }

        protected void NotifyRefresh(object sender, EventArgs args)
        {
            System.Windows.Application.Current.Dispatcher.InvokeAsync((Action)(() =>
            {
                OnPropertyChanged("TotalNetWeight");
                OnPropertyChanged("TotalTareWeight");
                OnPropertyChanged("TotalGrossWeight");
                Reset();
            }));
        }

        protected void ReConnectAllPort()
        {
            RS232Input.CloseConnect();
            RS232Output.CloseConnect();

            RS232Input.ConnectPort();
            RS232Output.ConnectPort();
        }

        public async void Reset()
        {
            await Task.Delay(delayReset);
            RS232Input.ResetWeight();
            if (SaveStateColor != System.ConsoleColor.Yellow.ToString())
                SaveStateColor = System.ConsoleColor.Yellow.ToString();
            ProductBarcode = string.Empty;
        }

        public override void Dispose()
        {
            RS232Output.SendCommand(RS232SaveStatus.Closed);
            RS232Input.CloseConnect();
            RS232Output.CloseConnect();
        }
        #endregion


        #region Test

        public DelegateCommand OpenLampCommand { get; set; }
        public DelegateCommand CloseLampCommand { get; set; }
        public DelegateCommand TestSaveItemCommand { get; set; }
        

        protected void OpenLamp()
        {
            RS232Output.SendCommand(RS232SaveStatus.Pass);
        }

        protected void CloseLamp()
        {
            RS232Output.SendCommand(RS232SaveStatus.Closed);
        }

        protected void TestSaveItem()
        {
            if (this.Id.HasValue)
                TriggerItemSearchVM(999.99m);
        }
        #endregion

    }
}
