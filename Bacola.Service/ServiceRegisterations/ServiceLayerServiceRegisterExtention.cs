using System;
using Bacola.Service.ExternalServices.Implementations;
using Bacola.Service.ExternalServices.Interfaces;
using Bacola.Service.Services.Implementations;
using Bacola.Service.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Bacola.Service.ServiceRegisterations
{
    public static class ServiceLaierServiceRegisterExtention
    {
        public static void ServiceLaierServiceRegister(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<ProductService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IProductImageService, ProductImageService>();
            services.AddScoped<IHomeSliderService, HomeSliderService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IWishlistService, WishlistService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICouponService, CouponService>();
            services.AddHttpContextAccessor();
            services.AddScoped<IEmailService, EmailService>();

        }

    }
}
