using Cet.Core;
using Cet.Core.Data;
using Cet.EntityFramework.Adaptor;
using DemoTalaadThaiWg.AppServiceContract;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Cet.Hw.Core;

namespace DemoTalaadThaiWg.Domain.Services
{
    public class MeasurnigServiceBase
    {
        [Dependency]
        public IMeasuringRepository Repository { get; set; }
        

        protected IUnityContainer container;

        public MeasurnigServiceBase(IUnityContainer container)
        {
            this.container = container;
        }

        #region Search Measuring
        public MeasuringData GetWithoutItem(Guid id)
        {
            var measuring = QryMeasuring(new MeasuringCriteria() { Id = id });
            var result = measuring.Select(MeasuringToData).FirstOrDefault();
            return result;
        }

        public List<MeasuringSearchData> Find(MeasuringCriteria criteria, SortingCriteria sortingCriteria, PagingCriteria pagingCriteria)
        {
            var measuring = QryMeasuring(criteria);

            if (sortingCriteria != null) measuring = measuring.OrderBy(sortingCriteria);
            if (pagingCriteria != null) measuring = measuring.Page(pagingCriteria);

            return measuring.Include(t => t.MeasuringMoveItems)
                    .Select(MeasuringToSearchData).ToList();
        }

        public int Count(MeasuringCriteria criteria)
        {
            return QryMeasuring(criteria).Count();
        }

        protected IQueryable<Measuring> QryMeasuring(MeasuringCriteria criteria)
        {
            var measuring = Repository.GetAll().Where(MeasuringSpecification.GetSpecification(criteria));
            return measuring;
        }

        Func<Measuring, MeasuringSearchData> MeasuringToSearchData = m => new MeasuringSearchData
        {
            Id = m.Id,
            No = m.No,
            ReferenceNo = m.ReferenceNo,
            LicensePlateNo = m.LicensePlateNo,
            Date = m.Date,
            Status = m.Status,
            Type = m.Type,
            Remark = m.NoteData,
            ToTalNetWeight = m.MeasuringMoveItems.Select(t => t.NetWeight).Sum(),
            ToTalTareWeight = m.MeasuringMoveItems.Select(t => t.TareWeight).Sum(),
        };

        Func<Measuring, MeasuringData> MeasuringToData = pt => new MeasuringData
        {
            Id = pt.Id,
            No = pt.No,
            LicensePlateNo = pt.LicensePlateNo,
            ReferenceNo = pt.ReferenceNo,
            Date = pt.Date,
            BusinessEntityId = pt.Id,
            Status = pt.Status,
            Type = pt.Type,
            Remark = pt.NoteData,
        };

        #endregion EndSearch 


        #region Search MeasuringMoveItem
        public int CountItem(MeasuringMoveCriteria criteria)
        {
            return QryMeasuringMoveItem(criteria).Count();
        }

        public IList<MeasuringMoveItemData> FindItem(MeasuringMoveCriteria criteria, SortingCriteria sortingCriteria, PagingCriteria pagingCriteria)
        {
            var measuringMoveItem = QryMeasuringMoveItem(criteria);

            if (sortingCriteria != null) measuringMoveItem = measuringMoveItem.OrderBy(sortingCriteria);
            if (pagingCriteria != null) measuringMoveItem = measuringMoveItem.Page(pagingCriteria);

            var results = measuringMoveItem.Select(MeasuringMoveItemToData).ToList();
            return results;
        }

        protected IQueryable<MeasuringMoveItem> QryMeasuringMoveItem(MeasuringMoveCriteria criteria)
        {
            var measuringMoveItem = Repository.GetAll().SelectMany(t => t.MeasuringMoveItems);

            if (criteria.Id.HasValue)
                measuringMoveItem = measuringMoveItem.Where(t => t.Id == criteria.Id.Value);

            if (criteria.MeasuringId.HasValue)
                measuringMoveItem = measuringMoveItem.Where(t => t.MeasuringId == criteria.MeasuringId.Value);

            if (criteria.IsDeleted.HasValue)
                measuringMoveItem = measuringMoveItem.Where(t => t.IsDeleted == criteria.IsDeleted.Value);

            return measuringMoveItem;
        }


        static Func<MeasuringMoveItem, MeasuringMoveItemData> MeasuringMoveItemToData = ti => new MeasuringMoveItemData
        {
            Id = ti.Id,
            MeasuringId = ti.MeasuringId,
            SeqNo = ti.SeqNo,
            GatewayItemType = ti.GatewayType,
            ProductBarcode = ti.ProductBarcode,
            ProductName = ti.ProductName,
            UnitPrice = ti.UnitPrice,
            NetWeight = ti.NetWeight,
            TareWeight = ti.TareWeight,
            UnitPerRatio = ti.UnitPerRatio,
            WeightUnitCode = ti.WeightUnitCode,
            Remark = ti.NoteData,
        };
        #endregion


        #region Save
        public Guid Save(MeasuringData data)
        {
            var saveAdator = container.Resolve<MeasurnigSaveAdaptor>();
            data.Date = data.Date.HasValue ? data.Date.Value : DateTimeOffset.Now;
            var measurnig = saveAdator.Save(data);
            Repository.UnitOfWork.Commit();
            return measurnig.Id;
        }

        public Guid SaveItem(MeasuringMoveItemData data)
        {
            var saveAdator = container.Resolve<MeasurnigSaveAdaptor>();
            var item = saveAdator.SaveItem(data);
            Repository.UnitOfWork.Commit();
            return item.Id;
        }
        #endregion

        #region Action
        public void Commit(Guid id)
        {
            var measuring = QryMeasuring(new MeasuringCriteria() { Id = id })
                            .Include(t => t.MeasuringMoveItems)
                            .FirstOrDefault();
            if (measuring == null)
                throw new FaultException<DataValidationException>(new DataValidationException() { Message = "Not found measuring document !" });

            measuring.Confirm();
            Repository.UnitOfWork.Commit();
        }


        public void Cancel(Guid id)
        {
            var measuring = QryMeasuring(new MeasuringCriteria() { Id = id })
                            .Include(t => t.MeasuringMoveItems)
                            .FirstOrDefault();
            if (measuring == null)
                throw new FaultException<DataValidationException>(new DataValidationException() { Message = "Not found measuring document !" });

            measuring.Cancel();
            Repository.UnitOfWork.Commit();
        }

        public void Rollback(Guid id)
        {
            var measuring = QryMeasuring(new MeasuringCriteria() { Id = id })
                            .Include(t => t.MeasuringMoveItems)
                            .FirstOrDefault();
            if (measuring == null)
                throw new FaultException<DataValidationException>(new DataValidationException() { Message = "Not found measuring document !" });

            measuring.Rollback();
            Repository.UnitOfWork.Commit();
        }
        #endregion


    }
}
