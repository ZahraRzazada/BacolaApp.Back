using System;
using Bacola.Core.Entities.BaseEntities;

namespace Bacola.Core.Entities
{
	public class Brand:BaseEntity
	{
		public string Name { get; set; } = null!;
		public List<Product> Products { get; set; } = null!;
	}
}

