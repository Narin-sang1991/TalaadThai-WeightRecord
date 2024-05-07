using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cet.SmartClient.Client;
using Microsoft.Practices.Unity;
using Cet.Core.Data;
using System.Windows.Input;
using Microsoft.Practices.Prism.Events;
using WPFLocalizeExtension.Extensions;
using DemoTaladThaiWg.Shell.Events;
using DemoTalaadThaiWg.AppServiceContract;
using DemoTalaadThai.AppService;

namespace DemoTaladThaiWg.Shell.ViewModels
{
    public class MeasuringSearchVM : SearchOnlyVMBase<MeasuringSearchData, MeasuringSearchData, MeasuringCriteria>
    {
        public MeasuringSearchVM(IUnityContainer container)
            : base(container)
        {
            this.PageSize = 20;
            this.PageIndex = 0;
            this.ExceptionIdList = new List<Guid?>();
            this.ClearCriteria();
        }

        private MeasuringMoveType type;
        public MeasuringMoveType Type
        {
            get { return type; }
            private set
            {
                type = value;
                SearchCriteria.Type = value;
             }
        }

        private IList<Guid?> exceptionIdList;
        public IList<Guid?> ExceptionIdList
        {
            get { return exceptionIdList; }
            private set { exceptionIdList = value; }
        }

        public void SetMeasuringType(MeasuringMoveType iType)
        {
            Type = iType;
        }

        public void SetExceptionIds(List<Guid?> exceptionIds)
        {
            ExceptionIdList = exceptionIds;
        }

        protected override IList<MeasuringSearchData> SearchInternal(MeasuringCriteria searchCriteria, SortingCriteria sortingCriteria, PagingCriteria pagingCriteria)
        {
            var service = this.Container.Resolve<MeasuringService>();
            var datas = service.Find(searchCriteria, sortingCriteria, pagingCriteria);

            foreach (var d in datas.Where(t => ExceptionIdList.Contains(t.Id)))
            {
                d.IsSelected = true;
            }
            return datas;
        }

        protected override int CountItemsInternal(MeasuringCriteria searchCriteria)
        {
            var service = this.Container.Resolve<MeasuringService>();
            return service.Count(searchCriteria);
        }

        public override void ClearCriteria()
        {
            base.ClearCriteria();
            this.SearchCriteria.FromDate = DateTime.Now.Date;
            this.SearchCriteria.ToDate = DateTime.Now.Date;
            this.SearchCriteria.Type = this.Type;
            OnPropertyChanged("SearchCriteria");
        }

        protected override void PrepareDefaultSortingCriteria(SortingCriteria sortingCriteria)
        {
            sortingCriteria.Add(new SortBy() { Direction = SortDirection.ASC, Name = "LicensePlateNo" });
            sortingCriteria.Add(new SortBy() { Direction = SortDirection.ASC, Name = "Date" });
        }

    }
}

