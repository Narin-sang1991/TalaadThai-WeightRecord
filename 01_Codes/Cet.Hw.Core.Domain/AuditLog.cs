using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Domain
{
    public class AuditLog : HwEntity
    {
        protected AuditLog() { }
        public AuditLog(string iAuditLogMessageCode)
        {
            this.Id = Guid.NewGuid();
            this.AuditLogMessageCode = iAuditLogMessageCode;
        }


        private Guid id;
        public Guid Id
        {
            get { return id; }
            private set { id = value; }
        }

        private string auditLogMessageCode;
        public string AuditLogMessageCode
        {
            get { return auditLogMessageCode; }
            private set { auditLogMessageCode = value; }
        }

        private string message;
        public string Message
        {
            get { return message; }
            private set { message = value; }
        }

        private DateTime eventDate;
        public DateTime EventDate
        {
            get { return eventDate; }
            private set { eventDate = value; }
        }

        private AuditLogMessage auditLogMessage;
        public virtual AuditLogMessage AuditLogMessage
        {
            get { return auditLogMessage; }
            private set { auditLogMessage = value; }
        }


        public void SetMessage(string iMessage)
        {
            this.Message = iMessage;
        }

        public void SetEventDate(DateTime iEventDate)
        {
            this.EventDate = iEventDate;
        }

    }
}
