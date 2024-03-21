
using AutoMapper;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Service.Extensions;
using Bacola.Service.Responses;
using Bacola.Service.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Bacola.Service.Services.Implementations
{
    public class BlogService : IBlogService
    {
        readonly IWebHostEnvironment _env;
        readonly IMapper _mapper;

        public BlogService(IWebHostEnvironment env, IBlogRepository blogRepository, IMapper mapper)
        {
            _env = env;
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        readonly IBlogRepository _blogRepository;

        public async Task<CustomResponse<Blog>> CreateAsync(BlogPostDto dto)
        {
            Blog blog = _mapper.Map<Blog>(dto);
            if (dto.ImageFile == null)
            {
                return new CustomResponse<Blog> { IsSuccess = false, Message = "The field image is required" };
            }
            if (!dto.ImageFile.IsImage())
            {
                return new CustomResponse<Blog> { IsSuccess = false, Message = "Image is not valid" };
            }
            if (dto.ImageFile.IsSizeOk(1))
            {
                return new CustomResponse<Blog> { IsSuccess = false, Message = "Size of Image is not valid" };
            }

            blog.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "assets/img/blog");

            foreach (var item in dto.TagIds)
            {
                blog.TagBlogs.Add(new TagBlog
                {
                    Blog = blog,
                    TagId = item,
                });
            }

            await _blogRepository.AddAsync(blog);
            await _blogRepository.SaveChangesAsync();
            return new CustomResponse<Blog> { IsSuccess = true, Message = $"{blog.Title} is created successfully", Data = blog };

        }

        public async Task<IEnumerable<BlogGetDto>> GetAllAsync()
        {
            IEnumerable<BlogGetDto> blogGetDtos = await _blogRepository.GetQuery(x => !x.IsDeleted).AsNoTrackingWithIdentityResolution().Include(x => x.Category).Include(x => x.TagBlogs).ThenInclude(x => x.Tag).Select(x => new BlogGetDto
            {
                Id = x.Id,
                Description = x.Description,
                Info = x.Info,
                Quotes = x.Quotes,
                Title = x.Title,
                SubInfo = x.SubInfo,
                SubTitle = x.SubTitle,
                Date = x.CreatedAt,
                Tags = x.TagBlogs.Select(y => new TagGetDto { Name = y.Tag.Name }),
                CategoryGetDto = new CategoryGetDto { Name = x.Category.Name },
                Image = x.Image,
              
            }).ToListAsync();

            return blogGetDtos;
        }

        public async Task<CustomResponse<BlogGetDto>> GetAsync(int id)
        {
            Blog? blog = await _blogRepository.GetAsync(x => !x.IsDeleted && x.Id == id, "TagBlogs.Tag", "Category", "Comments");
            if (blog == null)
            {
                return new CustomResponse<BlogGetDto> { IsSuccess = false, Message = "This Blog doesnt exist" };
            }
            BlogGetDto blogGetDto = new BlogGetDto
            {
          Id=blog.Id,
                Description = blog.Description,
                Info = blog.Info,
                Quotes = blog.Quotes,
                Title = blog.Title,
                SubInfo = blog.SubInfo,
                SubTitle = blog.SubTitle,
                Date = blog.CreatedAt,
                Tags = blog.TagBlogs.Select(y => new TagGetDto { Name = y.Tag.Name, Id = y.Tag.Id }),
                CategoryGetDto = new CategoryGetDto { Name = blog.Category.Name, Id = blog.Id },
                Image = blog.Image,
                Comments=blog.Comments
            };
            return new CustomResponse<BlogGetDto> { IsSuccess = true, Data = blogGetDto };
        }


        public async Task<CustomResponse<Blog>> RemoveAsync(int id)
        {
            Blog? blog = await _blogRepository.GetAsync(x => !x.IsDeleted && x.Id == id, "TagBlogs.Tag", "Category");
            if (blog == null)
            {
                return new CustomResponse<Blog> { IsSuccess = false, Message = "This Blog doesnt exist" };
            }
            blog.IsDeleted = true;
            await _blogRepository.UpdateAsync(blog);
            await _blogRepository.SaveChangesAsync();
            return new CustomResponse<Blog> { IsSuccess = true, Message = $"{blog.Title} is removed successfully", Data = blog };
        }

        public async Task<CustomResponse<Blog>> UpdateAsync(int id, BlogPostDto dto)
        {

            Blog? blog = await _blogRepository.GetAsync(x => !x.IsDeleted && x.Id == id, "TagBlogs.Tag", "Category");
            if (blog == null)
            {
                return new CustomResponse<Blog> { IsSuccess = false, Message = "This Blog doesnt exist" };
            }

            blog.CategoryId = dto.CategoryId;
            blog.Description = dto.Description;
            blog.Info = dto.Info;
            blog.Title = dto.Title;
            blog.SubInfo = dto.SubInfo;
            blog.SubTitle = dto.SubTitle;
            blog.Quotes = dto.Quotes;
            if (dto.ImageFile != null)
            {
                if (!dto.ImageFile.IsImage())
                {
                    return new CustomResponse<Blog> { IsSuccess = false, Message = "Image is not valid" };
                }

                if (dto.ImageFile.IsSizeOk(1))
                {
                    return new CustomResponse<Blog> { IsSuccess = false, Message = "Size of Image is not valid" };
                }

                blog.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "assets/img/blog");
            }
            blog.TagBlogs.Clear();
            foreach (var item in dto.TagIds)
            {
                blog.TagBlogs.Add(new TagBlog
                {
                    Blog = blog,
                    TagId = item,
                });
            }

            await _blogRepository.UpdateAsync(blog);
            await _blogRepository.SaveChangesAsync();
            return new CustomResponse<Blog> { IsSuccess = true, Message = $"{blog.Title} is updated successfully", Data = blog };
        }

        public async Task<List<BlogGetDto>> GetBlogsBySearchTextAsync(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                List<BlogGetDto> blogGetDtos = await _blogRepository.GetQuery(x => !x.IsDeleted)
                   .AsNoTrackingWithIdentityResolution().Include(x => x.Category)
                   .Select(x => new BlogGetDto
                   {

                       Id = x.Id,
                       Description = x.Description,
                       Info = x.Info,
                       Quotes = x.Quotes,
                       Title = x.Title,
                       SubInfo = x.SubInfo,
                       SubTitle = x.SubTitle,
                       Date = x.CreatedAt,
                       Tags = x.TagBlogs.Select(y => new TagGetDto { Name = y.Tag.Name, Id = y.Tag.Id }),
                       //CommentCount = x.Comments.Where(x => !x.IsDeleted).Count()
                   }).ToListAsync();

                return blogGetDtos;
            }

            return await _blogRepository.GetQuery(x => !x.IsDeleted).AsNoTrackingWithIdentityResolution()
                .Include(x => x.Category)
                 .Select(x => new BlogGetDto
                 {
                     Id = x.Id,
                     Description = x.Description,
                     Info = x.Info,
                     Quotes = x.Quotes,
                     Title = x.Title,
                     SubInfo = x.SubInfo,
                     SubTitle = x.SubTitle,
                     Date = x.CreatedAt,
                     Tags = x.TagBlogs.Select(y => new TagGetDto { Name = y.Tag.Name, Id = y.Tag.Id }),
                     //CommentCount = x.Comments.Where(x => !x.IsDeleted).Count()

                 }).Where(m => m.Title.ToLower().Contains(searchText.ToLower())).ToListAsync();
        }
    }
}

