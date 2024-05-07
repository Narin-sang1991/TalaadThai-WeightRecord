using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement
{
    public class ProcessPlanData
    {
        public Guid Id { get; set; }
        public Int64 SeqNo { get; set; }
        public string PosNo { get; set; }
        public DateTimeOffset ProcessPlanDate { get; set; }
        public string ProcessMachineCode { get; set; }
        public string CoilNo { get; set; }
        public decimal CoilWeight { get; set; }
        public Guid? RelatedId { get; set; }
        public string Remark { get; set; }

        public string AutoCompleteDisplay
        {
            get { return  string.Format("[{0}] {1}", ProcessMachineCode, CoilNo); }
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                var lotObj = obj as ProcessPlanData;
                return this.Id == lotObj.Id;
            }
        }

    }
}
