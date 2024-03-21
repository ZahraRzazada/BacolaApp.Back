using System;
namespace Bacola.Core.DTOS
{
	public class ProductFilterDto
	{
        public int? categoryId { get; set; }
        public int? brandId { get; set; }
        public int? fromPrice { get; set; }
        public int? toPrice { get; set; }
        public bool? IsInStock { get; set; }


    }
}

