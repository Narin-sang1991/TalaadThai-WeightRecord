using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.EntityFramework.Adaptor
{
    public interface IDbContextInitializer
    {
        void Initialize(DbModelBuilder modelBuilder);
    }
}
