using System;
using Bacola.Core.Entities;

namespace Bacola.Core.DTOS
{
	public record ProductGetDto
	{
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int PeriodOfUse { get; set; }
        public bool IsOrganic { get; set; } 
        public bool InStock { get; set; } 
        public string Info { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public double DiscountPercent { get; set; }
        public double DiscountPrice { get; set; }
        public BrandGetDto Brand { get; set; } = null!;
        public int BrandId { get; set; }
        public CategoryGetDto Category { get; set; } = null!;
        public int CategoryId { get; set; }
        public List<TagProduct> TagProducts { get; set; } = null!;
        public List<Specification> Specifications { get; set; } = null!;
        public List<ProductImage> ProductImages { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }

}

