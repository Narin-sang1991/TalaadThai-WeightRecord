using Cet.Core;
using Cet.Hw.Core.SmartClient;
using Cet.Hw.Core.SmartClient.ViewModels;
using Cet.SmartClient.Client;
using Microsoft.Practices.Unity;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Measurement;
using Cet.Hw.Core.AppServiceContract;
using Measurement.AppServiceContract;
using System.Windows.Forms;

namespace Measuring.SmartClient.ViewModels
{
    public class PartTransferItemEditorVM : EditorVMBase<MeasuringMoveItemData>
    {

        public PartTransferItemEditorVM()
            : base()
        {

        }
        public PartTransferItemEditorVM(IUnityContainer container)
            : base(container)
        {
            ProcessPlanAutoComplete = this.Container.Resolve<ProcessPlanSearchOnlyVM>();
        }

        #region Properties
        public event EventHandler RefreshParentItem;

        private ProcessPlanSearchOnlyVM processPlanAutoComplete;
        public ProcessPlanSearchOnlyVM ProcessPlanAutoComplete
        {
            get { return processPlanAutoComplete; }
            set
            {
                processPlanAutoComplete = value;
                OnPropertyChanged("ProcessPlanAutoComplete");
            }
        }

        private ProcessPlanData selectedProcessPlan;
        public ProcessPlanData SelectedProcessPlan
        {
            get { return selectedProcessPlan; }
            set
            {
                selectedProcessPlan = value;

                if (value != null)
                {
                    Poke(() => ProductBarcode = value.CoilNo);
                    ProductName = value.CoilNo;
                    ProcessPlanId = value.Id;
                    CoilNetWeight = value.CoilWeight;
                    Remark = string.Format("[{0}], {1}, {2}", value.PosNo, value.ProcessMachineCode, value.ProcessPlanDate.ToString("yyyy/MM/dd"));
                }
                else
                {
                    Poke(() => ProductBarcode = string.Empty);
                    ProductName = Remark = string.Empty;
                    ProcessPlanId = default(Guid?);
                    CoilNetWeight = default(decimal?);
                }
                OnPropertyChanged("SelectedProcessPlan");
            }
        }

        public string DiffStatusColor
        {
            get
            {
                decimal precision = Container.Resolve<decimal>("WeightPrecisionLimit");
                return CoilNetWeight.HasValue
                    ? (DiffWeight < (precision * -1) ? "Red" : "Green")
                    : "Transparent";
            }

        }

        private Guid id;
        public Guid Id
        {
            get { return id; }
            private set { id = value; }
        }

        private Guid measuringMovementId;
        public Guid MeasuringMovementId
        {
            get { return measuringMovementId; }
            set { measuringMovementId = value; }
        }

        private Guid? processPlanId;
        public Guid? ProcessPlanId
        {
            get { return processPlanId; }
            private set { processPlanId = value; }
        }

        private GatewayItemType itemType;
        public GatewayItemType ItemType
        {
            get { return itemType; }
            set
            {
                itemType = value;
                OnPropertyChanged("ItemTypeDisplay");
                OnPropertyChanged("IsMachine");
                OnPropertyChanged("ItemStatusColor");
            }
        }

        public bool IsMachine
        {
            get { return ItemType == GatewayItemType.Machine; }
        }

        private long seqNo;
        public long SeqNo
        {
            get { return seqNo; }
            set
            {
                seqNo = value;
                OnPropertyChanged("SeqNo");
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
                var text = string.IsNullOrWhiteSpace(value)
                            ? string.Empty
                            : value.Replace(Environment.NewLine, string.Empty);
                productBarcode = text;
                if (!IsPoked && text.Length >= ProcessPlanAutoComplete.MinimumLengthAutoTextSearch)
                    Poke(() =>
                    {
                        ProcessPlanAutoComplete.SearchCriteria.Barcode = text;
                        processPlanAutoComplete.SearchCriteria.NotUsed = true;
                        ProcessPlanAutoComplete.Search();
                        if (ProcessPlanAutoComplete.SearchResult.Count() > 0)
                            SelectedProcessPlan = ProcessPlanAutoComplete.SearchResult.FirstOrDefault();
                        else
                        {
                            productBarcode = string.Empty;
                            MessageBox.Show(Measuring.SmartClient.Resources.PartTransferItemSearchView.NotFound_ProcessSheet,
                                                "Information",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);
                        }
                    });

                OnPropertyChanged("ProductBarcode");
            }
        }

        private decimal? coilNetWeight;

        public decimal? CoilNetWeight
        {
            get { return coilNetWeight; }
            private set
            {
                coilNetWeight = value;
                OnPropertyChanged("CoilNetWeight");
                OnPropertyChanged("DiffWeight");
                OnPropertyChanged("DiffStatusColor");
            }
        }

