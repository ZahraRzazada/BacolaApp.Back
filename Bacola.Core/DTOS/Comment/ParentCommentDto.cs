using System;
using Bacola.Core.Entities;

namespace Bacola.Core.DTOS
{
    public class ParentCommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int BlogId { get; set; }
        public string AspNetUsersId { get; set; }
        public AppUser AppUser { get; set; }
        public List<ReplyDto>? Replies { get; set; }
        public DateTime CreatedAt { get; set; }
       
    }

    public class ReplyDto   
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int ParentCommentId { get; set; }
        public string AspNetUsersId { get; set; }
    }
}

