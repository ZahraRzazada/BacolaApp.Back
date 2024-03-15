using System;
using Bacola.Core.Entities.BaseEntities;

namespace Bacola.Core.Entities
{
	public class TagProduct:BaseEntity
	{
		public Tag Tag { get; set; } = null!;
		public Product Product { get; set; } = null!;
        public int ProductId { get; set; }
		public int TagId { get; set; }
    }
}

