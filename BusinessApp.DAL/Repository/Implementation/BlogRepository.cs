using BusinessApp.Core.Entities;
using BusinessApp.DAL.Context;
using BusinessApp.DAL.Repository.Interfaces;

namespace BusinessApp.DAL.Repository.Implementation
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        public BlogRepository(AppDbContext context) : base(context)
        {
        }
    }
}
