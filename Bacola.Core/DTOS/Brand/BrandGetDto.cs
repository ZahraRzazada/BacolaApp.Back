using System;
namespace Bacola.Core.DTOS
{
	public record BrandGetDto
	{
		public string Name { get; set; } = null!;
		public int Id { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}

