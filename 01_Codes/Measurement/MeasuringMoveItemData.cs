using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement
{
    public class MeasuringItemWithPlanData 
    {
        public Guid Id { get; set; }
        public long PosSeq { get; set; }
        public string PosNo { get; set; }
        public DateTimeOffset PlanDate { get; set; }
        public string Machine { get; set; }
        public string CoilNo { get; set; }
        public decimal CoilWeight { get; set; }
        public decimal ActualWeight { get; set; }
        public DateTimeOffset ActualDate { get; set; }
    }
}
