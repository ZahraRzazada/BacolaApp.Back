using System;
using System.ComponentModel.DataAnnotations;

namespace Bacola.Core.DTOS
{
	public class SliderGetDto
	{
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public double Price { get; set; }
        public double DisCountPercent { get; set; }
        public string Image { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}

