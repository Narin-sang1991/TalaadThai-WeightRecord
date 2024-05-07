using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement
{
   public class MeasuringReportCriteria
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string DocumentNo { get; set; }
        public string ReferenceNo { get; set; }
    }
}
