using System;
namespace Bacola.Core.DTOS
{
	public class CommentPostDto
	{
        public string Text { get; set; }
        public int BlogId { get; set; }
        public int? ParentId { get; set; }
        public string AppUserId { get; set; }
        //public DateTime CreatedAt { get; set; }
    }
}

