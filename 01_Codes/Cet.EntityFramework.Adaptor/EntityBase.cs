using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace Cet.EntityFramework.Adaptor
{
    public class EntityBase
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public EntityBase() { }

        public virtual void BuildUp(IUnityContainer parmContainer)
        {
            parmContainer.BuildUp(this);
        }

    }
}
