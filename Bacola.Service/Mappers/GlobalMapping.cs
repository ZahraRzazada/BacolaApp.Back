using System;
using AutoMapper;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;

namespace Bacola.Service.Mappers
{
    public class GlobalMapping : Profile
    {
        public GlobalMapping()
        {
            CreateMap<Tag, TagGetDto>().ReverseMap();
            CreateMap<Tag, TagPostDto>().ReverseMap();
            CreateMap<Blog, BlogGetDto>().ReverseMap();
            CreateMap<Blog, BlogPostDto>().ReverseMap();
            CreateMap<Brand, BrandGetDto>().ReverseMap();
            CreateMap<Brand, BrandPostDto>().ReverseMap();
            CreateMap<Product, ProductGetDto>().ReverseMap();
            CreateMap<Product, ProductPostDto>().ReverseMap();
            CreateMap<Contact, ContactGetDto>().ReverseMap();
            CreateMap<Contact, ContactPostDto>().ReverseMap();
            CreateMap<Setting, SettingGetDto>().ReverseMap();
            CreateMap<Setting, SettingPostDto>().ReverseMap();
            CreateMap<HomeSlider, SliderGetDto>().ReverseMap();
            CreateMap<HomeSlider, SliderPostDto>().ReverseMap();
            CreateMap<Category, CategoryPostDto>().ReverseMap();
            CreateMap<Category,  CategoryGetDto>().ReverseMap();
            CreateMap<Category, CategoryGetDto>().ReverseMap();
            CreateMap<AppUser, CreateAdminDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemGetDto>().ReverseMap();
            CreateMap<Order, OrderGetDto>().ReverseMap();
            CreateMap<Coupon, CouponGetDto>().ReverseMap();
            CreateMap<Coupon, CouponPostDto>().ReverseMap();

        }
    }
}

