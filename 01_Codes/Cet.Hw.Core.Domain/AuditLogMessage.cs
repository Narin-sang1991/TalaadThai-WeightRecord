using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Domain
{
    public class AuditLogMessage : HwEntity
    {
        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private int? levelNumber;
        public int? LevelNumber
        {
            get { return levelNumber; }
            set { levelNumber = value; }
        }

        private string template;
        public string Template
        {
            get { return template; }
            set { template = value; }
        }

        private string auditLogCategoryCode;
        public string AuditLogCategoryCode
        {
            get { return auditLogCategoryCode; }
            set { auditLogCategoryCode = value; }
        }

        private AuditLogCategory auditLogCategory;
        public virtual AuditLogCategory AuditLogCategory
        {
            get { return auditLogCategory; }
            set { auditLogCategory = value; }
        }

    }

}
