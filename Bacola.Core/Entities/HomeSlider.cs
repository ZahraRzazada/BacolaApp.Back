using System;
using System.ComponentModel.DataAnnotations;
using Bacola.Core.Entities.BaseEntities;

namespace Bacola.Core.Entities
{
	public class HomeSlider:BaseEntity
	{
        [StringLength(100)]
        public string Title { get; set; } = null!;
        [StringLength(300)]
        public string Text { get; set; } = null!;
        public double Price { get; set; }
        public double DisCountPercent { get; set; }
        public string Image { get; set; } = null!;
    }
}

