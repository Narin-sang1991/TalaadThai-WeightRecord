using Cet.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTalaadThaiWg.AppServiceContract
{
    public class MeasuringData : MeasuringSearchData
    {
        public MeasuringData()
        {
            MeasuringMoveItems = new List<MeasuringMoveItemData>();
        }
        public Guid? BusinessEntityId { get; set; }
        public List<MeasuringMoveItemData> MeasuringMoveItems { get; set; }
    }

    public class MeasuringSearchData
    {
        public Guid Id { get; set; }
        public string No { get; set; }
        public string ReferenceNo { get; set; }
        public DateTimeOffset? Date { get; set; }
        public MeasuringMoveType Type { get; set; }
        public string TypeDisplay
        {
            get
            {
                return this.Type.Translate();
            }
        }
        public MeasuringStatus Status { get; set; }
        public string StatusDisplay
        {
            get
            {
                return this.Status.Translate();
            }
        }
        public string LicensePlateNo { get; set; }
        public string Remark { get; set; }
        public decimal ToTalNetWeight { get; set; }
        public decimal ToTalTareWeight { get; set; }
        public bool IsSelected { get; set; }
    }

}
