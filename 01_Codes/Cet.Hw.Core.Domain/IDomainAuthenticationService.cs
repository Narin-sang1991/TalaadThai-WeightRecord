using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Domain
{
    public interface IDomainAuthenticationService
    {
        LoginResult Login(string login, string password, string realm = null);
        LoginResult Login(string token);
        //bool CheckPasswordExpire(string loginName);

        // Base
        UserProfile GetCurrentUserProfile(bool isThrowEx = true);
        bool PermissionAllow(Guid userId, Guid groupId);
        void Logout();
    }
}
