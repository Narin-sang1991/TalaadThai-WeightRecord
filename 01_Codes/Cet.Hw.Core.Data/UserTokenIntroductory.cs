using Cet.EntityFramework.Adaptor;
using Cet.Hw.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Data
{
    public class UserTokenIntroductory : Introductory<UserToken,Guid>, IUserTokenIntroductory
    {
        public UserTokenIntroductory(HwUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }
    }
}
