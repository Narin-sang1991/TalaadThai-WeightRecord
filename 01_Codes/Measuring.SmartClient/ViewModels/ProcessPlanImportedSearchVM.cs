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
using Cet.Hw.Core;
using Microsoft.Practices.Prism.Events;
using Measuring.SmartClient.Events;

namespace Measuring.SmartClient.ViewModels
{
    public class ProcessPlanImportedSearchVM : SearchOnlyVMBase<ProcessPlanImportedSearchData, ProcessPlanImportedSearchData, ProcessImportedCriteria>
    {
        public ProcessPlanImportedSearchVM() : this(null) { }
        public ProcessPlanImportedSearchVM(IUnityContainer container)
            : base(container)
        {
            ClearCriteria();
        }

        protected override int CountItemsInternal(ProcessImportedCriteria searchCriteria)
        {
            var service = this.Container.Resolve<IProcessPlanService>();
            return service.Count(searchCriteria);
        }

        protected override IList<ProcessPlanImportedSearchData> SearchInternal(ProcessImportedCriteria searchCriteria, SortingCriteria sortingCriteria, PagingCriteria pagingCriteria)
        {
            var service = this.Container.Resolve<IProcessPlanService>();
            return service.FindImported(searchCriteria, sortingCriteria, pagingCriteria);
        }

        protected override void PrepareDefaultSortingCriteria(SortingCriteria sortingCriteria)
        {
            sortingCriteria.Add(new SortBy() { Direction = SortDirection.ASC, Name = "ImportedNo" });
        }

        public void ItemGoTo(ProcessPlanImportedSearchData iData)
        {
            IEventAggregator eventAggregator = this.Container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<ProcessPlanImportedOpen>().Publish(new GeneralOpenPayLoad<Guid, object>() { OpenMode = OpenModeType.Update, Id = iData.Id, Param = null });
        }

        public override void ClearCriteria()
        {
            base.ClearCriteria();
            PageSize = 20;
            PageIndex = 0;
            this.SearchCriteria.FromDate = DateTime.Now.Date;
            this.SearchCriteria.ToDate = DateTime.Now.Date;
            this.SearchCriteria.CoilNo = string.Empty;
            this.SearchCriteria.ImportedNo = string.Empty;
            OnPropertyChanged("SearchCriteria");
        }

    }
}
