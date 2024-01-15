using BusinessApp.Core.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace BusinessApp.DAL.Repository.Interfaces
{
    public interface IRepository<T> where T : BaseAuditableEntity, new()
    {
        public DbSet<T> Table { get; }
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task CreateAsync(T entity);
        void Delete(T entity);
        void Update(T entity);

        Task<int> SaveChangesAsync();
    }
}
