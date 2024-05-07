using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cet.EntityFramework.Adaptor;
using Cet.Hw.Core.Domain;

namespace Cet.Hw.Core.Data
{
    public class UserIntroductory : Introductory<User, Guid>, IUserIntroductory
    {
        public UserIntroductory(HwUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }
    }
}
