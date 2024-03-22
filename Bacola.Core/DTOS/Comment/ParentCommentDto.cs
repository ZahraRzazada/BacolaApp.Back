using System;
using System.ComponentModel.DataAnnotations;
using Bacola.Core.Entities;

namespace Bacola.Core.DTOS
{
    public class ParentCommentDto
    {
        public ParentCommentDto()
        {
            Replies = new List<ReplyDto>();
        }
        public int Id { get; set; }
        public string Text { get; set; }
        public int BlogId { get; set; }
        public string AppUserId { get; set; }
        public List<ReplyDto> Replies { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ReplyDto   
    {
        public DateTime CreatedAt { get; set; }
        public int Id { get; set; }
        public string Text { get; set; }
        public int BlogId { get; set; }
        public int ParentCommentId { get; set; }
        public string AppUserId { get; set; }
    }
}

