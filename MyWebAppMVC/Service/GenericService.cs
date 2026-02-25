using Microsoft.EntityFrameworkCore;
using MyWebAppMVC.Data;

namespace MyWebAppMVC.Service
{
    public class GenericService<T>(ApplicationDbContext context) : IGenericService<T> where T : class
    {
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public IEnumerable<T> GetAll()
            => [.. _dbSet];

        public T? GetById(int id)
            => _dbSet.Find(id);

        public T Create(T entity)
        {
            _dbSet.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity is not null)
            {
                _dbSet.Remove(entity);
                context.SaveChanges();
            }
        }

        public bool Exists(int id)
            => _dbSet.Find(id) is not null;
    }
}
