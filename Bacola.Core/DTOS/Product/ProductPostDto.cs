using System;
using Microsoft.AspNetCore.Http;

namespace Bacola.Core.DTOS
{
	public record ProductPostDto
	{
        public string Title { get; set; } = null!;
        public int PeriodOfUse { get; set; }
        public bool IsOrganic { get; set; }
        public bool InStock { get; set; } 
        public string Info { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public double DiscountPercent { get; set; }
        public double DiscountPrice { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public List<int> TagIds { get; set; } = null!;
        public List<string> SpecificationKeys { get; set; } = null!;
        public List<string> SpecificationValues { get; set; } = null!;
        public List<IFormFile> ProductImageFiles { get; set; } = null!;
        public IFormFile? MainImage { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}

