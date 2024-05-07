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
using Cet.Hw.Core;

namespace Measurement.AppServiceContract
{
    [ServiceContract]
    public interface IProcessPlanService
    {
        [OperationContract]
        [FaultContract(typeof(ApplicationSecurityException))]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(ValidationResults))]
        [FaultContract(typeof(DataValidationException))]
        [FaultContract(typeof(UserNotLoginException))]
        IList<ProcessPlanImportedSearchData> FindImported(ProcessImportedCriteria criteria, SortingCriteria sortingCriteria, PagingCriteria pagingCriteria);

        [OperationContract]
        [FaultContract(typeof(ApplicationSecurityException))]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(ValidationResults))]
        [FaultContract(typeof(DataValidationException))]
        [FaultContract(typeof(UserNotLoginException))]
        int Count(ProcessImportedCriteria criteria);

        [OperationContract]
        [FaultContract(typeof(ApplicationSecurityException))]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(ValidationResults))]
        [FaultContract(typeof(DataValidationException))]
        [FaultContract(typeof(UserNotLoginException))]
        ProcessPlanImportedData Get(Guid id);

        [OperationContract]
        [FaultContract(typeof(ApplicationSecurityException))]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(ValidationResults))]
        [FaultContract(typeof(DataValidationException))]
        [FaultContract(typeof(UserNotLoginException))]
        Guid SaveProcessPlanImported(ProcessPlanImportedData data);

        [OperationContract]
        [FaultContract(typeof(ApplicationSecurityException))]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(ValidationResults))]
        [FaultContract(typeof(DataValidationException))]
        [FaultContract(typeof(UserNotLoginException))]
        void RemoveProcessPlanImported(Guid id);

        [OperationContract]
        [FaultContract(typeof(ApplicationSecurityException))]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(ValidationResults))]
        [FaultContract(typeof(DataValidationException))]
        [FaultContract(typeof(UserNotLoginException))]
        IList<ProcessPlanData> FindProcessPlan(ProcessPlanCriteria criteria, SortingCriteria sortingCriteria, PagingCriteria pagingCriteria);

    }
}
