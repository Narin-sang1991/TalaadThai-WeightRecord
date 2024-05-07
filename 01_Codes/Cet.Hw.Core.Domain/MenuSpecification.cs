using Cet.EntityFramework.Adaptor;
using Cet.Hw.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.SqlServer;

namespace Cet.Hw.Core.Domain
{
    public class MenuSpecification
    {
        public static ISpecification<Menu> GetSpecification01(MenuCriteria01 criteria)
        {
            Specification<Menu> spec = new TrueSpecification<Menu>();
            if (!String.IsNullOrEmpty(criteria.Code))
            {
                spec &= (t => SqlFunctions.PatIndex(criteria.Code, t.Code) > 0);
            }

            if (!String.IsNullOrEmpty(criteria.ParentCode))
            {
                spec &= (t => t.ParentCode == criteria.ParentCode);
            }

            if (criteria.IsRootNode.HasValue)
            {
                if (criteria.IsRootNode.Value)
                    spec &= (t => t.ParentCode.Equals(null));
                else
                    spec &= (t => !t.ParentCode.Equals(null));
            }

            if (criteria.IsHidden.HasValue)
                spec &= (t => t.IsHidden == criteria.IsHidden);


            spec &= (t => t.IsActive == true);

            return spec;
        }
    }
}
