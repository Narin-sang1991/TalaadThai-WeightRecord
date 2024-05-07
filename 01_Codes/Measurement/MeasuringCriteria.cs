using Cet.Hw.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement
{
    public class MeasuringCriteria : BaseStdCriteria
    {
        public string No { get; set; }
        public string ReferenceNo { get; set; }
        public Guid? BusinessEntityId { get; set; }
        public MeasuringMoveType? Type { get; set; }
        public MeasuringStatus? Status { get; set; }
    }

}
