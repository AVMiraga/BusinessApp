using BusinessApp.Core.Entities.Common;
using BusinessApp.DAL.Context;
using BusinessApp.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessApp.DAL.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseAuditableEntity, new()
    {
        private readonly AppDbContext _context;
        public DbSet<T> Table => _context.Set<T>();

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(T entity)
        {
            DateTime time = DateTime.Now;

            entity.CreatedAt = time;
            entity.UpdatedAt = time;

            await Table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.Now;

            Table.Update(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IQueryable<T> query = Table.Where(e => !e.IsDeleted);

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            IQueryable<T> query = Table.Where(e => !e.IsDeleted);

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<int> SaveChangesAsync()
        {
            int res = await _context.SaveChangesAsync();

            return res;
        }

        public void Update(T entity)
        {
            entity.UpdatedAt = DateTime.Now;

            Table.Update(entity);
        }
    }
}
