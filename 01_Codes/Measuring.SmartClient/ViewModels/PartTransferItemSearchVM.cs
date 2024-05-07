using Cet.SmartClient.Client;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cet.Core.Data;
using Cet.Hw.Core.SmartClient;
using Cet.Hw.Core.SmartClient.ViewModels;
using Cet.Core;
using Measurement;
using Measurement.AppServiceContract;

namespace Measuring.SmartClient.ViewModels
{
    public class PartTransferItemSearchVM : SearchVMBase<PartTransferItemEditorVM, MeasuringMoveItemData, MeasuringMoveCriteria>
    {
        //public PartTransferItemSearchVM() : this(null) { }
        public PartTransferItemSearchVM(IUnityContainer container)
            : base(container)
        {
            PageSize = 20;
            PageIndex = 0;
            SearchCriteria.IsDeleted = false;
            UpdateNameWithSameBarcode = false;
        }

        protected override int CountItemsInternal(MeasuringMoveCriteria searchCriteria)
        {
            if (searchCriteria.MeasuringId.HasValue)
            {
                var service = this.Container.Resolve<IMeasuringService>();
                return service.CountItem(searchCriteria);
            }
            return 0;
        }

        protected override IList<MeasuringMoveItemData> SearchInternal(MeasuringMoveCriteria searchCriteria, SortingCriteria sortingCriteria, PagingCriteria pagingCriteria)
        {
            if (searchCriteria.MeasuringId.HasValue)
            {
                var service = this.Container.Resolve<IMeasuringService>();
                return service.FindItem(searchCriteria, sortingCriteria, pagingCriteria);
            }
            return new List<MeasuringMoveItemData>();
        }

        protected override void PrepareDefaultSortingCriteria(SortingCriteria sortingCriteria)
        {
            sortingCriteria.Add(new SortBy() { Direction = SortDirection.DESC, Name = "SeqNo" });
        }

        #region Properties

        public bool IsReceive
        {
            get { return Type == MeasuringMoveType.Into; }
        }
        public bool IsSelling
        {
            get { return Type == MeasuringMoveType.Out; }
        }

        public event EventHandler RefreshParent;

        private MeasuringMoveType type;
        public MeasuringMoveType Type
        {
            get { return type; }
            private set
            {
                type = value;
                OnPropertyChanged("IsReceive");
                OnPropertyChanged("IsSelling");
            }
        }

        private Guid parnetId;
        public Guid ParentId
        {
            get { return parnetId; }
            private set
            { parnetId = value; }
        }

        public GatewayItemType ItemType { get; private set; }

        private string imgBg;
        public string ImgBg
        {
            get { return imgBg; }
            set
            {
                imgBg = value;
                OnPropertyChanged("ImgBg");
            }
        }

        private bool updateNameWithSameBarcode;
        public bool UpdateNameWithSameBarcode
        {
            get { return updateNameWithSameBarcode; }
            set
            {
                updateNameWithSameBarcode = value;
                if (!IsPoked)
                {
                    foreach (var editorVM in SearchResult)
                        editorVM.UpdateNameWithSameBarcode = this.UpdateNameWithSameBarcode;
                }
                OnPropertyChanged("UpdateNameWithSameBarcode");
            }
        }
        #endregion Properties

        protected override void InitializeAddingItem(PartTransferItemEditorVM editorVM)
        {
            base.InitializeAddingItem(editorVM);
            editorVM.MeasuringMovementId = ParentId;
            editorVM.SeqNo = (Int16)(CountItemsInternal(SearchCriteria) + 1);
            Poke(() => editorVM.UpdateNameWithSameBarcode = this.UpdateNameWithSameBarcode);
            editorVM.RefreshParentItem += RefreshParent;
            if (!IsPoked)
            {
                editorVM.ItemType = this.ItemType;
            }
        }

        protected override void InitializeLoadingItem(PartTransferItemEditorVM editorVM)
        {
            base.InitializeLoadingItem(editorVM);
            Poke(() => editorVM.UpdateNameWithSameBarcode = this.UpdateNameWithSameBarcode);
            editorVM.RefreshParentItem += RefreshParent;
        }

        public void SearchRefresh()
        {
            Search();
            RefreshParent(this, null);
        }

        public void SetParentData(Guid iParentId, MeasuringMoveType iType, bool isSelected = true)
        {
            SearchCriteria.MeasuringId = ParentId = iParentId;
            Type = iType;
            IsSelected = isSelected;
        }

        public bool SetWeight(WeightData iWeightData)
        {
            if (iWeightData != null)
            {
                var countAll = CountItemsInternal(this.SearchCriteria);

                var newEditorVM = Container.Resolve<PartTransferItemEditorVM>();
                var data = new MeasuringMoveItemData();
                data.GatewayItemType = GatewayItemType.Machine;
                data.SeqNo = countAll + 1;
                data.MeasuringId = ParentId;
                data.ProductName = iWeightData.ProductName;
                data.ProductBarcode = iWeightData.ProductBarcode;
                data.UnitPerRatio = iWeightData.UnitPerRatio;
                data.TareWeight = iWeightData.TareWeight;
                data.NetWeight = iWeightData.NetWeight;
                data.WeightUnitId = Container.Resolve<Guid>("DefaultUnitId");
                data.WeightUnitCode = iWeightData.WeightUnitCode;
                Poke(() => InitializeAddingItem(newEditorVM));
                var itemId = newEditorVM.SaveMovementItemInternal(data);
                //Search();
                if (itemId != Guid.Empty)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public override Task RemoveItemAsync(object item)
        {
            return OnShowRemarkDialog(item as PartTransferItemEditorVM);
        }

        protected Task OnShowRemarkDialog(PartTransferItemEditorVM item)
        {
            var dialog = new RemarkLookupDialog(Container);
            var vm = Container.Resolve<RemarkEditorVM>();
            dialog.DataContext = vm;
            vm.Header = string.Format(Cet.Hw.Core.SmartClient.Resources.RemarkEditorView.DISPLAY_HEADER_DIALOG, item.ProductName);
            vm.BeginEdit();
            bool? dialogResult = dialog.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value)
            {
                item.Remark = vm.Remark;
                return base.RemoveItemAsync(item);
            }
            else throw new Exception(Cet.Hw.Core.SmartClient.Resources.Messages.MSG_NOT_EVENT);
        }


    }
}
