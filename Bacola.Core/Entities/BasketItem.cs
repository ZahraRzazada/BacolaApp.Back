using System;
using Bacola.Core.Entities.BaseEntities;

namespace Bacola.Core.Entities
{
	public class BasketItem : BaseEntity
	{
		public int ProductId { get; set; }
		public Product Product { get; set; } = null!;
        public int BasketId { get; set; }
		public Basket Basket { get; set; } = null!;
        public int Count { get; set; }
    }
}

