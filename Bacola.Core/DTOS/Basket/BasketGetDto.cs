using System;

using Bacola.Core.Entities;

namespace Bacola.Core.DTOS
{
	public class BasketGetDto
	{
        public List<BasketItemDto> basketItems { get; set; }
        public double TotalPrice { get; set; }
        public bool IsCouponApplied { get; set; }
        public BasketGetDto()
        {
            basketItems = new List<BasketItemDto>();
        }
    }
    public class BasketItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double DiscountPrice { get; set; }
        public int Count { get; set; }
    }
}

