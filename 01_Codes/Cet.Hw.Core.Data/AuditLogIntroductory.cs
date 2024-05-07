using Cet.EntityFramwork.Adaptor;
using Cet.Hw.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Data
{
    public class AuditLogIntroductory : Introductory<AuditLog, Guid>, IAuditLogIntroductory
    {
        public AuditLogIntroductory(HwUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }
    }
}
