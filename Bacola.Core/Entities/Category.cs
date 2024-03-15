using System;
using Bacola.Core.Entities.BaseEntities;

namespace Bacola.Core.Entities
{
	public class Category:BaseEntity
	{
		public string Name { get; set; } = null!;
		public List<Blog> Blogs { get; set; } = null!;
		public List<Product> Products { get; set; } = null!;
		public int? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        public List<Category>? Subcategories { get; set; }
    }
}

