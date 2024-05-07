using Cet.Hw.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement
{
    public class ProcessPlanCriteria : BaseStdCriteria
    {
        public string Barcode { get; set; }
        public bool? NotUsed { get; set; }
        public string AutoCompletedText { get; set; }
    }

}
