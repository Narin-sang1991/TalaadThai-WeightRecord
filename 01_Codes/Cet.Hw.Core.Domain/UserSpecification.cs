using Cet.EntityFramework.Adaptor;
using System.Data.Entity.SqlServer;

namespace Cet.Hw.Core.Domain
{
    public static class UserSpecification
    {
        public static ISpecification<User> GetSpecification01(UserCriteria01 criteria)
        {
            Specification<User> spec = new TrueSpecification<User>();

            if (criteria.Id.HasValue)
                spec &= (t => t.Id == criteria.Id.Value);

            if (!string.IsNullOrEmpty(criteria.Login))
            {
                spec &= (t => SqlFunctions.PatIndex(criteria.Login, t.Login) > 0);
            }
            if (!string.IsNullOrEmpty(criteria.LoginLike))
            {
                criteria.LoginLike = criteria.LoginLike.Trim();
                spec &= (t => SqlFunctions.PatIndex("%" + criteria.LoginLike + "%", t.Login) > 0);
            }

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                spec &= (t => SqlFunctions.PatIndex("%" + criteria.Name + "%", t.Name) > 0);
            }

            if (!string.IsNullOrEmpty(criteria.Email))
            {
                spec &= (t => SqlFunctions.PatIndex("%" + criteria.Email + "%", t.Email) > 0);
            }
            if (!string.IsNullOrEmpty(criteria.EmailLike))
            {
                criteria.EmailLike = criteria.EmailLike.Trim();
                spec &= (t => SqlFunctions.PatIndex("%" + criteria.EmailLike + "%", t.Email) > 0);
            }

            if (criteria.IsGroup.HasValue)
            {
                spec &= (t => t.IsGroup == criteria.IsGroup);
            }

            if (criteria.IsActive.HasValue)
            {
                spec &= (t => t.IsActive == criteria.IsActive);
            }


            if (!string.IsNullOrEmpty(criteria.AutoCompleteText))
                spec &= (t => t.Email.Contains(criteria.AutoCompleteText)
                || t.Login.Contains(criteria.AutoCompleteText)
                || t.Name.Contains(criteria.AutoCompleteText));

            return spec;
        }
    }
}
