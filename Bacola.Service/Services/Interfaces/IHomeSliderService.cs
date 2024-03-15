using System;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Service.Responses;

namespace Bacola.Service.Services.Interfaces
{
	public interface IHomeSliderService
	{
        public Task<IEnumerable<SliderGetDto>> GetAllAsync();

        public Task<CustomResponse<HomeSlider>> CreateAsync(SliderPostDto dto);

        public Task<CustomResponse<HomeSlider>> RemoveAsync(int id);

        public Task<CustomResponse<HomeSlider>> UpdateAsync(int id, SliderPostDto dto);
        public Task<CustomResponse<SliderGetDto>> GetAsync(int id);
    }
}

