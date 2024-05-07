using Cet.EntityFramework.Adaptor;
using Cet.Hw.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement.Domain
{
    public class ProcessPlan : HwEntity
    {
        protected ProcessPlan() { }

        public ProcessPlan(ProcessPlanImported parent)
        {
            Id = Guid.NewGuid();
            ProcessPlanImported = parent;
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
            private set { seqNo = value; }
        }

        private string posNo;
        public string PosNo
        {
            get { return posNo; }
            private set { posNo = value; }
        }

        private DateTimeOffset processPlanDate;
        public DateTimeOffset ProcessPlanDate
        {
            get { return processPlanDate; }
            private set { processPlanDate = value; }
        }

        private string processMachineCode;
        public string ProcessMachineCode
        {
            get { return processMachineCode; }
            private set { processMachineCode = value; }
        }

        private string coilNo;
        public string CoilNo
        {
            get { return coilNo; }
            private set { coilNo = value; }
        }

        private decimal coilWeight;
        public decimal CoilWeight
        {
            get { return coilWeight; }
            private set { coilWeight = value; }
        }

        private bool isDeleted;
        public bool IsDeleted
        {
            get { return isDeleted; }
            private set { isDeleted = value; }
        }

        private Guid processPlanImportedId;
        public Guid ProcessPlanImportedId
        {
            get { return processPlanImportedId; }
            private set { processPlanImportedId = value; }
        }
        public virtual ProcessPlanImported ProcessPlanImported { get; private set; }

        private Guid? relatedId;
        public Guid? RelatedId
        {
            get { return relatedId; }
            private set { relatedId = value; }
        }
        public virtual MeasuringMoveItem RelatedItem { get; private set; }
        #endregion


        public void SetPackData(ProcessPlanData data)
        {
            this.SeqNo = data.SeqNo;
            this.PosNo = data.PosNo;
            this.ProcessPlanDate = data.ProcessPlanDate;
            this.ProcessMachineCode = data.ProcessMachineCode;
            this.CoilNo = data.CoilNo;
            this.CoilWeight = data.CoilWeight;
            this.SetNoteData(data.Remark);
        }

        public void SetRalated(MeasuringMoveItem iRelated)
        {
            this.RelatedItem = iRelated;
        }

        public void ClearRelated()
        {
            this.RelatedItem = null;
        }

        public void VirtualRemove()
        {
            this.IsDeleted = true;
        }

    }

    public class ProcessPlanItemRemoveEvent : IDomainEvent
    {
        public ProcessPlanItemRemoveEvent(ProcessPlan item)
        {
            ProcessPlanItem = item;
        }

        public ProcessPlan ProcessPlanItem { get; private set; }
    }

}
