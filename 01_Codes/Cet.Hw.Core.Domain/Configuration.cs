using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Domain
{
    public class Configuration : HwEntity
    {
        protected Configuration() { }

        public Configuration(bool dummy = true)
        { }

        private Guid id;
        public Guid Id
        {
            get { return id; }
            private set { id = value; }
        }

        private Guid configCategoryId;
        public Guid ConfigCategoryId
        {
            get { return configCategoryId; }
            private set { configCategoryId = value; }
        }

        private ConfigurationCategory category;
        public virtual ConfigurationCategory Category
        {
            get { return category; }
            private set { category = value; }
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

        private string description;
        public string Description
        {
            get { return description; }
            private set { description = value; }
        }

        private bool allowClientAccess;
        public bool AllowClientAccess
        {
            get { return allowClientAccess; }
            private set { allowClientAccess = value; }
        }

        private string configurationValue;
        public string ConfigurationValue
        {
            get { return configurationValue; }
            private set { configurationValue = value; }
        }

    }
}
