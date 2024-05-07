using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.EntityFramework.Adaptor
{

    public interface IDomainEvent { }


    public interface IHandles<T> where T : IDomainEvent
    {
        void Handle(T args);
    }

}
