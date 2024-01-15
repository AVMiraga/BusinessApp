using AutoMapper;
using BusinessApp.Business.Helpers;
using BusinessApp.Business.Services.Interfaces;
using BusinessApp.Business.ViewModels;
using BusinessApp.Core.Entities;
using BusinessApp.DAL.Repository.Interfaces;
using FluentValidation.Results;
using Microsoft.AspNetCore.Hosting;

namespace BusinessApp.Business.Services.Implementation
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repo;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public BlogService(IBlogRepository repo, IMapper mapper, IWebHostEnvironment env)
        {
            _repo = repo;
            _mapper = mapper;
            _env = env;
        }

        public async Task CreateAsync(CreateBlogVm vm)
        {
            Blog blog = _mapper.Map<Blog>(vm);

            string imageUrl = await vm.ImageFile.UploadFile(_env.WebRootPath, "Upload");

            blog.ImageUrl = imageUrl;

            await _repo.CreateAsync(blog);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Blog blog = await _repo.GetByIdAsync(id);

            _repo.Delete(blog);
            await _repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task UpdateAsync(UpdateBlogVm vm)
        {
            Blog blog = await _repo.GetByIdAsync(vm.Id);

            _mapper.Map(vm, blog);

            if(vm.ImageFile != null) 
            {
                string FileName = await vm.ImageFile.UploadFile(_env.WebRootPath, "Upload");

                blog.ImageUrl = FileName;
            }

            _repo.Update(blog);
            await _repo.SaveChangesAsync();
        }
    }
}
