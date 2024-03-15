using System;
using Bacola.Core.Entities.BaseEntities;

namespace Bacola.Core.Entities
{
	public class OrderItem:BaseEntity
	{
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public int Count { get; set; }
    }
}

