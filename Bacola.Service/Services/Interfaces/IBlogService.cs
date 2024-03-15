using System;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Service.Responses;

namespace Bacola.Service.Services.Interfaces
{
	public interface IBlogService
	{
        public Task<IEnumerable<BlogGetDto>> GetAllAsync();
        public Task<CustomResponse<Blog>> CreateAsync(BlogPostDto dto);
        public Task<CustomResponse<Blog>> RemoveAsync(int id);
        public Task<CustomResponse<Blog>> UpdateAsync(int id, BlogPostDto dto);
        public Task<CustomResponse<BlogGetDto>> GetAsync(int id);
        public Task<List<BlogGetDto>> GetBlogsBySearchTextAsync(string searchText);
    }
}

