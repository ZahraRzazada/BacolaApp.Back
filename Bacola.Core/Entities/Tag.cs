using System;
using Bacola.Core.Entities.BaseEntities;

namespace Bacola.Core.Entities
{
	public class Tag:BaseEntity
	{
		public string Name { get; set; } = null!;
		public List<TagBlog> TagBlogs { get; set; } = null!;
		public List<TagProduct> TagProducts { get; set; } = null!;
	}
}

