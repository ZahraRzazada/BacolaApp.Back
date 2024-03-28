using System;
namespace Bacola.Core.DTOS;

	public class ProductFilterDto
	{
    public List<int>?categoryIds { get; set; }
    public int categoryId { get; set; }

    public List<int>? brandIds { get; set; }
    public int? fromPrice { get; set; }
    public int? toPrice { get; set; }
    public bool? IsInStock { get; set; }


}




