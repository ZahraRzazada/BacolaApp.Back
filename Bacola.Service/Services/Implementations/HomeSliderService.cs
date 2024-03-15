using System;
using System.Reflection.Metadata;
using AutoMapper;
using Bacola.Core.DTOS;

using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Data.Repositories;
using Bacola.Service.Extensions;
using Bacola.Service.Responses;
using Bacola.Service.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Bacola.Service.Services.Implementations
{
    public class HomeSliderService : IHomeSliderService
    {
        readonly IHomeSliderRepository _sliderRepository;
        readonly IWebHostEnvironment _env;
        readonly IMapper _mapper;
        public HomeSliderService(IHomeSliderRepository sliderRepository, IWebHostEnvironment env, IMapper mapper)
        {
            _sliderRepository = sliderRepository;
            _env = env;
            _mapper = mapper;
        }
        public async Task<CustomResponse<HomeSlider>> CreateAsync(SliderPostDto dto)
        {
            HomeSlider homeSlider = _mapper.Map<HomeSlider>(dto);
            if (dto.ImageFile == null)
            {
                return new CustomResponse<HomeSlider> { IsSuccess = false, Message = "The field image is required" };
            }
            if (!dto.ImageFile.IsImage())
            {
                return new CustomResponse<HomeSlider> { IsSuccess = false, Message = "Image is not valid" };
            }
            if (dto.ImageFile.IsSizeOk(1))
            {
                return new CustomResponse<HomeSlider> { IsSuccess = false, Message = "Size of Image is not valid" };
            }

            homeSlider.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "assets/img/home");
            await _sliderRepository.AddAsync(homeSlider);
            await _sliderRepository.SaveChangesAsync();
            return new CustomResponse<HomeSlider> { IsSuccess = true, Message = $"{homeSlider.Title} is created successfully", Data = homeSlider };
        }

        public async Task<IEnumerable<SliderGetDto>> GetAllAsync()
        {
            IEnumerable<SliderGetDto> sliders = await _sliderRepository.GetQuery(x => !x.IsDeleted)
             .AsNoTrackingWithIdentityResolution().Select(x => new SliderGetDto { Title = x.Title, Text = x.Text, Image = x.Image,DisCountPercent=x.DisCountPercent,Price=x.Price, Id = x.Id })
             .ToListAsync();
            return sliders;
        }

        public async Task<CustomResponse<SliderGetDto>> GetAsync(int id)
        {
            HomeSlider? slider = await _sliderRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (slider == null)
            {
                return new CustomResponse<SliderGetDto> { IsSuccess = false, Message = "This Slider doesnt exist" };
            }

            SliderGetDto slidergetdto = new SliderGetDto
            {
                Id = slider.Id,
                Title=slider.Title,
                Text=slider.Text,
                DisCountPercent=slider.DisCountPercent,
                Price=slider.Price,
                Image = slider.Image
            };
            return new CustomResponse<SliderGetDto> { IsSuccess = true, Data = slidergetdto };
        }

        public async Task<CustomResponse<HomeSlider>> RemoveAsync(int id)
        {
            HomeSlider? slider = await _sliderRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (slider == null)
            {
                return new CustomResponse<HomeSlider> { IsSuccess = false, Message = "This Slider doesnt exist" };
            }
            slider.IsDeleted = true;
            await _sliderRepository.UpdateAsync(slider);
            await _sliderRepository.SaveChangesAsync();
            return new CustomResponse<HomeSlider> { IsSuccess = true, Message = $"{slider.Title} is removed successfully", Data = slider };
        }

        public async Task<CustomResponse<HomeSlider>> UpdateAsync(int id, SliderPostDto dto)
        {
            HomeSlider? slider = await _sliderRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (slider == null)
            {
                return new CustomResponse<HomeSlider> { IsSuccess = false, Message = "This Slider doesnt exist" };
            }
            slider.Text = dto.Text;
            slider.Title = dto.Title;
            slider.Price = dto.Price;
            if (!dto.ImageFile.IsImage())
            {
                return new CustomResponse<HomeSlider> { IsSuccess = false, Message = "Image is not valid" };
            }

            if (dto.ImageFile.IsSizeOk(1))
            {
                return new CustomResponse<HomeSlider> { IsSuccess = false, Message = "Size of Image is not valid" };
            }

            slider.Image = dto.ImageFile.SaveFile(_env.WebRootPath, "assets/img/blog");
            slider.DisCountPercent = dto.DisCountPercent;
            await _sliderRepository.UpdateAsync(slider);
            await _sliderRepository.SaveChangesAsync();
            return new CustomResponse<HomeSlider> { IsSuccess = true, Message = $"{slider.Title} is updated successfully", Data = slider };
        }
    }
}

