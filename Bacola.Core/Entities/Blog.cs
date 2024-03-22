using System;
using Bacola.Core.Entities.BaseEntities;

namespace Bacola.Core.Entities
{
	public class Blog:BaseEntity
	{
        public Blog()
        {
            TagBlogs = new List<TagBlog>();
        }

        public string Title { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Info { get; set; } = null!;
        public string Quotes { get; set; } = null!;
        public string SubTitle { get; set; } =null!;
        public string SubInfo { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public List<TagBlog> TagBlogs { get; set; }
        public List<Coment> Coments { get; set; }

    }
}

