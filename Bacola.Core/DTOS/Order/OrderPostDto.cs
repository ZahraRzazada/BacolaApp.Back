using System;
namespace Bacola.Core.DTOS
{
	public class OrderPostDto
	{
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Text { get; set; } = null!;
    }
}

