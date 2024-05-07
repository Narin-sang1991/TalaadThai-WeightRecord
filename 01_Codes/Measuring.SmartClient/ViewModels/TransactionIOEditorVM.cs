using Cet.SmartClient.Client;
using Microsoft.Practices.Unity;
using System;
using System.Windows.Threading;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Linq;
using Cet.Core;
using Measurement;
using Measurement.AppServiceContract;

namespace Measuring.SmartClient.ViewModels
{
    public class TransactionIOEditorVM<T, I> : EditorVMBase<T>, IOpenEditorVM<Guid>
        where T : MeasuringData, new()
        where I : MeasuringMoveItemData
    {

        public TransactionIOEditorVM() : base() { }

        public TransactionIOEditorVM(IUnityContainer container)
                : base(container)
        {
            delayReset = Container.Resolve<int>("DelayResetStanbyInput");
            DefaultTare = 0m;

            CommitFlowCommand = new DelegateCommand(Commit);
            CancelFlowCommand = new DelegateCommand(Cancel);
            RollbackFlowCommand = new DelegateCommand(Rollback);
        }

        #region Properties        
        public DelegateCommand CommitFlowCommand { get; set; }
        public DelegateCommand CancelFlowCommand { get; set; }
        public DelegateCommand RollbackFlowCommand { get; set; }

        private Guid? id;
        public Guid? Id
        {
            get { return id; }
            protected set
            {
                id = value;
            }
        }

        public bool HasId { get { return Id.HasValue; } }

        private string docNo;
        public string DocNo
        {
            get { return docNo; }
            private set
            {
                docNo = value;
                OnPropertyChanged("DocNo");
            }
        }

        private decimal unitPrice;
        public decimal UnitPrice
        {
            get { return unitPrice; }
            set
            {
                unitPrice = value;
                OnPropertyChanged("UnitPrice");
            }
        }



        private DateTimeOffset? movementDate;
        public DateTimeOffset? MovementDate
        {
            get { return movementDate; }
            private set
            {
                movementDate = value;
                //OnPropertyChanged("MovementDate");
            }
        }

        private MeasuringMoveType type;
        public MeasuringMoveType Type
        {
            get { return type; }
            protected set
            {
                type = value;
                OnPropertyChanged("TypeDisplay");
            }
        }

        public string TypeDisplay
        {
            get
            {
                return this.Type.Translate() + System.Environment.NewLine + this.Type.ToString();
            }
        }

        private string referenceNo;
        public string ReferenceNo
        {
            get { return referenceNo; }
            set
            {
                referenceNo = value;
                OnPropertyChanged("ReferenceNo");
            }
        }

        private decimal defaultTare;
        public decimal DefaultTare
        {
            get { return defaultTare; }
            set
            {
                defaultTare = value;
                OnPropertyChanged("DefaultTare");
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

        protected int delayReset { get; set; }


        protected WeightData totalWeightData
        {
            get
            {
                var service = Container.Resolve<IMeasuringService>();
                var result = service.GetTotalWeight(this.Id.HasValue ? this.Id.Value : Guid.Empty);
                return result;
            }
        }

        public decimal TotalNetWeight
        {
            get
            {
                var result = totalWeightData != null ? Math.Round(totalWeightData.NetWeight, 0, MidpointRounding.AwayFromZero) : 0;
                return result;
            }
        }

        public decimal TotalTareWeight
        {
            get
            {
                var result = totalWeightData != null ? Math.Round(totalWeightData.TareWeight, 0, MidpointRounding.AwayFromZero) : 0;
                return result;
            }
        }

        public decimal TotalGrossWeight
        {
            get
            {
                var result = TotalNetWeight + TotalTareWeight;
                result = Math.Round(result, 0, MidpointRounding.AwayFromZero);
                return result;
            }

        }

        private MeasuringStatus status;
        public MeasuringStatus Status
        {
            get { return status; }
            private set
            {
                status = value;
                OnPropertyChanged("AllowCommit");
                OnPropertyChanged("AllowRollback");
                OnPropertyChanged("AllowCancel");
                OnPropertyChanged("StatusDisplay");
            }
        }

        public string StatusDisplay
        {
            get
            {
                return this.Status.Translate();
            }
        }

        public bool AllowCommit
        {
            get
            {
                return (Status == MeasuringStatus.Draft && IsNotEditing);
            }
        }

        public bool AllowRollback
        {
            get
            {
                return (Status != MeasuringStatus.Draft && IsNotEditing);
            }
        }

        public bool AllowCancel
        {
            get
            {
                return (Status == MeasuringStatus.Commit && IsNotEditing);
            }
        }
        #endregion

        #region Action
        public override void BeginEdit()
        {
            base.BeginEdit();
            OnPropertyChanged("AllowRollback");
            OnPropertyChanged("AllowCommit");
            OnPropertyChanged("AllowCancel");
        }

        public override void EndEdit()
        {
            base.EndEdit();
            OnPropertyChanged("AllowRollback");
            OnPropertyChanged("AllowCommit");
            OnPropertyChanged("AllowCancel");
        }

        protected virtual void Commit()
        {

        }

        protected virtual void Cancel()
        {

        }

        protected virtual void Rollback()
        {

        }
        #endregion

        #region Method
        public override Task ReloadAsync()
        {
            return LoadAsync();
        }

        public async Task LoadAsync()
        {
            IsBusy = true;
            System.Globalization.CultureInfo uiOldCulture = System.Threading.Thread.CurrentThread.CurrentUICulture;

            try
            {
                await Task.Run(() =>
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = uiOldCulture;
                    Load(this.Id.Value);
                });
                FullLoad();
            }
            finally
            {
                IsBusy = false;
            }
            RefreshHeader(OpenModeType.Update);
        }

        public void Load(Guid id)
        {
            OriginalSource = LoadInternal(id);
            if (!IsBusy)
                RefreshHeader(OpenModeType.Update);
        }

        protected virtual T LoadInternal(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual void RefreshHeader(OpenModeType mode)
        {
            if (mode == OpenModeType.New || string.IsNullOrWhiteSpace(this.DocNo))
            {
                this.DocNo = "N/A";
                Status = MeasuringStatus.Draft;
            }
        }

        protected override void LoadOriginalSource(T originalSource)
        {
            this.Id = originalSource.Id;
            this.MovementDate = originalSource.Date;
            this.DocNo = originalSource.No;
            this.ReferenceNo = originalSource.ReferenceNo;
            this.Status = originalSource.Status;
            this.Type = originalSource.Type;
        
            OnPropertyChanged("HasId");
        }


        protected override void SaveOriginalSource(T originalSource)
        {
            originalSource.Id = this.Id.HasValue ? this.Id.Value : Guid.Empty;
            originalSource.Date = this.MovementDate.HasValue ? this.MovementDate.Value : DateTimeOffset.Now;
            originalSource.No = this.DocNo;
            originalSource.ReferenceNo = this.ReferenceNo;
            originalSource.Type = this.Type;
        }

        protected virtual void TriggerItemSearchVM(decimal iWeight)
        {
            throw new NotImplementedException();
        }

        protected virtual void NotifyPartItemChanged()
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
