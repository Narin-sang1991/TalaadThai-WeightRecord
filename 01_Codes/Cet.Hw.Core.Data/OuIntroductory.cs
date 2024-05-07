using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cet.EntityFramework.Adaptor;
using Cet.Hw.Core.Domain;

namespace Cet.Hw.Core.Data
{
    public class OuIntroductory : Introductory<OrganizationUnit, Guid>, IOuIntroductory
    {
        public OuIntroductory(HwUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }
    }
}
