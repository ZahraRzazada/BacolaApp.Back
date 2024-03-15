using System;
namespace Bacola.Core.DTOS
{
	public class CouponGetDto
	{
        public int Id { get; set; }
        public int Status { get; set; }
        public string Code { get; set; } = null!;
        public string Title { get; set; } = null!;
        public int DiscountAmount { get; set; }
    }
}

