using System;
namespace Bacola.Core.DTOS
{
	public class CategoryPostDto
	{
        public string Name { get; set; } = null!;
        public int? ParentCategoryId { get; set; }
    }
}

