using System;
using Bacola.Core.Entities.BaseEntities;

namespace Bacola.Core.Entities
{
	public class ProductImage:BaseEntity
	{
        public int ProductId { get; set; }
		public Product Product { get; set; } = null!;
		public bool IsMain { get; set; }
		public string Image { get; set; } = null!;
	}

}

