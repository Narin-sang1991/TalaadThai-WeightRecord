using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTalaadThaiWg.AppServiceContract
{
    public class WeightData
    {
        public string ProductBarcode { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPerRatio { get; set; }
        public decimal NetWeight { get; set; }
        public decimal TareWeight { get; set; }
        public Guid WeightUnitId { get; set; }
        public string WeightUnitCode { get; set; }
    }

    public class InfoData : EventArgs
    {
        public string Text { get; set; }
    }
}
