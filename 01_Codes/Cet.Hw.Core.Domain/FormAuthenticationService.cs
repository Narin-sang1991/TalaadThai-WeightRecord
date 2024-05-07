using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Cet.Core.Data;
using System.Security.Cryptography;
using System.Runtime.Caching;
using Cet.Core;

namespace Cet.Hw.Core.Domain
{
    public class FormAuthenticationService : AuthenticationServiceBase, IDomainAuthenticationService
    {
        const string AddEventLogin = "Core0020";
        const string ClearOldUserTokenEvent = "Core0022";

        [Dependency("AllowMultipleSignIn")]
        public bool AllowMultipleSignIn { get; set; }

        [Dependency]
        public IUserIntroductory UserIntroductory { get; set; }

        public FormAuthenticationService(IUnityContainer container)
            : base(container)
        {

        }

        public LoginResult Login(string login, string password, string realm = null)
        {
            password = MD5.Create().ComputeHash(password);

            //IUserIntroductory UserIntroductory = Container.Resolve<IUserIntroductory>(); 
            var users = UserIntroductory.GetAll().Where(t => (t.Login.Equals(login) && t.Password.Equals(password)) && t.IsActive == true && t.IsGroup == false);

            if (!string.IsNullOrEmpty(realm))
            {
                users = users.Where(t => t.Realm == null || t.Realm == realm);
            }

            if (users.Count() == 0) return new LoginResult() { LoginResultCode = LoginResultCode.Invalid };
            Guid userId = users.Select(t => t.Id).First();

            var userTokenIntroductory = Container.Resolve<IUserTokenIntroductory>();
            UserToken userToken = userTokenIntroductory.Get(userId);

            // GET IP
            string ipAddress = string.Empty;
            if (this.Container.IsRegistered<ISecurityIPAddressProvider>())
            {
                var ipAddressProvider = this.Container.Resolve<ISecurityIPAddressProvider>();
                ipAddress = ipAddressProvider.GetIPAddress();
            }

            //AuditLogService service = Container.Resolve<AuditLogService>();

            if (!AllowMultipleSignIn && userToken != null)
            {
                ObjectCache cache = Container.Resolve<ObjectCache>();
                cache.Remove(userToken.Token);

                //service.AddLog(ClearOldUserTokenEvent, new object[] { login, ipAddress });
            }

            UserProfile profile = CreateUserProfile(userId);
            if (!string.IsNullOrEmpty(realm))
            {
                profile.UpdateRealm(realm);
            }

            string token = SaveUserProfile(profile);

            if (!AllowMultipleSignIn)
            {
                if (userToken == null)
                {
                    userToken = new UserToken(userId);
                    userToken.SetToken(token);
                    userTokenIntroductory.Add(userToken);
                }
                else
                {
                    userToken.SetToken(token);
                }
            }

            //service.AddLog(AddEventLogin, new object[] { login, ipAddress });
            UserIntroductory.UnitOfWork.Commit();

            return new LoginResult { LoginResultCode = LoginResultCode.Success, Token = token };
        }

        //public bool CheckPasswordExpire(string loginName)
        //{
        //    AuthenticationServiceBase service = this.Container.Resolve<AuthenticationServiceBase>();
        //    IUserRepository repository = Container.Resolve<IUserRepository>();
        //    var user = repository.GetAll().Where(t => t.Login == loginName).FirstOrDefault();
        //    if (DateTime.Now >= user.PasswordExpireDate)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        public bool CheckCurrentUserPassword(string password)
        {
            UserProfile userProfile = GetCurrentUserProfile(true);
            var userRepostiory = this.Container.Resolve<IUserIntroductory>();
            User user = userRepostiory.Get(userProfile.UserId);

            if (user.Password == MD5.Create().ComputeHash(password))
                return true;

            return false;
        }

        public LoginResult Login(string token)
        {
            throw new NotImplementedException();
        }
    }
}
