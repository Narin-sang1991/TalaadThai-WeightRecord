using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement
{
    public class ProcessPlanImportedData : ProcessPlanImportedSearchData
    {
        public ProcessPlanImportedData()
        {
            ProcessPlanItems = new List<ProcessPlanData>();
        }
        public List<ProcessPlanData> ProcessPlanItems { get; set; }
    }

    public class ProcessPlanImportedSearchData
    {
        public Guid Id { get; set; }
        public string No { get; set; }
        public DateTimeOffset? Date { get; set; }
        public string Remark { get; set; }
        public decimal ToTalCoilWeight { get; set; }
        public int ToTalCoilQty { get; set; }
    }

}
