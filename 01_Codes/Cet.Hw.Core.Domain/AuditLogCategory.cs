using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Domain
{
    public class AuditLogCategory : HwEntity
    {
        private string code;
        public string Code
        { get { return code; } set { code = value; } }

        private string name;
        public string Name
        { get { return name; } set { name = value; } }
    }
}
