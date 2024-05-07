using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement
{
    public class MeasuringMoveCriteria
    {
        public Guid? Id { get; set; }
        public Guid? MeasuringId { get; set; }
        public bool? IsDeleted { get; set; }
    }

}
