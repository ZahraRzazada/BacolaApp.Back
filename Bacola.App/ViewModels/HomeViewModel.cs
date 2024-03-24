using System;
using Bacola.Core.DTOS;
using Bacola.Service.Responses;

namespace Bacola.App.ViewModels
{
	public class HomeViewModel
	{
        public IEnumerable<SliderGetDto> Sliders { get; set; }
        public IEnumerable<BlogGetDto> Blogs { get; set; }
        public IEnumerable<ProductGetDto> Products { get; set; }
    }
}


