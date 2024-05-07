using Cet.Core;
using Cet.Core.Data;
using Cet.Hw.Core.Domain;
using Cet.Hw.Core;
using Cet.EntityFramework.Adaptor;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Cet.Hw.Core.Domain
{
    public class UserServiceBase
    {
        protected IUnityContainer Container { get; set; }

        public IUserIntroductory UserIntroductory { get; set; }

        public UserServiceBase(IUnityContainer container)
        {
            Container = container;
            UserIntroductory = Container.Resolve<IUserIntroductory>();
        }

        #region Save
        public User Save(UserData data)
        {
            var user = UserIntroductory.Get(data.UserId);
            if (user == null)
            {
                Validate(data);
                if (UserIntroductory.GetAll().Where(t => t.Login == data.Login.Trim()).Count() > 0)
                {
                    throw new FaultException<DataValidationException>(new DataValidationException() { Message = "User [" + data.Login.Trim() + "] has been exist !" });
                }
                user = AddInternal(data);
                UserIntroductory.Add(user);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(data.Password))
                    Validate(data);

                UpdateInternal(user, data);
            }

            return user;
        }

        protected User CreateModelRequest()
        {
            var user = new User();
            Container.BuildUp(user.GetType(), user);
            user.BuildUp(Container);

            return user;
        }

        protected User AddInternal(UserData data)
        {
            var user = CreateModelRequest();
            UpdateInternal(user, data);
            return user;
        }

        protected void UpdateInternal(User domain, UserData data)
        {
            domain.SetUserData(data);
            if (data.ParentId.HasValue)
            {
                var parent = UserIntroductory.Get(data.ParentId.Value);
                // In case --> data not use.
                UpdateUserData(parent, data, domain);
            }
        }

        protected void UpdateUserData(User user, UserData data, User childUser = null)
        {
            if (user == null) throw new FaultException<DataValidationException>(new DataValidationException() { Message = "USer/Group not found!" });

            if (childUser != null)
                user.AddChildUser(childUser);
            else
            {
                user.SetLoginPassword(data.Login, data.Password);
                user.SetEMail(data.Email);
                user.SetIsActive(data.IsActive);
            }
        }

        public void Remove(Guid id)
        {
            User user = UserIntroductory.Get(id);
            if (user != null)
                UserIntroductory.Remove(user);
        }

        private void Validate(UserData request)
        {
            bool requireUpperChar = Container.Resolve<bool>("RequireUpperChar"); // get from Core.Configuration
            bool requireLower = Container.Resolve<bool>("RequireLowerChar");  // get from Core.Configuration
            bool requireNumeric = Container.Resolve<bool>("RequireNumeric");  // get from Core.Configuration
            bool requireSpecial = Container.Resolve<bool>("RequireSpecialChar");  // get from Core.Configuration
            int passwordLength = Container.Resolve<byte>("PasswordLength");
            if (String.IsNullOrEmpty(request.Password))
            {
                throw new FaultException<DataValidationException>(new DataValidationException() { Message = "Password must be at least " + passwordLength + " characters long" });
            }
            bool valid = PasswordHelper.CheckValidPassword(request.Password, requireUpperChar, requireLower, requireNumeric, requireSpecial);
            if (!valid)
            {
                throw new FaultException<DataValidationException>(new DataValidationException() { Message = "This password invalid format." });
            }

            if (request.Password.Length < passwordLength)
            {
                throw new FaultException<DataValidationException>(new DataValidationException() { Message = "Password must be at least " + passwordLength + " characters long" });
            }

        }
        #endregion

        #region Search
        public UserData Get(Guid id)
        {
            var user = UserIntroductory.Get(id);
            return userToUserData01(user);
        }

        public IList<UserData> FindUserInternal(UserCriteria01 criteria, SortingCriteria sortingCriteria, PagingCriteria pagingCriteria)
        {
            var userQuery = UserQuery(criteria);

            if (sortingCriteria != null) userQuery = userQuery.OrderBy(sortingCriteria);
            if (pagingCriteria != null) userQuery = userQuery.Page(pagingCriteria);

            return userQuery.Select(userToUserData01).ToList();
        }

        Func<User, UserData> userToUserData01 = t => new UserData()
        {
            UserId = t.Id,
            Email = t.Email,
            Login = t.Login,
            Name = t.Name,
            IsGroup = t.IsGroup,
            IsActive = t.IsActive.HasValue ? t.IsActive.Value : false,
            UserUID = t.UserUID,
            CreatedDate = t.MustData.CreatedDate,
            UpdatedDate = t.MustData.UpdatedDate
        };

        public int CountUserInternal(UserCriteria01 criteria)
        {
            return UserQuery(criteria).Count();
        }

        public IList<BaseStdData> FindUserFromParent(Guid parentId)
        {
            var user = UserIntroductory.GetAll();
            if (parentId == Guid.Empty)
                user = user.Where(t => t.Path == null);
            else
                user = user.Where(t => t.Id == parentId).SelectMany(t => t.ChildMembers);

            user = user.Where(t => t.IsGroup == false).OrderBy(t => t.Name);
            return user.Select(userToBaseStdData1).ToList();
        }

        public IList<BaseStdData> FindParent(Guid parentId)
        {
            var parent = UserIntroductory.GetAll();
            if (parentId == Guid.Empty)
                parent = parent.Where(t => t.Path == null);
            else
                parent = parent.Where(t => t.Id == parentId).SelectMany(t => t.ChildMembers);

            parent = parent.Where(t => t.IsGroup == true).OrderBy(t => t.Name);
            return parent.Select(userToBaseStdData1).ToList();
        }

        private IQueryable<User> UserQuery(UserCriteria01 criteria)
        {
            var userQuery = UserIntroductory.GetAll().Where(UserSpecification.GetSpecification01(criteria));
            if (criteria.ParentId.HasValue)
            {
                var childIds = UserIntroductory.GetAll().Where(t => t.Id == criteria.ParentId.Value)
                            .SelectMany(t => t.ChildMembers).Select(t => t.Id).ToList();
                userQuery = userQuery.Where(t => childIds.Contains(t.Id));
            }
            return userQuery;
        }

        Func<User, BaseStdData> userToBaseStdData1 = t => new BaseStdData()
        {
            Id = t.Id,
            Name = t.Name
        };

        #endregion

    }
}
