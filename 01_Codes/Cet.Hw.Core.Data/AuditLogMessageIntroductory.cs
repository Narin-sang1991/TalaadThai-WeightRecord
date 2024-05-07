using Cet.EntityFramwork.Adaptor;
using Cet.Hw.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Data
{
    public class AuditLogMessageIntroductory : Introductory<AuditLogMessage, string>, IAuditLogMessageIntroductory
    {
        public AuditLogMessageIntroductory(HwUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }
    }
}
