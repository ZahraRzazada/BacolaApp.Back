using System;
using Bacola.Core.Entities.BaseEntities;

namespace Bacola.Core.Entities
{
	public class TagBlog:BaseEntity
	{
		public Tag Tag { get; set; } = null!;
		public int TagId { get; set; }
        public Blog Blog { get; set; } = null!;
        public int BlogId { get; set; }
    }
}

