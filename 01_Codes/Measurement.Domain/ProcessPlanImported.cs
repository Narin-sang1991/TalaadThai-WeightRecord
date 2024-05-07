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
    public class ProcessPlanImported : HwEntity
    {
        protected ProcessPlanImported() { }

        public ProcessPlanImported(bool dummy = true)
        {
            Id = Guid.NewGuid();
            ProcessPlanItems = new List<ProcessPlan>();
        }

        #region Properties
        private Guid id;
        public Guid Id
        {
            get { return id; }
            private set { id = value; }
        }

        private DateTimeOffset importedDate;
        public DateTimeOffset ImportedDate
        {
            get { return importedDate; }
            private set { importedDate = value; }
        }

        private string importedNo;
        public string ImportedNo
        {
            get { return importedNo; }
            private set { importedNo = value; }
        }
        public virtual ICollection<ProcessPlan> ProcessPlanItems { get; private set; }
        #endregion


        #region Method
        public void SeDate(DateTimeOffset iDate)
        {
            ImportedDate = iDate;
        }

        public void SetNo(string iNo)
        {
            ImportedNo = iNo;
        }

        public virtual void AddItem(ProcessPlan item)
        {
            ProcessPlanItems.Add(item);
        }

        public void PrepareRemove()
        {
            if (!string.IsNullOrEmpty(ImportedNo))
                throw new DomainException(Measurement.Domain.Resources.Message.MSG_CANNOT_REMOVE_HAS_NO);
            foreach (var item in ProcessPlanItems.ToList())
            {
                RemoveItem(item);
            }
        }

        public virtual void RemoveItem(ProcessPlan item)
        {
            ProcessPlanItems.Remove(item);
            var domainEvent = this.Container.Resolve<UnityDomainEvents>();
            domainEvent.Raise(new ProcessPlanItemRemoveEvent(item));

        }
        #endregion
    }
}
