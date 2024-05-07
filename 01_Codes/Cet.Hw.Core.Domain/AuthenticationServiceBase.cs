using System.Runtime.Caching;
using Cet.Core;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Unity;
using System;
using System.Linq;
using System.ServiceModel;

namespace Cet.Hw.Core.Domain
{
    public class AuthenticationServiceBase
    {
        const string AddEventLogout = "Core0021";

        protected IUnityContainer Container { get; private set; }
        //string Anonymous = "Anonymous";
        //string Authenticated = "Authenticated";
        //string Everyone = "Everyone";

        public AuthenticationServiceBase(IUnityContainer container)
        {
            this.Container = container;
        }

        protected virtual bool IsAnonymousProfile(UserProfile userProfile)
        {
            return userProfile.Name == string.Empty;
        }

        public bool PermissionAllow(Guid userId, Guid groupId)
        {
            var userIntroductory = Container.Resolve<IUserIntroductory>();
            var result = userIntroductory.GetAll().Where(t => t.Id == groupId && t.IsGroup == true && t.IsActive == true)
                .SelectMany(t => t.ChildMembers).Where(t2 => t2.Id == userId && t2.IsGroup == false && t2.IsActive == true).Count();
            return result > 0;
        }

        public UserProfile GetCurrentUserProfile(bool isThrowEx = true)
        {
            if (Container.IsRegistered<UserProfile>("current"))
            {
                var currentProfile = Container.Resolve<UserProfile>("current");
                if (!IsAnonymousProfile(currentProfile))
                    return currentProfile;

            }


            ISecurityTokenProvider provider = Container.Resolve<ISecurityTokenProvider>();
            string token = provider.GetToken();
            if (String.IsNullOrEmpty(token))
                return CreateAnonymousProfile();

            //GuidToken tokenKey = new GuidToken(new Guid(token));
            //ISecurityCacheProvider secCache = SecurityCacheFactory.GetSecurityCacheProvider("Security Cache");
            //UserProfile profile = secCache.GetProfile(tokenKey) as UserProfile;
            ObjectCache cache = Container.Resolve<ObjectCache>();
            UserProfile profile = cache.Get(token) as UserProfile;

            if (isThrowEx && profile == null)
                throw new FaultException<UserNotLoginException>(new UserNotLoginException());

            if (profile != null && !IsAnonymousProfile(profile))
            {
                Container.RegisterInstance(typeof(UserProfile), "current", profile, new HierarchicalLifetimeManager());
            }

            return profile;
        }

        protected UserProfile CreateAnonymousProfile()
        {
            UserProfile profile = new UserProfile(Guid.Empty, null, String.Empty, string.Empty);
            return profile;
        }

        protected UserProfile CreateUserProfile(Guid userId)
        {
            IUserIntroductory repository = Container.Resolve<IUserIntroductory>();
            var user = repository.Get(userId);
            UserProfile profile = new UserProfile(userId, user.UserUID, user.Name, user.Realm);
            return profile;
        }

        protected string SaveUserProfile(UserProfile profile)
        {
            ObjectCache cache = Container.Resolve<ObjectCache>();
            //ISecurityCacheProvider secCache = SecurityCacheFactory.GetSecurityCacheProvider("Security Cache");
            //string token = secCache.SaveProfile(profile).Value;
            int minuteInactiveTime = Container.Resolve<byte>("MinuteInactiveTime");
            Logger.Write("minuteInactiveTime : " + minuteInactiveTime);
            string token = Guid.NewGuid().ToString();
            var cacheItemPolicy = new CacheItemPolicy();
            cacheItemPolicy.AbsoluteExpiration = ObjectCache.InfiniteAbsoluteExpiration;
            cacheItemPolicy.SlidingExpiration = new TimeSpan(0, minuteInactiveTime, 0);
            cache.Add(token, profile, cacheItemPolicy);
            ISecurityTokenProvider provider = Container.Resolve<ISecurityTokenProvider>();
            provider.SaveToken(token);
            return token;

        }

        public void Logout()
        {

            ISecurityTokenProvider provider = Container.Resolve<ISecurityTokenProvider>();
            string token = provider.GetToken();
            if (String.IsNullOrEmpty(token)) return;

            UserProfile userProfile = this.GetCurrentUserProfile(false);

            if (userProfile != null)
            {
                IUserIntroductory repository = Container.Resolve<IUserIntroductory>();
                var user = repository.Get(userProfile.UserId);

                string ipAddress = string.Empty;
                if (this.Container.IsRegistered<ISecurityIPAddressProvider>())
                {
                    var ipAddressProvider = this.Container.Resolve<ISecurityIPAddressProvider>();
                    ipAddress = ipAddressProvider.GetIPAddress();
                }

                //AuditLogService service = Container.Resolve<AuditLogService>();
                //service.AddLog(AddEventLogout, new object[] { user.Login, ipAddress });
                repository.UnitOfWork.Commit();
            }

            //GuidToken tokenKey = new GuidToken(new Guid(token));
            //ISecurityCacheProvider secCache = SecurityCacheFactory.GetSecurityCacheProvider("Security Cache");
            //secCache.ExpireProfile(tokenKey);
            provider.ClearToken();
            ObjectCache cache = Container.Resolve<ObjectCache>();
            cache.Remove(token);

            if (Container.IsRegistered<UserProfile>("current"))
            {
                Container.RegisterInstance(typeof(UserProfile), "current", CreateAnonymousProfile(), new HierarchicalLifetimeManager());
            }
        }

        public void UpdateRealm(string realm)
        {
            ISecurityTokenProvider provider = Container.Resolve<ISecurityTokenProvider>();
            string token = provider.GetToken();
            if (String.IsNullOrEmpty(token))
                return;

            ObjectCache cache = Container.Resolve<ObjectCache>();
            UserProfile profile = cache.Get(token) as UserProfile;
            profile.UpdateRealm(realm);

            int minuteInactiveTime = Container.Resolve<byte>("MinuteInactiveTime");

            var cacheItemPolicy = new CacheItemPolicy();
            cacheItemPolicy.AbsoluteExpiration = ObjectCache.InfiniteAbsoluteExpiration;
            cacheItemPolicy.SlidingExpiration = new TimeSpan(0, minuteInactiveTime, 0);

            cache.Set(token, profile, cacheItemPolicy);
        }
    }
}
