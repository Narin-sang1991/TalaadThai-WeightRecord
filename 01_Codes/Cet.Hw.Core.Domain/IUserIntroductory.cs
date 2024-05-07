using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cet.EntityFramework.Adaptor;

namespace Cet.Hw.Core.Domain
{
    public interface IUserIntroductory : IIntroductory<User, Guid>
    {
    }
}
