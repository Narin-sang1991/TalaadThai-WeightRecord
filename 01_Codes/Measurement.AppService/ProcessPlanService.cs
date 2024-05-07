using Measurement.AppServiceContract;
using Measurement.Domain.Service;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cet.Core.Data;
using Cet.Hw.Core;

namespace Measurement.AppService
{
    public class ProcessPlanService : ProcessPlanServiceBase, IProcessPlanService
    {
        public ProcessPlanService(IUnityContainer container)
             : base(container)
        {
            this.container = container;
        }

        public Guid SaveProcessPlanImported(ProcessPlanImportedData data)
        {
            var processPlanImported = Save(data);
            Repository.UnitOfWork.Commit();
            return processPlanImported.Id;
        }

        public void RemoveProcessPlanImported(Guid id)
        {
            Remove(id);
            Repository.UnitOfWork.Commit();
        }

        public IList<ProcessPlanImportedSearchData> FindImported(ProcessImportedCriteria criteria, SortingCriteria sortingCriteria, PagingCriteria pagingCriteria)
        {
            return Find(criteria, sortingCriteria, pagingCriteria);
        }

        public IList<ProcessPlanData> FindProcessPlan(ProcessPlanCriteria criteria, SortingCriteria sortingCriteria, PagingCriteria pagingCriteria)
        {
            return FindItem(criteria, sortingCriteria, pagingCriteria);
        }

    }
}
