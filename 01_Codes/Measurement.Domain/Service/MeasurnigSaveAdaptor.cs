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
    public class MeasurnigSaveAdaptor
    {
        #region Dependency
        [Dependency]
        public IUnityContainer Container { get; set; }
        [Dependency]
        public IMeasuringIntroductory Repository { get; set; }

        [Dependency]
        public IUnitIntroductory UnitRepository { get; set; }

        [Dependency]
        public IProcessPlanImportedIntroductory ProcessPlanImportedRepository { get; set; }
        #endregion Dependency

        public Measuring Save(MeasuringData data)
        {
            Measuring measuring = null;

            if (data.Id != Guid.Empty)
            {
                measuring = Repository.GetAll().Include(t => t.MeasuringMoveItems)
                            .Where(t => t.Id == data.Id).FirstOrDefault();
                UpdateMeasuring(measuring, data);
            }
            else
            {
                measuring = AddMeasuring(data);
            }
            return measuring;
        }

        public Measuring SaveWithoutItem(MeasuringData data)
        {
            Measuring measuring = null;

            if (data.Id != Guid.Empty)
            {
                measuring = Repository.GetAll().Include(t => t.MeasuringMoveItems)
                            .Where(t => t.Id == data.Id).FirstOrDefault();
                UpdateMeasuringWithoutItem(measuring, data);
            }
            else
            {
                measuring = CreateMeasuring(data);
                Container.BuildUp(measuring.GetType(), measuring);
                UpdateMeasuringWithoutItem(measuring, data);
                Repository.Add(measuring);
                return measuring;
            }
            return measuring;
        }

        #region SaveMeasuring
        protected Measuring AddMeasuring(MeasuringData data)
        {
            var measuring = CreateMeasuring(data);
            Container.BuildUp(measuring.GetType(), measuring);
            UpdateMeasuring(measuring, data);
            Repository.Add(measuring);
            return measuring;
        }

        protected Measuring CreateMeasuring(MeasuringData data)
        {
            var measuring = new Measuring(data.Type);
            Container.BuildUp(measuring.GetType(), measuring);
            return measuring;
        }

        protected virtual void UpdateMeasuring(Measuring measuring, MeasuringData data)
        {
            UpdateMeasuringWithoutItem(measuring, data);

            CollectionSaveAdaptor.Execute(
                measuring.MeasuringMoveItems.OrderBy(t => t.Id).ToList(),
                data.MeasuringMoveItems.OrderBy(t => t.Id).ToList(),
                (t1, t2) => (t1.Id == t2.Id),
                (t1) => RemoveMeasuringMoveItem(t1),
                (t1, t2) => UpdateMeasuringMoveItem(t1, t2),
                (t2) => AddMeasuringMoveItem(t2, measuring));
        }

        protected void UpdateMeasuringWithoutItem(Measuring measuring, MeasuringData data)
        {
            if (measuring == null)
                throw new FaultException<DataValidationException>(new DataValidationException() { Message = Measurement.Domain.Resources.Message.ErrorChangeData });

            if (data.Date.HasValue)
                measuring.SeDate(data.Date.Value);
            else
                measuring.SeDate(DateTimeOffset.Now);

            measuring.SetReferenceNo(data.ReferenceNo);
            if (string.IsNullOrWhiteSpace(measuring.No))
            {
                var runningService = Container.Resolve<IDocumentRunningService<Measuring>>(measuring.Type.ToString());
                measuring.SetNo(runningService.GenerateRunningNo(measuring));
            }
        }
        #endregion SaveMeasuring


        #region SaveMeasuringMoveItem

        public MeasuringMoveItem SaveItem(MeasuringMoveItemData data)
        {
            MeasuringMoveItem measuringMoveItem = null;

            if (data.Id != Guid.Empty)
            {
                measuringMoveItem = Repository.GetAll().SelectMany(t => t.MeasuringMoveItems)
                                    .Include(t => t.ProcessPlans)
                                    .Where(t => t.Id == data.Id).FirstOrDefault();
                UpdateMeasuringMoveItem(measuringMoveItem, data);
            }
            else
            {
                var parent = Repository.GetAll()
                         .Where(t => t.Id == data.MeasuringId)
                         .FirstOrDefault();
                measuringMoveItem = AddMeasuringMoveItem(data, parent);
            }
            return measuringMoveItem;
        }

        protected MeasuringMoveItem AddMeasuringMoveItem(MeasuringMoveItemData data, Measuring parent)
        {
            var measuringMoveItem = new MeasuringMoveItem(parent, data.GatewayItemType);
            Container.BuildUp(measuringMoveItem.GetType(), measuringMoveItem);
            parent.AddItem(measuringMoveItem);

            var wgUnit = UnitRepository.Get(data.WeightUnitId);
            if (wgUnit == null)
                throw new DomainException("Unit of weight is required !");
            measuringMoveItem.SetWeightData(data.NetWeight, data.TareWeight, wgUnit, data.UnitPerRatio);
            UpdateMeasuringMoveItem(measuringMoveItem, data);

            return measuringMoveItem;
        }

        protected void UpdateMeasuringMoveItem(MeasuringMoveItem measuringMoveItem, MeasuringMoveItemData data)
        {
            if (data.ProcessPlanId.HasValue)
            {
                var processPlan = ProcessPlanImportedRepository.GetAll().SelectMany(t => t.ProcessPlanItems)
                              .Where(ti => ti.Id == data.ProcessPlanId.Value).FirstOrDefault();
                if (processPlan == null)
                    throw new FaultException<DataValidationException>(new DataValidationException() { Message = Measurement.Domain.Resources.Message.MSG_PROCESS_PLAN_NOT_FOUND });
                else if (processPlan != null && processPlan.RelatedId.HasValue)
                    throw new FaultException<DataValidationException>(new DataValidationException() { Message = Measurement.Domain.Resources.Message.MSG_PROCESS_PLAN_USED });
                else if (processPlan != null && processPlan.SeqNo != measuringMoveItem.SeqNo)
                    throw new FaultException<DataValidationException>(new DataValidationException() { Message = Measurement.Domain.Resources.Message.MSG_SEQ_NOT_MAP });

                processPlan.SetRalated(measuringMoveItem);
            }

            if (measuringMoveItem.ProcessPlans.Count() > 0)
            {
                foreach (var processPlan in measuringMoveItem.ProcessPlans)
                    processPlan.ClearRelated();
            }

            measuringMoveItem.SetSeqNo(data.SeqNo);
            measuringMoveItem.SetProductData(data.ProductBarcode, data.ProductName, data.UnitPrice);
            measuringMoveItem.SetCoilNetWeight(data.CoilNetWeight);
            measuringMoveItem.SetRemark(data.Remark);
            if (data.UpdateNameWithSameBarcode && !string.IsNullOrWhiteSpace(data.ProductName))
            {
                var measuringMoveSameNameItems = Repository.GetAll().SelectMany(t => t.MeasuringMoveItems)
                                          .Where(t => t.MeasuringId == data.MeasuringId
                                                    && t.Id != data.Id
                                                    && t.ProductBarcode.Trim() == data.ProductBarcode.Trim())
                                          .ToList();
                foreach (var sameNameItem in measuringMoveSameNameItems)
                    sameNameItem.SetProductName(data.ProductName);
            }

            if (measuringMoveItem.GatewayType == GatewayItemType.Human)
            {
                var wgUnit = UnitRepository.Get(data.WeightUnitId);
                if (wgUnit == null)
                    throw new DomainException("Unit of weight is required !");

                measuringMoveItem.SetWeightData(data.NetWeight, data.TareWeight, wgUnit, data.UnitPerRatio);
            }
        }

        protected void RemoveMeasuringMoveItem(MeasuringMoveItem measuringMoveItem)
        {
            measuringMoveItem.Measuring.RemoveItem(measuringMoveItem);
        }
        #endregion SaveMeasuringItem


    }
}
