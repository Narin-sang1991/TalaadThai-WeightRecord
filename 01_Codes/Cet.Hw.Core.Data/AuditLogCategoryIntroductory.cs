using Cet.EntityFramwork.Adaptor;
using Cet.Hw.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Data
{
    public class AuditLogCategoryIntroductory : Introductory<AuditLogCategory, string>, IAuditLogCategoryIntroductory
    {
        public AuditLogCategoryIntroductory(HwUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }
    }
}
