using System;
using Bacola.Core.Entities.BaseEntities;

namespace Bacola.Core.Entities
{
	public class Product : BaseEntity
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
        public Brand Brand { get; set; } = null!;
		public int BrandId { get; set; }
		public Category Category { get; set; } = null!;
		public int CategoryId { get; set; }
		public List<TagProduct> TagProducts { get; set; } = null!;
        public List<Specification> Specifications { get; set; } = null!;
        public List<ProductImage> ProductImages { get; set; } = null!;
        public List<BasketItem> BasketItems { get; set; } = null!;
        public List<WishlistItem> WishlistItems { get; set; } = null!;
        public List<OrderItem> OrderItems { get; set; } = null!;
        public Product()
        {
            TagProducts = new List<TagProduct>();
            ProductImages = new List<ProductImage>();
            Specifications = new List<Specification>();
        }
    }
}

