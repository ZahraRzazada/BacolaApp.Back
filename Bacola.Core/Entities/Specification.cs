using System;
using Bacola.Core.Entities.BaseEntities;

namespace Bacola.Core.Entities
{
	public class Specification:BaseEntity
	{
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}

