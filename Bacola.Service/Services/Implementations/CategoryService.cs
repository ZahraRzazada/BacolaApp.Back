using System;
using System.Reflection.Metadata;
using AutoMapper;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Data.Repositories;
using Bacola.Service.Responses;
using Bacola.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bacola.Service.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        readonly ICategoryRepository _categoryRepository;
        readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<CustomResponse<Category>> CreateAsync(CategoryPostDto dto)
        {
            bool categoryExists = await _categoryRepository.GetQuery(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower() && !x.IsDeleted).AnyAsync();
            if (categoryExists)
            {
                return new CustomResponse<Category> { IsSuccess = false, Message = $"Category '{dto.Name}' already exists in the database", Data = null };
            }
            Category category = _mapper.Map<Category>(dto);
            if (dto.ParentCategoryId==null)
            {
                category.ParentCategoryId = null;
                await _categoryRepository.AddAsync(category);
            }
            else
            {
                var parentCategory = await _categoryRepository.GetAsync(x => !x.IsDeleted && x.Id == dto.ParentCategoryId);
                if (parentCategory == null)
                {
                    return new CustomResponse<Category> { IsSuccess = false, Message = $"Parent category with ID {dto.ParentCategoryId} not found", Data = null };
                }
                category.ParentCategoryId = parentCategory.Id;
                if (parentCategory.Subcategories == null)
                {
                    parentCategory.Subcategories = new List<Category>();
                }
                parentCategory.Subcategories.Add(category);

            }
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return new CustomResponse<Category> { IsSuccess = true, Message = $"{category.Name} is created successfully", Data = category };
        }

        public async Task<IEnumerable<CategoryGetDto>> GetAllAsync()
        {
            IEnumerable<CategoryGetDto> categories = await _categoryRepository.GetQuery(x => !x.IsDeleted).AsNoTrackingWithIdentityResolution()
                .Include(x=>x.ParentCategory).ThenInclude(x=> x.Subcategories)
                .Select(x => new CategoryGetDto { Name = x.Name, Id = x.Id, ParentCategoryId = x.ParentCategoryId, Subcategories=x.Subcategories}).ToListAsync();
            return categories;
        }

        public async Task<CustomResponse<CategoryGetDto>> GetAsync(int id)
        {
            Category? category = await _categoryRepository.GetAsync(x => !x.IsDeleted && x.Id == id ,"ParentCategory");
            if (category == null)
            {
                return new CustomResponse<CategoryGetDto> { IsSuccess = false, Message = "This Category doesnt exist" };
            }
            CategoryGetDto categoryGetDto = new CategoryGetDto
            {
                CreatedAt = category.CreatedAt,
                Name = category.Name,
                ParentCategoryId=category.ParentCategoryId,
                Id = category.Id
            };
            return new CustomResponse<CategoryGetDto> { IsSuccess = true, Data = categoryGetDto };
        }

        public async Task<CustomResponse<Category>> RemoveAsync(int id)
        {
            Category category = await _categoryRepository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (category == null)
            {
                return new CustomResponse<Category> { IsSuccess = false, Message = "This Category doesnt exist" };
            }
            category.IsDeleted = true;
        
            await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return new CustomResponse<Category> { IsSuccess = true, Message = $"{category.Name} is removed successfully", Data = category };
        }

        public async Task<CustomResponse<Category>> UpdateAsync(int id, CategoryPostDto dto)
        {
            Category category = await _categoryRepository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (category == null)
            {
                return new CustomResponse<Category> { IsSuccess = false, Message = "This Category doesnt exist" };
            }
            category.Name = dto.Name;
            category.ParentCategoryId = dto.ParentCategoryId;
            await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return new CustomResponse<Category> { IsSuccess = true, Message = $"{category.Name} is updated successfully", Data = category };
        }

      
    }
}

