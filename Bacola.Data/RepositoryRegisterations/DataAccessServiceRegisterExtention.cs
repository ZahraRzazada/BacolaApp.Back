using System;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Data.Contexts;
using Bacola.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bacola.Data.RepositoryRegisterations
{
    public static class DataAccessServiceRegisterExtention
    {
        public static void DataAccessServiceRegister(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BacolaDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("Default"),
                                 b => b.MigrationsAssembly("Bacola.Data"));
            });
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IHomeSliderRepository, HomeSliderRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketItemRepository, BasketItemRepository>();
            services.AddScoped<IWishlistRepository, WishlistRepository>();
            services.AddScoped<IWishlistItemRepository, WishlistItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<ICouponRepository, CouponRepository>();
            services.AddScoped<IComentRepository, ComentRepository>();
        
            services.AddIdentity<AppUser, IdentityRole>(
               opt =>
               {
                   opt.User.RequireUniqueEmail = true;
                   opt.Lockout.MaxFailedAccessAttempts = 3;
                   opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);

                   opt.SignIn.RequireConfirmedEmail = true;

               })
               .AddEntityFrameworkStores<BacolaDbContext>().AddDefaultTokenProviders();
        }

    }
}

