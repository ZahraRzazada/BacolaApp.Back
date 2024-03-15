using System;
using Bacola.Core.Entities.BaseEntities;

namespace Bacola.Core.Entities
{
	public class Order:BaseEntity
	{
        public string Address { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string AppUserId { get; set; } = null!;
		public AppUser AppUser { get; set; } = null!;
		public int Status { get; set; }
        public int OrderTracking { get; set; }
        public List<OrderItem> OrderItems { get; set; } = null!;

		public Order()
		{
			OrderItems = new List<OrderItem>();
		}
	}
}

