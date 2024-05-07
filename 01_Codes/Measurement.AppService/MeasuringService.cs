using Measurement.Domain;
using Measurement.AppServiceContract;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cet.Hw.Core.AppServiceContract;
using Cet.Core.Data;
using System.ServiceModel;
using Cet.Core;
using Cet.Hw.Core.Domain;
using Cet.Hw.Core;

namespace Measurement.AppService
{
    public class MeasuringService : MeasurnigServiceBase, IMeasuringService
    {
        public MeasuringService(IUnityContainer container)
             : base(container)
        {
            this.container = container;
        }

        public WeightData GetTotalWeight(Guid measuringId)
        {
            var measuringMoveItem = Repository.GetAll().SelectMany(t => t.MeasuringMoveItems)
                                 .Where(ti => ti.MeasuringId == measuringId);

            var result = new WeightData();
            if (measuringMoveItem.Count() != 0)
            {
                var measuringMoveItems = measuringMoveItem.Where(ti => ti.IsDeleted == false).ToList();

                result.NetWeight = measuringMoveItems.Select(ti => ti.NetWeight).Sum();
                result.TareWeight = measuringMoveItems.Select(ti => ti.TareWeight).Sum();
            }
            else
            {
                result.NetWeight = 0;
                result.TareWeight = 0;
            }

            return result;
        }

        public void RemoveItem(RemarkData data)
        {
            //Check User Profile.
            var permissionService = container.Resolve<IDomainAuthenticationService>(AuthType.MyAccount.ToString());
            permissionService.GetCurrentUserProfile();

            var measuringMoveItem = Repository.GetAll().SelectMany(t => t.MeasuringMoveItems)
                                .Where(ti => ti.Id == data.Id).FirstOrDefault();

            if (measuringMoveItem == null)
                throw new FaultException<DataValidationException>(new DataValidationException() { Message = Measurement.Domain.Resources.Message.ErrorChangeData });

            measuringMoveItem.SetRemark(data.Value);
            measuringMoveItem.VirtualRemove();

            var moreSeqNoItems = Repository.GetAll().SelectMany(t => t.MeasuringMoveItems)
                             .Where(ti => ti.MeasuringId == measuringMoveItem.MeasuringId
                                         && ti.IsDeleted == false && ti.Id != measuringMoveItem.Id
                                         && ti.SeqNo > measuringMoveItem.SeqNo)
                             .ToList();
            var newSeqNo = measuringMoveItem.SeqNo;
            foreach (var moreSeqNoItem in moreSeqNoItems)
            {
                moreSeqNoItem.SetSeqNo(newSeqNo);
                newSeqNo++;
            }

            Repository.UnitOfWork.Commit();
        }

        IList<MeasuringSearchData> IMeasuringService.Find(MeasuringCriteria criteria, SortingCriteria sortingCriteria, PagingCriteria pagingCriteria)
        {
            return base.Find(criteria, sortingCriteria, pagingCriteria);
        }
    }
}
