using Cet.Hw.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Domain
{
    public class Menu : HwEntity
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string ParentCode { get; private set; }
        public Guid ResourceUID { get; private set; }
        public int Ordinary { get; private set; }
        public string Command { get; private set; }
        public bool? IsActive { get; private set; }
        public bool? IsHidden { get; private set; }

        public virtual ICollection<MenuTranslate> MenuTranslates { get; private set; }
    }
}
