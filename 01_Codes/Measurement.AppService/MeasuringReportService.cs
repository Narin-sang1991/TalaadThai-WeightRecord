using Cet.Core.Data;
using Measurement.AppServiceContract;
using Measurement.Domain;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cet.EntityFramework.Adaptor;
using System.Data.Entity.SqlServer;
using System.ServiceModel;
using Cet.Core;
using Cet.Hw.Core.Domain;
using Cet.Hw.Core;

namespace Measurement.AppService
{
    public class MeasuringReportService : IMeasuringReportService
    {
        [Dependency]
        public MeasurnigServiceBase ServiceBase { get; set; }

        [Dependency]
        public IMeasuringIntroductory Repository { get; set; }

        [Dependency]
        public IUnityContainer Container { get; set; }

        public IList<MeasuringItemWithPlanData> GetMeasuringMoveItems(Guid measuringId)
        {
            var sortingCriteria = new SortingCriteria();
            sortingCriteria.Add(new SortBy() { Direction = SortDirection.ASC, Name = "PosSeq" });
            return ServiceBase.FindItemWithPlan(new MeasuringMoveCriteria() { MeasuringId = measuringId }, sortingCriteria, null);
        }

        public int CountPartMoveItemGroupingReport(MeasuringReportCriteria criteria)
        {
            return QryMeasuringMoveItem(criteria).Count();
        }

        public IList<MeasuringMoveItemGroupingData> FindPartMoveItemGroupingReport(MeasuringReportCriteria criteria, SortingCriteria sortingCriteria, PagingCriteria pagingCriteria)
        {
            //Check User Profile.
            var permissionService = Container.Resolve<IDomainAuthenticationService>(AuthType.MyAccount.ToString());
            var userProfile = permissionService.GetCurrentUserProfile();
            if (permissionService.PermissionAllow(userProfile.UserId, Container.Resolve<Guid>("AdminGroupId")))
            {
                var measuringItems = QryMeasuringMoveItem(criteria).Include(t => t.Measuring)
                                                   .Include(t => t.WeightUnit)
                                                   .Select(MeasuringMoveItemToGroupData).AsQueryable();

                if (sortingCriteria != null) measuringItems = measuringItems.OrderBy(sortingCriteria);
                if (pagingCriteria != null) measuringItems = measuringItems.Page(pagingCriteria);

                var results = measuringItems.ToList();
                return results;
            }
            else
                throw new FaultException<UserNotLoginException>(new UserNotLoginException() { Message = "Access Denied !" });


        }
        Func<MeasuringMoveItem, MeasuringMoveItemGroupingData> MeasuringMoveItemToGroupData = mi => new MeasuringMoveItemGroupingData
        {
            Id = mi.Id,
            DocumentNo = mi.Measuring.No,
            ReferenceNo = mi.Measuring.ReferenceNo,
            DocumentDate = mi.Measuring.Date.HasValue ? mi.Measuring.Date.Value : default(DateTimeOffset),
            MeasuringId = mi.MeasuringId,

            SeqNo = mi.SeqNo,
            GatewayItemType = mi.GatewayType,
            ProductBarcode = mi.ProductBarcode,
            ProductName = mi.ProductName,
            WeightUnitCode = mi.WeightUnit.Abbreviation,
            NetWeight = mi.NetWeight,
            TareWeight = mi.TareWeight,
            ActionDateTime = mi.MustData.UpdatedDate.HasValue ? mi.MustData.UpdatedDate.Value : mi.MustData.CreatedDate.Value,
            Remark = mi.NoteData

        };

        private IQueryable<MeasuringMoveItem> QryMeasuringMoveItem(MeasuringReportCriteria criteria)
        {
            var measuring = Repository.GetAll();

            if (!String.IsNullOrWhiteSpace(criteria.DocumentNo))
            {
                var no = criteria.DocumentNo.Trim();
                measuring = measuring.Where(t => SqlFunctions.PatIndex("%" + no + "%", t.No) > 0);
            }

            if (!String.IsNullOrWhiteSpace(criteria.ReferenceNo))
            {
                var refNo = criteria.ReferenceNo.Trim();
                measuring = measuring.Where(t => SqlFunctions.PatIndex("%" + refNo + "%", t.ReferenceNo) > 0);
            }


            if (criteria.DateFrom.HasValue)
            {
                var startDate = new DateTimeOffset(criteria.DateFrom.Value);
                measuring = measuring.Where(t => t.Date >= startDate || !t.Date.HasValue);
            }

            if (criteria.DateTo.HasValue)
            {
                var toDate = new DateTimeOffset(criteria.DateTo.Value.AddDays(1));
                measuring = measuring.Where(t => t.Date < toDate || !t.Date.HasValue);
            }

            return measuring.SelectMany(t => t.MeasuringMoveItems).Include(t => t.Measuring);
        }

    }
}
