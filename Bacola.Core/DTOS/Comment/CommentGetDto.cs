using System;
using Bacola.Core.Entities;

namespace Bacola.Core.DTOS
{
	public class CommentGetDto
	{
        public int Id { get; set; }
        public string Text { get; set; }
        public int BlogId { get; set; }
        public string AppUserId { get; set; }
        public string Username { get; set; }
        public int? ParentId { get; set; }
        public Coment Parent { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}

