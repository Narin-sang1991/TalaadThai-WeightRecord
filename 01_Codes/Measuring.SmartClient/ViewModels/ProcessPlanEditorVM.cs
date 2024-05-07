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

namespace Measuring.SmartClient.ViewModels
{
    public class ProcessPlanEditorVM : DocEditorVMBase<ProcessPlanData>
    {

        public ProcessPlanEditorVM()
            : base()
        {

        }
        public ProcessPlanEditorVM(IUnityContainer container)
            : base(container)
        {
            this.Used = false;
        }

        #region Properties
        private Guid id;
        public Guid Id
        {
            get { return id; }
            private set { id = value; }
        }

        private Int64 seqNo;
        public Int64 SeqNo
        {
            get { return seqNo; }
            set
            {
                seqNo = value;
                OnPropertyChanged("SeqNo");
            }
        }

        private DateTime processPlanDate;
        public DateTime ProcessPlanDate
        {
            get { return processPlanDate; }
            private set
            {
                processPlanDate = value;
                OnPropertyChanged("ProcessPlanDate");
            }
        }

        private string posNo;
        public string PosNo
        {
            get { return posNo; }
            private set
            {
                posNo = value;
                OnPropertyChanged("PosNo");
            }
        }

        private string processMachineCode;
        public string ProcessMachineCode
        {
            get { return processMachineCode; }
            private set
            {
                processMachineCode = value;
                OnPropertyChanged("ProcessMachineCode");
            }
        }

        private string coilNo;
        public string CoilNo
        {
            get { return coilNo; }
            private set
            {
                coilNo = value;
                OnPropertyChanged("CoilNo");
            }
        }

        private int coilQty;
        public int CoilQty
        {
            get { return coilQty; }
            private set
            {
                coilQty = value;
                OnPropertyChanged("CoilQty");
            }
        }

        private decimal coilWeight;
        public decimal CoilWeight
        {
            get { return coilWeight; }
            private set
            {
                coilWeight = value;
                OnPropertyChanged("CoilWeight");
            }
        }

        private string remark;
        public string Remark
        {
            get { return remark; }
            private set
            {
                remark = value;
                OnPropertyChanged("Remark");
            }
        }

        private bool used;
        public bool Used
        {
            get { return used; }
            private set
            {
                used = value;
                OnPropertyChanged("Used");
                OnPropertyChanged("UsedStatus");
                OnPropertyChanged("UsedStatusColor");
            }
        }
        public string UsedStatus
        {
            get
            {
                return Used ? "/" : string.Empty;
            }

        }
        public string UsedStatusColor
        {
            get
            {
                return Used ? "Green" : "Transparent";
            }

        }

        #endregion Properties
        public override void SaveOriginalSource(ProcessPlanData originalSource)
        {
            originalSource.Id = this.Id;
            originalSource.SeqNo = this.SeqNo;
            originalSource.ProcessPlanDate = this.ProcessPlanDate;
            originalSource.PosNo = this.PosNo;
            originalSource.ProcessMachineCode = this.ProcessMachineCode;
            originalSource.CoilNo = this.CoilNo;
            originalSource.CoilWeight = this.CoilWeight;
            originalSource.Remark = this.Remark;
        }

        public override void LoadOriginalSource(ProcessPlanData originalSource)
        {
            this.Id = originalSource.Id;
            this.SeqNo = originalSource.SeqNo;
            this.ProcessPlanDate = originalSource.ProcessPlanDate.DateTime;
            this.PosNo = originalSource.PosNo;
            this.ProcessMachineCode = originalSource.ProcessMachineCode;
            this.CoilNo = originalSource.CoilNo;
            this.CoilWeight = originalSource.CoilWeight;
            this.Used = originalSource.RelatedId.HasValue;
            this.Remark = originalSource.Remark;
        }

    }
}
