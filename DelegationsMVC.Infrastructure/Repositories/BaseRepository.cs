using DelegationsMVC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DelegationsMVC.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly Context _context;
        public BaseRepository(Context context)
        {
            _context = context;
        }
        public void AddEntity(T entityToAdd)
        {
            _context.Set<T>().Add(entityToAdd);
            _context.SaveChanges();
        }

        public void AddRange(IEnumerable<T> entitiesToAdd)
        {
            _context.Set<T>().AddRange(entitiesToAdd);
            _context.SaveChanges();
        }

        public IQueryable<T> GetAllEntitites()
        {
            return _context.Set<T>();
        }

        public T GetEntityByExpression(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().FirstOrDefault(expression);
        }

        public T GetEntityById(int entityId)
        {
            return _context.Set<T>().Find(entityId);
        }

        public void RemoveEntity(int entityToRemoveId)
        {
            var entityToRemove = GetEntityById(entityToRemoveId);
            if(entityToRemove != null)
            {
                _context.Set<T>().Remove(entityToRemove);
                _context.SaveChanges();
            }
        }

        public void RemoveRange(IEnumerable<T> entitiesToRemove)
        {
            _context.Set<T>().RemoveRange(entitiesToRemove);
            _context.SaveChanges();
        }
    }
}
