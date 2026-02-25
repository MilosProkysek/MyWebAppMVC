using Microsoft.EntityFrameworkCore;
using MyWebAppMVC.Data;

namespace MyWebAppMVC.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // GET all entities
        public IEnumerable<T> GetAll()
            => _dbSet.ToList();

        // GET single entity by primary key
        public T? GetById(int id)
            => _dbSet.Find(id);

        // INSERT new entity
        public T Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        // UPDATE existing entity
        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        // DELETE entity by primary key
        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity is not null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        // CHECK if entity with given id exists
        public bool Exists(int id)
            => _dbSet.Find(id) is not null;
    }
}
