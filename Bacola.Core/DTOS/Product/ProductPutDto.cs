using Bacola.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Bacola.Core.DTOS;

public class ProductPutDto
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
    public List<string> SpecificationKeys { get; set; } = new();
    public List<string> SpecificationValues { get; set; } = new();
    public List<IFormFile> ProductImageFiles { get; set; } = new();
    public IFormFile? MainImage { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Specification>? Specifications { get; set; }
    public List<ProductImage>? ProductImages { get; set; }

}