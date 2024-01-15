using BusinessApp.Business.ViewModels;
using BusinessApp.Core.Entities;

namespace BusinessApp.Business.Services.Interfaces
{
    public interface IBlogService
    {
        Task<Blog> GetByIdAsync(int id);
        Task<IEnumerable<Blog>> GetAllAsync();
        Task CreateAsync(CreateBlogVm vm);
        Task UpdateAsync(UpdateBlogVm vm);
        Task DeleteAsync(int id);
    }
}
