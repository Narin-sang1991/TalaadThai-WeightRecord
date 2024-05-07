using Cet.Core;
using Cet.EntityFramework.Adaptor;
using Cet.Hw.Core.Domain;
using Measurement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace Measurement.Domain
{
    public class Measuring : HwEntity
    {
        protected Measuring() { }

        public Measuring(MeasuringMoveType iType)
        {
            Id = Guid.NewGuid();
            TypeValue = (int)iType;
            StatusValue = (int)MeasuringStatus.Draft;
            MeasuringMoveItems = new List<MeasuringMoveItem>();
        }

        #region Properties
        private Guid id;
        public Guid Id
        {
            get { return id; }
            private set { id = value; }
        }

        private int statusValue;
        public int StatusValue
        {
            get { return statusValue; }
            private set { statusValue = value; }
        }

        [NotMapped]
        public MeasuringStatus Status { get { return (MeasuringStatus)StatusValue; } }

        private DateTimeOffset? date;
        public DateTimeOffset? Date
        {
            get { return date; }
            private set { date = value; }
        }

        private string no;
        public string No
        {
            get { return no; }
            private set { no = value; }
        }

        private string referenceNo;
        public string ReferenceNo
        {
            get { return referenceNo; }
            private set { referenceNo = value; }
        }

        private Guid? businessEntityId;
        public Guid? BusinessEntityId
        {
            get { return businessEntityId; }
            private set { businessEntityId = value; }
        }

        private int typeValue;
        public int TypeValue
        {
            get { return typeValue; }
            private set { typeValue = value; }
        }
        [NotMapped]
        public MeasuringMoveType Type
        {
            get
            {
                return (MeasuringMoveType)TypeValue;
            }
        }

        public virtual ICollection<MeasuringMoveItem> MeasuringMoveItems { get; private set; }
        #endregion

        #region Method
        public void SeDate(DateTimeOffset iDate)
        {
            Date = iDate;
        }

        public void SetNo(string iNo)
        {
            No = iNo;
        }

        public void SetReferenceNo(string iRefNo)
        {
            ReferenceNo = iRefNo;
        }

        public virtual void AddItem(MeasuringMoveItem item)
        {
            if (Status != MeasuringStatus.Draft)
                throw new DomainException(string.Format(Measurement.Domain.Resources.Message.MSG_ACTION_STATUS_ONLY, MeasuringStatus.Draft.ToString()));
            MeasuringMoveItems.Add(item);
        }

        public void PrepareRemove()
        {
            if (!string.IsNullOrEmpty(No))
                throw new DomainException(Measurement.Domain.Resources.Message.MSG_CANNOT_REMOVE_HAS_NO);
            foreach (var item in MeasuringMoveItems.ToList())
            {
                RemoveItem(item);
            }
        }

        public virtual void RemoveItem(MeasuringMoveItem item)
        {
            if (Status != MeasuringStatus.Draft)
                throw new DomainException(string.Format(Measurement.Domain.Resources.Message.MSG_ACTION_STATUS_ONLY, MeasuringStatus.Draft.ToString()));

            MeasuringMoveItems.Remove(item);
            var domainEvent = this.Container.Resolve<UnityDomainEvents>();
            domainEvent.Raise(new MeasuringMoveItemRemoveEvent(item));

        }
        #endregion

        #region Action
        public void Confirm()
        {
            if (Status != MeasuringStatus.Draft)
                throw new DomainException("Unable to [Commit] to non-draft measuring");

            this.StatusValue = (int)MeasuringStatus.Commit;
            //if (String.IsNullOrEmpty(this.No))
            //    this.No = Container.Resolve<IDocumentRunningService<Measuring>>(Type.ToString()).GenerateRunningNo(this);
        }

        public void Cancel()
        {
            if (this.Status == MeasuringStatus.Cancelled)
                return;
            if (this.Status == MeasuringStatus.Draft && string.IsNullOrEmpty(No))
                throw new DomainException("Unable to [Cancel] as have't document no.");

            this.StatusValue = (int)MeasuringStatus.Cancelled;
        }

        public void Rollback()
        {
            if (Status != MeasuringStatus.Commit && Status != MeasuringStatus.Cancelled)
                throw new DomainException("Only [Confirmed] and [Canceled] can be done [Rollback].");
            this.StatusValue = (int)MeasuringStatus.Draft;
        }
        #endregion

    }

}
