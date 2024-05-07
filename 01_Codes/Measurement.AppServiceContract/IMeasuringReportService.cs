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
    public interface IMeasuringReportService
    {

        [OperationContract]
        [FaultContract(typeof(ApplicationSecurityException))]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(ValidationResults))]
        [FaultContract(typeof(DataValidationException))]
        [FaultContract(typeof(UserNotLoginException))]
        IList<MeasuringItemWithPlanData> GetMeasuringMoveItems(Guid measuringId);

        [OperationContract]
        [FaultContract(typeof(ApplicationSecurityException))]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(ValidationResults))]
        [FaultContract(typeof(DataValidationException))]
        [FaultContract(typeof(UserNotLoginException))]
        int CountPartMoveItemGroupingReport(MeasuringReportCriteria criteria);


        [OperationContract]
        [FaultContract(typeof(ApplicationSecurityException))]
        [FaultContract(typeof(ValidationFault))]
        [FaultContract(typeof(ValidationResults))]
        [FaultContract(typeof(DataValidationException))]
        [FaultContract(typeof(UserNotLoginException))]
        IList<MeasuringMoveItemGroupingData> FindPartMoveItemGroupingReport(MeasuringReportCriteria criteria, SortingCriteria sortingCriteria, PagingCriteria pagingCriteria);


    }
}
