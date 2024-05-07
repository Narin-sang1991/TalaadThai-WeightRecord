using Cet.Hw.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTalaadThaiWg.AppServiceContract
{
    public class MeasuringCriteria : BaseStdCriteria
    {
        public string No { get; set; }
        public string LicensePlateNo { get; set; }
        public string ReferenceNo { get; set; }
        public Guid? BusinessEntityId { get; set; }
        public MeasuringMoveType? Type { get; set; }
        public MeasuringStatus? Status { get; set; }
    }


    public class MeasuringMoveCriteria
    {
        public Guid? Id { get; set; }
        public Guid? MeasuringId { get; set; }
        public bool? IsDeleted { get; set; }
    }

}
