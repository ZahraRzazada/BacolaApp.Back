using System;
using Bacola.Core.Entities;

namespace Bacola.Core.DTOS
{
	public class OrderGetDto
	{
        public int Id { get; set; }
        public string UserEmail { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int Status { get; set; }
        public int OrderTracking { get; set; }
        public AppUser AppUser { get; set; } = null!;
        public List<OrderItemGetDto> OrderItems { get; set; } = null!;
    }
    public class OrderItemGetDto
    {
        public ProductGetDto Product { get; set; } = null!;
        public int Count { get; set; }
    }
}

