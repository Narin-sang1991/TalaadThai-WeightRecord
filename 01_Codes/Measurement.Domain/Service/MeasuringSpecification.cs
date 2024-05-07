using Cet.EntityFramework.Adaptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.SqlServer;

namespace Measurement.Domain
{
    public class MeasuringSpecification
    {
        public static ISpecification<Measuring> GetSpecification(MeasuringCriteria criteria)
        {

            Specification<Measuring> spec = new TrueSpecification<Measuring>();

            if (criteria.Id.HasValue)
                spec &= (t => t.Id == criteria.Id);

            if (!String.IsNullOrWhiteSpace(criteria.No))
            {
                var no = criteria.No.Trim();
                spec &= (t => SqlFunctions.PatIndex("%" + no + "%", t.No) > 0);
            }

            if (!String.IsNullOrWhiteSpace(criteria.ReferenceNo))
            {
                var refNo = criteria.ReferenceNo.Trim();
                spec &= (t => SqlFunctions.PatIndex("%" + refNo + "%", t.ReferenceNo) > 0);
            }

            if (criteria.BusinessEntityId.HasValue)
                spec &= (t => t.BusinessEntityId.HasValue && t.BusinessEntityId == criteria.BusinessEntityId);

            if (criteria.FromDate.HasValue)
            {
                var startDate = new DateTimeOffset(criteria.FromDate.Value);
                spec &= (t => t.Date >= startDate || !t.Date.HasValue);
            }

            if (criteria.ToDate.HasValue)
            {
                var toDate = new DateTimeOffset(criteria.ToDate.Value.AddDays(1));
                spec &= (t => t.Date < toDate || !t.Date.HasValue);
            }

            if (criteria.Status.HasValue)
                spec &= (t => t.StatusValue == (int)criteria.Status.Value);

            if (criteria.Type.HasValue)
                spec &= (t => t.TypeValue == (int)criteria.Type.Value);

            return spec;
        }

    }
}
