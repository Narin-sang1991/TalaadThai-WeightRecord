using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections;

namespace Cet.EntityFramework.Adaptor
{
    public interface IIntroductory<TEntity, Tkey> where TEntity : EntityBase
    {
        IUnitOfWork UnitOfWork { get; }
        void Add(TEntity item);
        void Remove(TEntity item);
        void TrackItem(TEntity item);
        void Merge(TEntity persisted, TEntity current);
        TEntity Get(Tkey id);
        TEntity2 Get<TEntity2>(Tkey id) where TEntity2 : TEntity;
        IQueryable<TEntity> GetAll();
        IQueryable<TProperty> GetQuery<TProperty>(TEntity obj, Expression<Func<TEntity, TProperty>> navigationProperty)
            where TProperty : class;
        IQueryable<TProperty> GetQuery<TProperty>(TEntity obj, Expression<Func<TEntity, ICollection<TProperty>>> navigationProperty)
            where TProperty : class;
        IEnumerable SqlQuery(Type elementType, string sql, params object[] parameters);
        IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters);
    }
}
