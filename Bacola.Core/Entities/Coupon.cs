using System;
using Bacola.Core.Entities.BaseEntities;

namespace Bacola.Core.Entities
{
	public class Coupon:BaseEntity
	{
        public int Status { get; set; }
        public string Code { get; set; } = null!;
        public string Title { get; set; } = null!;
        public int DiscountAmount { get; set; }
    }
}

