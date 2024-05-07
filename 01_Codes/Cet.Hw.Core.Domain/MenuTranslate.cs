using Cet.Hw.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Domain
{
    public class MenuTranslate : HwEntity
    {
        private string code;
        public string Code { get { return code; } private set { code = value; } }

        private string name;
        public string Name { get { return name; } private set { name = value; } }

        private string languageCode;
        public string LanguageCode { get { return languageCode; } private set { languageCode = value; } }

    }
}
