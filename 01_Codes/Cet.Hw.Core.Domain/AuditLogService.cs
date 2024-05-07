using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace Cet.Hw.Core.Domain
{
    public class AuditLogService
    {
        IUnityContainer container;

        public AuditLogService(IUnityContainer container)
        {
            this.container = container;
        }

        public void AddLog(string messageCode, params Object[] args)
        {
            var repository = container.Resolve<IAuditLogMessageIntroductory>();
            var auditLogMessage = repository.Get(messageCode);

            string msg = "";
            if (auditLogMessage != null)
                msg = string.Format(auditLogMessage.Template, args);
            var AuditLogIntroductory = this.container.Resolve<IAuditLogIntroductory>();
            var auditLog = new AuditLog(messageCode);
            auditLog.SetMessage(msg);
            auditLog.SetEventDate(DateTime.Now);
            AuditLogIntroductory.Add(auditLog);
        }
    }
}
