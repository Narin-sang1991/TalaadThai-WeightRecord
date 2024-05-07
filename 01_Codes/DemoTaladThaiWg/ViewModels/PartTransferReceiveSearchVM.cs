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
    public class PartTransferReceiveSearchVM : TransactionSearchVM<MeasuringSearchData, MeasuringCriteria>
    {
        public PartTransferReceiveSearchVM(IUnityContainer container)
            : base(container)
        {
            this.PageSize = 20;
            this.PageIndex = 0;
            this.ClearCriteria();
        }

        protected override IList<MeasuringSearchData> SearchInternal(MeasuringCriteria searchCriteria, SortingCriteria sortingCriteria, PagingCriteria pagingCriteria)
        {
            var service = this.Container.Resolve<MeasuringService>();
            return service.Find(searchCriteria, sortingCriteria, pagingCriteria);
        }

        protected override int CountItemsInternal(MeasuringCriteria searchCriteria)
        {
            var service = this.Container.Resolve<MeasuringService>();
            return service.Count(searchCriteria);
        }

        public override void ItemGoTo(MeasuringSearchData iData)
        {
            IEventAggregator eventAggregator = this.Container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<PartTransferReceiveOpen>().Publish(new GeneralOpenPayLoad<Guid, object>() { OpenMode = OpenModeType.Update, Id = iData.Id, Param = null });
        }

        public override void ClearCriteria()
        {
            base.ClearCriteria();
            this.SearchCriteria.FromDate = DateTime.Now.Date;
            this.SearchCriteria.ToDate = DateTime.Now.Date;
        }

        protected override void ExportExcel()
        {
            //var viewer = new MovementReceiveReportViewer();
            //viewer.DataContext = this;
            //viewer.Show();
        }

    }
}

