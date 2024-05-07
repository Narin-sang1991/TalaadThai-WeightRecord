using Cet.Core;
using Cet.EntityFramework.Adaptor;
using Measurement;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Measurement.Domain
{
    public class ProcessPlanSaveAdaptor
    {
        #region Dependency
        [Dependency]
        public IUnityContainer Container { get; set; }
        [Dependency]
        public IProcessPlanImportedIntroductory Repository { get; set; }

        #endregion Dependency

        public ProcessPlanImported Save(ProcessPlanImportedData data)
        {
            ProcessPlanImported processPlanImported = null;

            if (data.Id != Guid.Empty)
            {
                processPlanImported = Repository.GetAll().Include(t => t.ProcessPlanItems)
                            .Where(t => t.Id == data.Id).FirstOrDefault();
                UpdateInternal(processPlanImported, data);
            }
            else
            {
                processPlanImported = AddInternal(data);
            }
            return processPlanImported;
        }

        #region SaveProcessPlanImported
        protected ProcessPlanImported AddInternal(ProcessPlanImportedData data)
        {
            var processPlanImported = CreateInternal();
            UpdateInternal(processPlanImported, data);
            Repository.Add(processPlanImported);
            return processPlanImported;
        }

        protected ProcessPlanImported CreateInternal()
        {
            var processPlanImported = new ProcessPlanImported();
            Container.BuildUp(processPlanImported.GetType(), processPlanImported);
            return processPlanImported;
        }

        protected virtual void UpdateInternal(ProcessPlanImported processPlanImported, ProcessPlanImportedData data)
        {
            if (processPlanImported == null)
                throw new FaultException<DataValidationException>(new DataValidationException() { Message = Measurement.Domain.Resources.Message.ErrorChangeData });

            if (data.Date.HasValue)
                processPlanImported.SeDate(data.Date.Value);
            else
                processPlanImported.SeDate(DateTimeOffset.Now);

            if (string.IsNullOrWhiteSpace(processPlanImported.ImportedNo))
            {
                var runningService = Container.Resolve<IDocumentRunningService<ProcessPlanImported>>();
                processPlanImported.SetNo(runningService.GenerateRunningNo(processPlanImported));
            }

            CollectionSaveAdaptor.Execute(
                processPlanImported.ProcessPlanItems.OrderBy(t => t.Id).ToList(),
                data.ProcessPlanItems.OrderBy(t => t.Id).ToList(),
                (t1, t2) => (t1.Id == t2.Id),
                (t1) => RemoveItem(t1),
                (t1, t2) => UpdateItemInternal(t1, t2),
                (t2) => AddItemInternal(processPlanImported, t2));
        }
        #endregion SaveProcessPlanImported


        #region SaveProcessPlan
        protected ProcessPlan AddItemInternal(ProcessPlanImported parent, ProcessPlanData data)
        {
            var processPlan = new ProcessPlan(parent);
            Container.BuildUp(processPlan.GetType(), processPlan);
            parent.AddItem(processPlan);
            UpdateItemInternal(processPlan, data);
            return processPlan;
        }

        protected void UpdateItemInternal(ProcessPlan processPlan, ProcessPlanData data)
        {
            if (processPlan == null)
                throw new FaultException<DataValidationException>(new DataValidationException() { Message = Measurement.Domain.Resources.Message.ErrorChangeData });

            processPlan.SetPackData(data);
        }

        protected void RemoveItem(ProcessPlan processPlan)
        {
            if (processPlan.RelatedItem != null)
                processPlan.RelatedItem.ClearRelated(processPlan);
            processPlan.VirtualRemove();
        }
        #endregion SaveProcessPlan


    }
}
