using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Domain
{
    public class ConfigurationCategory : HwEntity
    {
        protected ConfigurationCategory() { }

        public ConfigurationCategory(bool dummy = true)
        { }

        private Guid id;
        public Guid Id
        {
            get { return id; }
            private set { id = value; }
        }

        private string code;
        public string Code
        {
            get { return code; }
            private set { code = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        private bool isAccessWrite;
        public bool IsAccessWrite
        {
            get { return isAccessWrite; }
            private set { isAccessWrite = value; }
        }

    }
}
