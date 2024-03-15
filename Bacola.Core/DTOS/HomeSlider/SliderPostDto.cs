using System;
using Microsoft.AspNetCore.Http;

namespace Bacola.Core.DTOS
{
	public class SliderPostDto
	{
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public double Price { get; set; }
        public double DisCountPercent { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}

