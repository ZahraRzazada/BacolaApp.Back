using System;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Service.Responses;

namespace Bacola.Service.Services.Interfaces
{
	public interface ITagService
	{

        public Task<IEnumerable<TagGetDto>> GetAllAsync();
        public Task<CustomResponse<Tag>> CreateAsync(TagPostDto dto);
        public Task<CustomResponse<Tag>> RemoveAsync(int id);
        public Task<CustomResponse<Tag>> UpdateAsync(int id, TagPostDto dto);
        public Task<CustomResponse<TagGetDto>> GetAsync(int id);
    }
}

