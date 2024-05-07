using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement
{
    public class MeasuringMoveItemGroupingData : MeasuringMoveItemData
    {
        public string DocumentNo { get; set; }
        public string ReferenceNo { get; set; }
        public DateTimeOffset DocumentDate { get; set; }
        public DateTimeOffset ActionDateTime { get; set; }
        public string Actor { get; set; }
    }
}
