using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.EntityFramework.Adaptor
{
    public interface IServiceBase<TEntity, data>
         where TEntity : EntityBase
    {

        TEntity Save(data data);

        TEntity CreateModelRequest();

        TEntity AddInternal(data data);

        void UpdateInternal(TEntity domain, data data);

        void Remove(Guid id);

    }
}
