using System;
using Bacola.Core.Entities;

namespace Bacola.Core.DTOS
{
	public class CategoryGetDto
	{
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        public List<Category>? Subcategories { get; set; }
    }
}

