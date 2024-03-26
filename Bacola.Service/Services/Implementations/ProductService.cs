using System;
using System.Reflection.Metadata;
using AutoMapper;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;

using Bacola.Service.Extensions;
using Bacola.Service.Responses;
using Bacola.Service.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Bacola.Service.Services.Implementations
{
    public class ProductService : IProductService
    {
        readonly IProductRepository _productRepository;
        readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IWebHostEnvironment env, IMapper mapper)
        {
            _productRepository = productRepository;
            _env = env;
            _mapper = mapper;
        }

        readonly IWebHostEnvironment _env;
        public async Task<CustomResponse<Product>> CreateAsync(ProductPostDto dto)
        {
            Product product = _mapper.Map<Product>(dto);
            if (dto.MainImage == null)
            {
                return new CustomResponse<Product> { IsSuccess = false, Message = "The field image is required" };
            }
            if (dto.ProductImageFiles == null || dto.ProductImageFiles.Count() == 0)
            {
                return new CustomResponse<Product> { IsSuccess = false, Message = "The field image is required" };
            }
            if (dto.SpecificationKeys == null || dto.SpecificationValues == null || dto.SpecificationValues.Count() != dto.SpecificationKeys.Count())
            {
                return new CustomResponse<Product> { IsSuccess = false, Message = "The Specification is not valid" };
            }
            foreach (var item in dto.TagIds)
            {
                product.TagProducts.Add(new TagProduct
                {
                    Product = product,
                    TagId = item,
                });
            }
            if(!dto.MainImage.IsImage())
            {
                return new CustomResponse<Product> { IsSuccess = false, Message = "The image is not valid" };
            }
            if (dto.MainImage.IsSizeOk(1))
            {
                return new CustomResponse<Product> { IsSuccess = false, Message = "Size of image is not valid" };
            }

            string mainImage = dto.MainImage.SaveFile(_env.WebRootPath, "assets/img/product");
            product.ProductImages.Add(new ProductImage
            {
                Image = mainImage,
                IsMain = true,
                Product = product,
            });


            foreach (var item in dto.ProductImageFiles)
            {
                if (!item.IsImage())
                {
                    return new CustomResponse<Product> { IsSuccess = false, Message = "The image is not valid" };
                }
                if (item.IsSizeOk(1))
                {
                    return new CustomResponse<Product> { IsSuccess = false, Message = "Size of Image is not valid" };
                }
                string image = item.SaveFile(_env.WebRootPath, "assets/img/product");
                product.ProductImages.Add(new ProductImage
                {
                    Image = image,
                    IsMain = false,
                    Product = product
                });
            }
            for (int i = 0; i < dto.SpecificationKeys.Count(); i++)
            {
                product.Specifications.Add(new Specification
                {
                    Product = product,
                    Key = dto.SpecificationKeys[i],
                    Value = dto.SpecificationValues[i],
                });
            }
            for (int i = 0; i < dto.SpecificationKeys.Count(); i++)
            {
                product.Specifications.Add(new Specification
                {
                    Product = product,
                    Key = dto.SpecificationKeys[i],
                    Value = dto.SpecificationValues[i],
                });
            }
            product.DiscountPrice = dto.DiscountPercent == 0 ? dto.Price : (dto.Price * dto.DiscountPercent) / 100;
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            return new CustomResponse<Product> { IsSuccess = true, Message = $"{product.Title} is created successfully", Data = product };

        }

        public async Task<PagginatedResponse<ProductGetDto>> GetAllAsync(int page = 1)
        {
            const int PageSize = 20;
            PagginatedResponse<ProductGetDto> pagginatedResponse = new PagginatedResponse<ProductGetDto>();
            pagginatedResponse.CurrentPage = page;
            var query = _productRepository.GetQuery(x => !x.IsDeleted)
         .Include(x => x.Category)
         .Include(x => x.Brand)
         .Include(x => x.ProductImages)
         .Skip((page - 1) * PageSize)
         .Take(PageSize);

            pagginatedResponse.TotalPages = (int)Math.Ceiling((double)query.Count() / PageSize);

            IEnumerable<ProductGetDto> productGetDtos = query.Select(x => new ProductGetDto
            {

                DiscountPercent = x.DiscountPercent,
                Id = x.Id,
                Brand = new BrandGetDto { Name = x.Brand.Name },
                Title = x.Title,
                InStock = x.InStock,
                Category = new CategoryGetDto { Name = x.Category.Name },
                Price = x.Price,
                DiscountPrice = x.DiscountPercent == 0 ? x.Price : Math.Round((x.Price * (100 - x.DiscountPercent)) / 100, 1),
                ProductImages = x.ProductImages
            });
            pagginatedResponse.Items = productGetDtos;

            return pagginatedResponse;
        }
        public async Task<CustomResponse<ProductGetDto>> GetAsync(int id)
        {
            var query = _productRepository.GetQuery(x => x.IsDeleted == false && x.Id == id)
                .Include(x => x.ProductImages)
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Specifications)
                .Include(x => x.TagProducts)
                .ThenInclude(x => x.Tag);

            ProductGetDto? productGetDto = await query.Select(x => new ProductGetDto
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                Brand = new BrandGetDto { Name = x.Brand.Name },
                Category = new CategoryGetDto { Name = x.Category.Name },
                Description = x.Description,
                Price = x.Price,
                DiscountPrice = x.DiscountPercent == 0 ? x.Price : Math.Round((x.Price * (100 - x.DiscountPercent)) / 100, 1),
                DiscountPercent = x.DiscountPercent,
                Info = x.Info,
                Title = x.Title,
                PeriodOfUse = x.PeriodOfUse,
                IsOrganic = x.IsOrganic,
                InStock = x.InStock,
                ProductImages = x.ProductImages,
                Specifications = x.Specifications,
                TagProducts = x.TagProducts
            }).FirstOrDefaultAsync();
            if (productGetDto == null)
            {
                return new CustomResponse<ProductGetDto> { IsSuccess = false, Message = "This Product doesnt exist" };
            }
            return new CustomResponse<ProductGetDto> { IsSuccess = true, Data = productGetDto };
        }


        public async Task<CustomResponse<Product>> RemoveAsync(int id)
        {
            Product product = new Product();
            if (product == null)
            {
                return new CustomResponse<Product> { IsSuccess = false, Message = "This Product doesnt exist" };
            }
            product.IsDeleted = true;
            await _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();
            return new CustomResponse<Product> { IsSuccess = true, Message = $"{product.Title} is removed successfully", Data = product };
        }
        public async Task<CustomResponse<Product>> UpdateAsync(int id, ProductPostDto dto)
        {
            if (dto.SpecificationKeys == null || dto.SpecificationValues == null || dto.SpecificationValues.Count() != dto.SpecificationKeys.Count())
            {
                return new CustomResponse<Product> { IsSuccess = false, Message = "The Specification is not valid" };
            }
            var product = await _productRepository.GetQuery(x => x.IsDeleted == false && x.Id == id)
               .Include(x => x.ProductImages)
               .Include(x => x.Category)
               .Include(x => x.Brand)
               .Include(x => x.Specifications)
               .Include(x => x.TagProducts)
               .ThenInclude(x => x.Tag)
               .FirstOrDefaultAsync();
            if (product == null)
            {
                return new CustomResponse<Product> { IsSuccess = false, Message = "This Product doesnt exist" };
            }
            product.DiscountPercent = dto.DiscountPercent;
            product.BrandId = dto.BrandId;
            product.CategoryId = dto.CategoryId;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.DiscountPrice = dto.DiscountPrice;
            product.Info = dto.Info;
            product.Title = dto.Title;
            product.PeriodOfUse = dto.PeriodOfUse;
            product.IsOrganic = dto.IsOrganic;
            product.InStock = dto.InStock;
            product.Specifications.Clear();
            for (int i = 0; i < dto.SpecificationKeys.Count(); i++)
            {
                product.Specifications.Add(new Specification
                {
                    Product = product,
                    Key = dto.SpecificationKeys[i],
                    Value = dto.SpecificationValues[i],
                });
            }
            product.TagProducts.Clear();
            foreach (var item in dto.TagIds)
            {
                product.TagProducts.Add(new TagProduct
                {
                    Product = product,
                    TagId = item,
                });
            }
            if (dto.ProductImageFiles != null)
            {
                foreach (var item in dto.ProductImageFiles)
                {
                    if (!item.IsImage())
                    {
                        return new CustomResponse<Product> { IsSuccess = false, Message = "Image is not valid" };
                    }

                    if (item.IsSizeOk(1))
                    {
                        return new CustomResponse<Product> { IsSuccess = false, Message = "Size of Image is not valid" };
                    }

                    string image = item.SaveFile(_env.WebRootPath, "assets/img/product");

                    product.ProductImages.Add(new ProductImage
                    {
                        Image = image,
                        IsMain = false,
                        Product = product,
                    });
                }
            }
            if (dto.MainImage != null)
            {
                if (!dto.MainImage.IsImage())
                {
                    return new CustomResponse<Product> { IsSuccess = false, Message = "Image is not valid" };
                }

                if (dto.MainImage.IsSizeOk(1))
                {
                    return new CustomResponse<Product> { IsSuccess = false, Message = "Size of Image is not valid" };
                }

                string mainImage = dto.MainImage.SaveFile(_env.WebRootPath, "assets/img/product");

                product.ProductImages.Remove(product.ProductImages.FirstOrDefault(x => x.IsMain));

                product.ProductImages.Add(new ProductImage
                {
                    Image = mainImage,
                    IsMain = true,
                    Product = product,
                });
            }
            await _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();
            return new CustomResponse<Product> { IsSuccess = true, Message = $"{product.Title} is updated successfully", Data = product };

        }

        public async Task<PagginatedResponse<ProductGetDto>> GetFilteredProducts(ProductFilterDto filter)
        {
           
                var query = _productRepository.GetQuery(x => !x.IsDeleted);

                query = query.Include(p => p.Category)
                             .Include(p => p.Brand);
                if (filter.categoryIds != null)
                {
                   query= query.Where(x => filter.categoryIds.Contains(x.CategoryId));
                }
            if (filter.categoryId != null)
            {
                query = query.Where(x => filter.categoryId==x.CategoryId);
            }
            if (filter.brandIds != null)
                {
                query = query.Where(x => filter.brandIds.Contains(x.BrandId));
            }
                if (filter.fromPrice != null)
                {
                    query = query.Where(p => p.DiscountPrice >= filter.fromPrice);
                }
                if (filter.toPrice != null)
                {
                    query = query.Where(p => p.DiscountPrice <= filter.toPrice);
                }
                if (filter.IsInStock != null)
                {
                    query = query.Where(p => p.InStock == filter.IsInStock);
                }
                var products = await query.Select(p => new ProductGetDto
                {
                    Id = p.Id,
                    Category = new CategoryGetDto { Name = p.Category.Name },
                    Brand = new BrandGetDto { Name = p.Brand.Name },
                    Title = p.Title,
                    InStock = p.InStock, 
                    Price = p.Price,
                    DiscountPrice = p.DiscountPercent == 0 ? p.Price : (p.Price * p.DiscountPercent) / 100,
                    ProductImages = p.ProductImages 
                }).ToListAsync();


                var dtos = _mapper.Map< IEnumerable<ProductGetDto>>(products);
            return new() { Items = dtos, };
            
        
        }

        public async Task<CustomResponse<List<ProductGetDto>>> Search(string search)
        {
            var query = _productRepository.GetQuery(x => !x.IsDeleted)
         .Include(x => x.Category)
         .Include(x => x.Brand)
         .Include(x => x.ProductImages)
         .Where(x =>
             x.Title.ToLower().Contains(search.ToLower()) ||
             x.Category.Name.ToLower().Contains(search.ToLower())

         );
            List<ProductGetDto> productGetDtos = await query.Select(x => new ProductGetDto
            {
                DiscountPercent = x.DiscountPercent,
                Id = x.Id,
                Brand = new BrandGetDto { Name = x.Brand.Name },
                Title = x.Title,
                InStock = x.InStock,
                Category = new CategoryGetDto { Name = x.Category.Name },
                Price = x.Price,
                DiscountPrice = x.DiscountPercent == 0 ? x.Price : Math.Round((x.Price * (100 - x.DiscountPercent)) / 100, 1),
                ProductImages = x.ProductImages
            }).ToListAsync();

         return new  CustomResponse<List<ProductGetDto>> { IsSuccess = true, Message = "TProduct Searched succesfully" ,Data=productGetDtos };

        }

    }
}


