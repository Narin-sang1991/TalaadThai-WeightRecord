using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Cet.Core;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WCF;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Measurement;
using Cet.Core.Data;
using Cet.Hw.Core.AppServiceContract;

namespace Measurement.AppServiceContract
{
    [ServiceContract]
    public interface IMeasuringService
    {

        [OperationContract]
        [FaultContract(typeof(ApplicationSecurityException))]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(ValidationResults))]
        [FaultContract(typeof(DataValidationException))]
        [FaultContract(typeof(UserNotLoginException))]
        MeasuringData GetWithoutItem(Guid id);

        [OperationContract]
        [FaultContract(typeof(ApplicationSecurityException))]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(ValidationResults))]
        [FaultContract(typeof(DataValidationException))]
        [FaultContract(typeof(UserNotLoginException))]
        IList<MeasuringSearchData> Find(MeasuringCriteria criteria, SortingCriteria sortingCriteria, PagingCriteria pagingCriteria);

        [OperationContract]
        [FaultContract(typeof(ApplicationSecurityException))]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(ValidationResults))]
        [FaultContract(typeof(DataValidationException))]
        [FaultContract(typeof(UserNotLoginException))]
        int Count(MeasuringCriteria criteria);

        [OperationContract]
        [FaultContract(typeof(ApplicationSecurityException))]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(ValidationResults))]
        [FaultContract(typeof(DataValidationException))]
        [FaultContract(typeof(UserNotLoginException))]
        Guid Save(MeasuringData data);

        [OperationContract]
        [FaultContract(typeof(ApplicationSecurityException))]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(ValidationResults))]
        [FaultContract(typeof(DataValidationException))]
        [FaultContract(typeof(UserNotLoginException))]
        int CountItem(MeasuringMoveCriteria criteria);

        [OperationContract]
        [FaultContract(typeof(ApplicationSecurityException))]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(ValidationResults))]
        [FaultContract(typeof(DataValidationException))]
        [FaultContract(typeof(UserNotLoginException))]
        IList<MeasuringMoveItemData> FindItem(MeasuringMoveCriteria criteria, SortingCriteria sortingCriteria, PagingCriteria pagingCriteria);

        [OperationContract]
        [FaultContract(typeof(ApplicationSecurityException))]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(ValidationResults))]
        [FaultContract(typeof(DataValidationException))]
        [FaultContract(typeof(UserNotLoginException))]
        Guid SaveItem(MeasuringMoveItemData data);

        [OperationContract]
        [FaultContract(typeof(ApplicationSecurityException))]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(ValidationResults))]
        [FaultContract(typeof(DataValidationException))]
        [FaultContract(typeof(UserNotLoginException))]
        void RemoveItem(RemarkData data);

        [OperationContract]
        [FaultContract(typeof(ApplicationSecurityException))]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(ValidationResults))]
        [FaultContract(typeof(DataValidationException))]
        [FaultContract(typeof(UserNotLoginException))]
        WeightData GetTotalWeight(Guid measuringId);

        [OperationContract]
        [FaultContract(typeof(ApplicationSecurityException))]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(ValidationResults))]
        [FaultContract(typeof(DataValidationException))]
        [FaultContract(typeof(UserNotLoginException))]
        void Commit(Guid id);

        [OperationContract]
        [FaultContract(typeof(ApplicationSecurityException))]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(ValidationResults))]
        [FaultContract(typeof(DataValidationException))]
        [FaultContract(typeof(UserNotLoginException))]
        void Cancel(Guid id);

        [OperationContract]
        [FaultContract(typeof(ApplicationSecurityException))]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(ValidationResults))]
        [FaultContract(typeof(DataValidationException))]
        [FaultContract(typeof(UserNotLoginException))]
        void Rollback(Guid id);


    }

}
