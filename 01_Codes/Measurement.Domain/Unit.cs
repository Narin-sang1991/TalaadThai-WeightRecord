using Cet.Hw.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measurement.Domain
{
    public class Unit : HwEntity
    {
        protected Unit() { }

        public Unit(string abbr)
        {
            Id = Guid.NewGuid();
            Abbreviation = abbr;
        }

        private Guid id;
        public Guid Id
        {
            get { return id; }
            private set { id = value; }
        }

        private string abbreviation;
        public string Abbreviation
        {
            get { return abbreviation; }
            private set { abbreviation = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            private set { isActive = value; }
        }
    }
}