        public decimal? DiffWeight
        {
            get
            {
                return CoilNetWeight.HasValue
                    ? NetWeight - CoilNetWeight.Value
                    : default(decimal?);
            }
        }

        private decimal unitPerRatio;
        public decimal UnitPerRatio
        {
            get { return unitPerRatio; }
            set
            {
                unitPerRatio = value;
                OnPropertyChanged("UnitPerRatio");
            }
        }

        private decimal netWeight;
        public decimal NetWeight
        {
            get { return netWeight; }
            private set
            {
                netWeight = value;
                OnPropertyChanged("NetWeight");
            }
        }

        private decimal tareWeight;
        public decimal TareWeight
        {
            get { return tareWeight; }
            set
            {
                tareWeight = value;
                if (!IsPoked)
                {
                    NetWeight = GrossWeight - TareWeight;
                }
                OnPropertyChanged("TareWeight");
            }
        }

        private decimal grossWeight;
        public decimal GrossWeight
        {
            get { return grossWeight; }
            set
            {
                grossWeight = value;
                if (!IsPoked)
                {
                    NetWeight = GrossWeight - TareWeight;
                }
                OnPropertyChanged("GrossWeight");
            }

        }

        public Guid WeightUnitId { get; private set; }
        private string weightUnitCode;
        public string WeightUnitCode
        {
            get { return weightUnitCode; }
            private set
            {
                weightUnitCode = value;
                OnPropertyChanged("WeightUnitCode");
            }
        }

        private decimal? unitPrice;
        public decimal? UnitPrice
        {
            get { return unitPrice; }
            set
            {
                unitPrice = value;
                OnPropertyChanged("UnitPrice");
            }
        }

        public string ItemStatusColor
        {
            get
            {
                return (string.IsNullOrWhiteSpace(ProductBarcode) ? "Yellow" : "Transparent");
            }

        }

        public string ItemTypeDisplay
        {
            get
            {
                return ItemType.Translate();
            }

        }

        private String remark;
        public String Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        public bool UpdateNameWithSameBarcode { get; set; }
        #endregion Properties

        protected override void SaveInternal()
        {
            var SaveDocumentData = new MeasuringMoveItemData();
            SaveOriginalSource(SaveDocumentData);
            SaveMovementItemInternal(SaveDocumentData);
            RefreshParentItem(this, null);
        }


        protected override void SaveOriginalSource(MeasuringMoveItemData originalSource)
        {
            originalSource.Id = this.Id;
            originalSource.MeasuringId = this.MeasuringMovementId;
            originalSource.ProcessPlanId = this.ProcessPlanId;
            originalSource.SeqNo = this.SeqNo;
            originalSource.ProductName = this.ProductName;
            originalSource.ProductBarcode = this.ProductBarcode;
            originalSource.CoilNetWeight = this.CoilNetWeight;
            originalSource.NetWeight = this.NetWeight;
            originalSource.TareWeight = this.TareWeight;
            originalSource.WeightUnitId = this.WeightUnitId;
            originalSource.UnitPrice = this.UnitPrice.HasValue ? this.UnitPrice.Value : 0m;
            originalSource.UnitPerRatio = this.UnitPerRatio;
            originalSource.UpdateNameWithSameBarcode = this.UpdateNameWithSameBarcode;
            originalSource.GatewayItemType = this.ItemType;
            originalSource.Remark = this.Remark;
        }

        protected override void LoadOriginalSource(MeasuringMoveItemData originalSource)
        {
            this.Id = originalSource.Id;
            this.MeasuringMovementId = originalSource.MeasuringId;
            this.ProcessPlanId = originalSource.ProcessPlanId;
            this.SeqNo = originalSource.SeqNo;
            this.ProductName = originalSource.ProductName;
            Poke(() => this.ProductBarcode = originalSource.ProductBarcode);
            this.CoilNetWeight = originalSource.CoilNetWeight;
            this.UnitPerRatio = originalSource.UnitPerRatio;
            this.WeightUnitCode = originalSource.WeightUnitCode;
            this.UnitPrice = originalSource.UnitPrice;
            this.NetWeight = originalSource.NetWeight;
            Poke(() => this.TareWeight = originalSource.TareWeight);
            Poke(() => this.GrossWeight = originalSource.NetWeight + originalSource.TareWeight);
            this.ItemType = originalSource.GatewayItemType;
            this.WeightUnitId = originalSource.WeightUnitId;
            this.Remark = originalSource.Remark;
        }

        public Guid SaveMovementItemInternal(MeasuringMoveItemData data)
        {
            //var service = this.Container.Resolve<IMeasuringService>();
            //this.Id = service.SaveItem(data);
            //return this.Id;
            return Guid.Empty;
        }

        protected override void RemoveInternal()
        {
            //var data = new RemarkData() { Id = this.Id, Value = this.Remark };
            //this.Container.Resolve<IMeasuringService>().RemoveItem(data);
        }

    }
}
