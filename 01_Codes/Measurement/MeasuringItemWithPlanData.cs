using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement
{
    public class MeasuringMoveItemData : WeightData
    {
        public Guid Id { get; set; }
        public Guid MeasuringId { get; set; }
        public Guid? ProcessPlanId { get; set; }
        public long SeqNo { get; set; }
        public decimal UnitPrice { get; set; }
        public GatewayItemType GatewayItemType { get; set; }
        public bool UpdateNameWithSameBarcode { get; set; }
        public decimal? CoilNetWeight { get; set; }
        public string Remark { get; set; }

    }
}
