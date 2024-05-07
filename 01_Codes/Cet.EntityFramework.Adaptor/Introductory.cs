using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Cet.Core.Logging;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Collections;

namespace Cet.EntityFramework.Adaptor
{
    public class Introductory<TEntity, Tkey> : IIntroductory<TEntity, Tkey> where TEntity : EntityBase
    {
        QueryableUnitOfWork _UnitOfWork;

        protected Introductory(QueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            _UnitOfWork = unitOfWork;
        }


        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _UnitOfWork;
            }
        }

        public virtual void Add(TEntity item)
        {

            if (item != (TEntity)null)
                GetSet().Add(item); // add new item in this set
            else
            {
                Logger.Write(
                    String.Format(Messages.CannotAddNullEntity, typeof(TEntity).ToString()),
                    IntroductoryLogCategory.General,
                    (int)Priority.High,
                    (int)IntroductoryGeneralLogCategoryEvent.CannotAddNullEntity,
                    global::System.Diagnostics.TraceEventType.Warning);
            }
        }

        protected virtual void Remove<T>(T item) where T : class
        {
            if (item != (T)null)
            {
                var set = _UnitOfWork.CreateSet<T>();
                if (!set.Local.Contains(item)) return;

                //set as "removed"
                set.Remove(item);
            }
            else
            {
                Logger.Write(
                    String.Format(Messages.CannotRemoveNullEntity, typeof(T).ToString()),
                    IntroductoryLogCategory.General,
                    (int)Cet.Core.Logging.Priority.High,
                    (int)IntroductoryGeneralLogCategoryEvent.CannotRemoveNullEntity,
                    global::System.Diagnostics.TraceEventType.Warning);
            }

        }

        public virtual void Remove(TEntity item)
        {
            Remove<TEntity>(item);
        }

        public virtual void TrackItem(TEntity item)
        {
            if (item != (TEntity)null)
                _UnitOfWork.Attach(item);
            else
            {
                Logger.Write(
                    String.Format(Messages.CannotRemoveNullEntity, typeof(TEntity).ToString()),
                    IntroductoryLogCategory.General,
                    (int)Priority.High,
                    (int)IntroductoryGeneralLogCategoryEvent.CannotRemoveNullEntity,
                    global::System.Diagnostics.TraceEventType.Warning);

            }
        }

        public virtual void Modify(TEntity item)
        {
            if (item != (TEntity)null)
                _UnitOfWork.SetModified(item);
            else
            {
                Logger.Write(
                     String.Format(Messages.CannotRemoveNullEntity, typeof(TEntity).ToString()),
                     IntroductoryLogCategory.General,
                     (int)Priority.High,
                     (int)IntroductoryGeneralLogCategoryEvent.CannotRemoveNullEntity,
                     global::System.Diagnostics.TraceEventType.Warning);
            }
        }

        public virtual TEntity Get(Tkey id)
        {
            if (id != null)
                return GetSet().Find(id);
            else
                return null;
        }

        public virtual TEntity2 Get<TEntity2>(Tkey id) where TEntity2 : TEntity
        {
            return _UnitOfWork.CreateSet<TEntity2>().Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return GetSet().AsQueryable<TEntity>();
        }

        public virtual void Merge(TEntity persisted, TEntity current)
        {
            _UnitOfWork.ApplyCurrentValues(persisted, current);
        }

        IDbSet<TEntity> GetSet()
        {
            return _UnitOfWork.CreateSet<TEntity>();
        }

        public IQueryable<TProperty> GetQuery<TProperty>(TEntity obj, Expression<Func<TEntity, TProperty>> navigationProperty)
            where TProperty : class
        {
            return _UnitOfWork.GetQuery(obj, navigationProperty);
        }

        public IQueryable<TProperty> GetQuery<TProperty>(TEntity obj, Expression<Func<TEntity, ICollection<TProperty>>> navigationProperty)
            where TProperty : class
        {
            return _UnitOfWork.GetQuery(obj, navigationProperty);
        }

        public IEnumerable SqlQuery(Type elementType, string sql, params object[] parameters)
        {
            return _UnitOfWork.Database.SqlQuery(elementType, sql, parameters);
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return _UnitOfWork.Database.SqlQuery<TElement>(sql, parameters);
        }
    }

}
