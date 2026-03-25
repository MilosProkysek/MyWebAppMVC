using Microsoft.EntityFrameworkCore;
using MyWebAppMVC.Contracts.Common;
using MyWebAppMVC.Extensions;
using MyWebAppMVC.Repository;

namespace MyWebAppMVC.Service
{
    public class GenericService<T>(IGenericRepository<T> repository) : IGenericService<T> where T : class
    {
        public IEnumerable<T> GetAll()
            => repository.GetAll();
        public PagedResult<T> GetPaged(PaginationQuery query)
                    => repository.GetAllQueryable().ToPagedResult(query);

        public IQueryable<T> GetAllQueryable()
        => repository.GetAllQueryable();

        public T? GetById(int id)
            => repository.GetById(id);

        public T Create(T entity)
            => repository.Add(entity);

        public void Update(T entity)
            => repository.Update(entity);

        public void Delete(int id)
            => repository.Delete(id);

        public bool Exists(int id)
            => repository.Exists(id);

    }
}
