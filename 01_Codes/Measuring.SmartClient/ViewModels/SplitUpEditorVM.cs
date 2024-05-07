using Cet.SmartClient.Client;
using Measurement;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFLocalizeExtension.Extensions;

namespace Measuring.SmartClient.ViewModels
{
    public class SplitUpEditorVM : EditorVMBase<SplitUpData>, IOpenEditorVM<Guid>
    {
        public SplitUpEditorVM() : base() { }

        public SplitUpEditorVM(IUnityContainer container)
                : base(container)
        {

            ItemSearchVM = Container.Resolve<PartTransferItemSearchVM>();
            ItemSearchVM.RefreshParent += NotifyRefresh;
            WeightDocLookupCommand = new DelegateCommand(WeightDocLookup);

            TruckTypeItems = new List<TruckTypeData>();
            PrepareChildVMs();
        }

        #region Properties
        public DelegateCommand WeightDocLookupCommand { get; set; }

        private string licensePlateNo;
        public string LicensePlateNo
        {
            get { return licensePlateNo; }
            private set
            {
                licensePlateNo = value;
                OnPropertyChanged("LicensePlateNo");
            }
        }

        private string referenceNo;
        public string ReferenceNo
        {
            get { return referenceNo; }
            private set
            {
                referenceNo = value;
                OnPropertyChanged("ReferenceNo");
            }
        }

        private string memberCode;
        public string MemberCode
        {
            get { return memberCode; }
            private set
            {
                memberCode = value;
                OnPropertyChanged("MemberCode");
            }
        }

        public List<TruckTypeData> TruckTypeItems { get; set; }

        private Guid truckTypeId;
        public Guid TruckTypeId
        {
            get { return truckTypeId; }
            set
            {
                truckTypeId = value;
                if (value != null && value != Guid.Empty)
                {
                    var truckType = TruckTypeItems.Where(t => t.Id == value).FirstOrDefault();
                    TruckTare = truckType.Weight;
                }
                OnPropertyChanged("TruckTypeId");
            }
        }

        private decimal truckTare;
        public decimal TruckTare
        {
            get { return truckTare; }
            set
            {
                truckTare = value;
                OnPropertyChanged("TruckTare");
                OnPropertyChanged("TotalTare");
                OnPropertyChanged("NetWeight");
            }
        }

        private int driverQty;
        public int DriverQty
        {
            get { return driverQty; }
            set
            {
                driverQty = value;
                OnPropertyChanged("DriverQty");
                OnPropertyChanged("DriverTare");
                OnPropertyChanged("TotalTare");
                OnPropertyChanged("NetWeight");
            }
        }

        public decimal DriverTare
        {
            get
            {
                return DriverQty * 75;
            }
        }

        private decimal? containerTare;
        public decimal? ContainerTare
        {
            get { return containerTare; }
            set
            {
                containerTare = value;
                OnPropertyChanged("ContainerTare");
                OnPropertyChanged("TotalTare");
                OnPropertyChanged("NetWeight");
            }
        }

        private decimal grossWeight;
        public decimal GrossWeight
        {
            get { return grossWeight; }
            private set
            {
                grossWeight = value;
                OnPropertyChanged("GrossWeight");
                OnPropertyChanged("GrossWeightDisplay");
                OnPropertyChanged("TotalTare");
                OnPropertyChanged("NetWeight");
            }
        }
        public string GrossWeightDisplay
        {
            get
            {
                return string.Format("{0:#,##0.00} กิโลกรัม", this.GrossWeight);
            }
        }

        public decimal TotalTare
        {
            get
            {
                return TruckTare + DriverTare + (ContainerTare ?? 0);
            }
        }

        public decimal NetWeight
        {
            get
            {
                return GrossWeight - TotalTare;
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
        #endregion

        #region Method
        public void Load(Guid id)
        {
            var data = new SplitUpData();
        }

        public void RefreshHeader(OpenModeType mode)
        {
            Header = "SplitUp Editor";
        }

        public override void PrepareChildVMs()
        {
            ItemSearchVM.Header = new LocTextExtension() { Key = "DISPLAY_HEADER", Dict = "PartTransferItemSearchView", Assembly = "Measuring.SmartClient" };
            AddChildNode(ItemSearchVM);

            TruckTypeItems.Add(new TruckTypeData()
            {
                Id = Guid.NewGuid(),
                Name = "บรรทุกเล็ก(กระบะ)",
                Weight = 1100
            });
            TruckTypeItems.Add(new TruckTypeData()
            {
                Id = Guid.NewGuid(),
                Name = "บรรทุกกลาง(6ล้อ)",
                Weight = 1500
            }); TruckTypeItems.Add(new TruckTypeData()
            {
                Id = Guid.NewGuid(),
                Name = "บรรทุกใหญ่(10ล้อ)",
                Weight = 1900
            });
        }

        protected void NotifyRefresh(object sender, EventArgs args)
        {
            System.Windows.Application.Current.Dispatcher.InvokeAsync((Action)(() =>
            {
                OnPropertyChanged("TotalTare");
                OnPropertyChanged("NetWeight");
            }));
        }
        #endregion


        #region Action
        protected void WeightDocLookup()
        {
            var dialog = new MeasuringSearchLookupDilog(Container);
            var vm = dialog.DataContext as MeasuringSearchVM;
            vm.SetMeasuringCriteria(DemoTalaadThaiWg.AppServiceContract.MeasuringMoveType.Into, DemoTalaadThaiWg.AppServiceContract.MeasuringStatus.Commit);
            //if (this.referenceId != Guid.Empty)
            //    vm.SetExceptionIds(new List<Guid?>() { referenceId });
            vm.Search();
            bool? dialogResult = dialog.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value)
            {
                var measuring = (DemoTalaadThaiWg.AppServiceContract.MeasuringSearchData)dialog.SelectedItem;
                //this.referenceId = measuring.Id;
                this.ReferenceNo = measuring.No;
                this.LicensePlateNo = measuring.LicensePlateNo;
                this.GrossWeight = measuring.ToTalNetWeight;
            }
        }

        #endregion


    }


    public class TruckTypeData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
    }

}
