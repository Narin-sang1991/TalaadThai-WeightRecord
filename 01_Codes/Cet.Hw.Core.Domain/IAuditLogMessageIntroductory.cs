using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cet.EntityFramwork.Adaptor;

namespace Cet.Hw.Core.Domain
{
    public interface IAuditLogMessageIntroductory : IIntroductory<AuditLogMessage, string>
    {
    }
}
