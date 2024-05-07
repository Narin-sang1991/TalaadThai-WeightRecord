using Cet.Core;
using Cet.Core.Data;
using Cet.EntityFramework.Adaptor;
using Cet.Hw.Core;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Measurement.Domain.Service
{
    public class ProcessPlanServiceBase
    {

        [Dependency]
        public IProcessPlanImportedIntroductory Repository { get; set; }

        protected IUnityContainer container;

        public ProcessPlanServiceBase(IUnityContainer container)
        {
            this.container = container;
        }

        #region Search
        public List<ProcessPlanImportedSearchData> Find(ProcessImportedCriteria criteria, SortingCriteria sortingCriteria, PagingCriteria pagingCriteria)
        {
            var imported = QryProcessPlanImported(criteria);
            if (sortingCriteria != null) imported = imported.OrderBy(sortingCriteria);
            if (pagingCriteria != null) imported = imported.Page(pagingCriteria);

            return imported.Include(t => t.ProcessPlanItems).Select(ProcessPlanImportedToSearchData).ToList();
        }

        Func<ProcessPlanImported, ProcessPlanImportedSearchData> ProcessPlanImportedToSearchData = p => new ProcessPlanImportedSearchData
        {
            Id = p.Id,
            No = p.ImportedNo,
            Date = p.ImportedDate,
            Remark = p.NoteData,
            ToTalCoilWeight = p.ProcessPlanItems.Where(t => t.IsDeleted == false).Select(t => t.CoilWeight).Sum(),
            ToTalCoilQty = p.ProcessPlanItems.Where(t => t.IsDeleted == false).Select(t => t.Id).Distinct().Count(),
        };

        Func<ProcessPlanImported, ProcessPlanImportedData> ProcessPlanImportedToData = p => new ProcessPlanImportedData
        {
            Id = p.Id,
            No = p.ImportedNo,
            Date = p.ImportedDate,
            Remark = p.NoteData,
            ToTalCoilWeight = p.ProcessPlanItems.Where(t => t.IsDeleted == false).Select(t => t.CoilWeight).Sum(),
            ToTalCoilQty = p.ProcessPlanItems.Where(t => t.IsDeleted == false).Select(t => t.Id).Distinct().Count(),
            ProcessPlanItems = p.ProcessPlanItems.Where(t => t.IsDeleted == false).OrderBy(t => t.SeqNo).Select(ProcessPlanToData).ToList()
        };

        public int Count(ProcessImportedCriteria criteria)
        {
            return QryProcessPlanImported(criteria).Count();
        }

        public ProcessPlanImportedData Get(Guid id)
        {
            var imported = QryProcessPlanImported(new ProcessImportedCriteria() { Id = id });
            return imported.Include(t => t.ProcessPlanItems).Select(ProcessPlanImportedToData).FirstOrDefault();
        }

        private IQueryable<ProcessPlanImported> QryProcessPlanImported(ProcessImportedCriteria criteria)
        {
            var qry = Repository.GetAll();

            if (criteria.Id.HasValue)
                qry = qry.Where(t => t.Id == criteria.Id.Value);

            if (!string.IsNullOrWhiteSpace(criteria.ImportedNo))
            {
                var importedNo = criteria.ImportedNo.Trim().ToLower();
                qry = qry.Where(ti => SqlFunctions.PatIndex("%" + importedNo.ToLower() + "%", ti.ImportedNo.ToLower()) > 0);
            }

            if (!string.IsNullOrWhiteSpace(criteria.CoilNo))
            {
                var coilNo = criteria.CoilNo.Trim().ToLower();
                qry = qry.SelectMany(t => t.ProcessPlanItems)
                        .Where(ti => SqlFunctions.PatIndex("%" + coilNo.ToLower() + "%", ti.CoilNo.ToLower()) > 0)
                        .Select(t => t.ProcessPlanImported);
            }

            if (criteria.FromDate.HasValue)
            {
                var startDate = new DateTimeOffset(criteria.FromDate.Value);
                qry = qry.Where(t => t.ImportedDate >= startDate);
            }

            if (criteria.ToDate.HasValue)
            {
                var toDate = new DateTimeOffset(criteria.ToDate.Value.AddDays(1));
                qry = qry.Where(t => t.ImportedDate < toDate);
            }

            return qry;
        }
        #endregion

        #region SearchItem
        public List<ProcessPlanData> FindItem(ProcessPlanCriteria criteria, SortingCriteria sortingCriteria, PagingCriteria pagingCriteria)
        {
            var processPlan = QryProcessPlan(criteria);
            if (sortingCriteria != null) processPlan = processPlan.OrderBy(sortingCriteria);
            if (pagingCriteria != null) processPlan = processPlan.Page(pagingCriteria);

            return processPlan.Select(ProcessPlanToData).Distinct().ToList();
        }

        static Func<ProcessPlan, ProcessPlanData> ProcessPlanToData = p => new ProcessPlanData
        {
            Id = p.Id,
            SeqNo = p.SeqNo,
            PosNo = p.PosNo,
            ProcessMachineCode = p.ProcessMachineCode,
            ProcessPlanDate = p.ProcessPlanDate,
            CoilNo = p.CoilNo,
            CoilWeight = p.CoilWeight,
            RelatedId = p.RelatedId,
            Remark = p.NoteData
        };

        private IQueryable<ProcessPlan> QryProcessPlan(ProcessPlanCriteria criteria)
        {
            var qry = Repository.GetAll().SelectMany(t => t.ProcessPlanItems).Where(t => t.IsDeleted == false);

            if (criteria.Id.HasValue)
                qry = qry.Where(t => t.Id == criteria.Id.Value);

            if (criteria.NotUsed.HasValue)
                if (criteria.NotUsed.Value)
                    qry = qry.Where(t => !t.RelatedId.HasValue);
                else
                    qry = qry.Where(t => t.RelatedId.HasValue);

            if (!String.IsNullOrEmpty(criteria.Barcode))
            {
                var barcodeText = criteria.Barcode.Trim();
                qry = qry.Where(t => t.CoilNo.Equals(barcodeText));
            }

            if (!String.IsNullOrEmpty(criteria.AutoCompletedText))
            {
                var autoCompletedText = criteria.AutoCompletedText.Trim();
                qry = qry.Where(t => SqlFunctions.PatIndex("%" + autoCompletedText.ToLower() + "%", t.CoilNo.ToLower()) > 0);
            }

            return qry;
        }
        #endregion

        #region SaveProcessPlan
        public ProcessPlanImported Save(ProcessPlanImportedData data)
        {
            var saveAdator = container.Resolve<ProcessPlanSaveAdaptor>();
            var planImported = saveAdator.Save(data);
            return planImported;
        }

        public void Remove(Guid id)
        {
            var processPlanImported = Repository.GetAll().Include(t => t.ProcessPlanItems)
                              .Where(t => t.Id == id).FirstOrDefault();
            if (processPlanImported == null)
                throw new FaultException<DataValidationException>(new DataValidationException() { Message = Measurement.Domain.Resources.Message.ErrorChangeData });
            processPlanImported.PrepareRemove();
            Repository.Remove(processPlanImported);
        }
        #endregion

    }
}
