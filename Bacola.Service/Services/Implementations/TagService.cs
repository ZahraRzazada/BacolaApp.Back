using System;
using AutoMapper;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Service.Responses;
using Bacola.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bacola.Service.Services.Implementations
{
    public class TagService : ITagService
    {
        readonly ITagRepository _tagRepository;
        readonly IMapper _mapper;

        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }
        public async Task<CustomResponse<Tag>> CreateAsync(TagPostDto dto)
        {

            bool tagExists = await _tagRepository.GetQuery(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower() && !x.IsDeleted).AnyAsync();
            if (tagExists)
            {
                return new CustomResponse<Tag> { IsSuccess = false, Message = $"Tag '{dto.Name}' already exists in the database", Data = null };
            }
            Tag tag = _mapper.Map<Tag>(dto);
            await _tagRepository.AddAsync(tag);
            await _tagRepository.SaveChangesAsync();
            return new CustomResponse<Tag> { IsSuccess = true, Message = $"{tag.Name} is created successfully", Data = tag };
        }
        public async Task<IEnumerable<TagGetDto>> GetAllAsync()
        {
            IEnumerable<TagGetDto> Tags = await _tagRepository.GetQuery(x => !x.IsDeleted)
            .AsNoTrackingWithIdentityResolution().Select(x => new TagGetDto { Name = x.Name, Id = x.Id, CreatedAt = x.CreatedAt }).ToListAsync();
            return Tags;
        }
        public async Task<CustomResponse<TagGetDto>> GetAsync(int id)
        {
            Tag tag = await _tagRepository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (tag == null)
            {
                return new CustomResponse<TagGetDto> { IsSuccess = false, Message = "This tag doesnt exist" };
            }
            TagGetDto tagGetDto = new TagGetDto
            {
                CreatedAt = tag.CreatedAt,
                Name = tag.Name,
                Id = tag.Id
            };
            return new CustomResponse<TagGetDto> { IsSuccess = true, Data = tagGetDto };
        }

        public async Task<CustomResponse<Tag>> RemoveAsync(int id)
        {
            Tag tag = await _tagRepository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (tag == null)
            {
                return new CustomResponse<Tag> { IsSuccess = false, Message = "This tag doesnt exist" };
            }
            tag.IsDeleted = true;
            await _tagRepository.UpdateAsync(tag);
            await _tagRepository.SaveChangesAsync();
            return new CustomResponse<Tag> { IsSuccess = true, Message = $"{tag.Name} is removed successfully", Data = tag };
        }

        public async Task<CustomResponse<Tag>> UpdateAsync(int id, TagPostDto dto)
        {
            Tag tag = await _tagRepository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (tag == null)
            {
                return new CustomResponse<Tag> { IsSuccess = false, Message = "This tag doesnt exist" };
            }
            tag.Name = dto.Name;
            await _tagRepository.UpdateAsync(tag);
            await _tagRepository.SaveChangesAsync();
            return new CustomResponse<Tag> { IsSuccess = true, Message = $"{tag.Name} is updated successfully", Data = tag };
        }
    }
}

