using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DelegationsMVC.Domain.Interfaces
{
    public interface IBaseRepository<T> where T: class
    {
        void AddEntity(T entityToAdd);
        void RemoveEntity(int entityToRemoveId);
        T GetEntityById(int entityId);
        void AddRange(IEnumerable<T> entitiesToAdd);
        void RemoveRange(IEnumerable<T> itemsToRemove);
        T GetEntityByExpression(Expression<Func<T, bool>> expression);
        IQueryable<T> GetAllEntitites();
    }
}
