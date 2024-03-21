using System;
using System.ComponentModel.DataAnnotations;
using Bacola.Core.Entities.BaseEntities;

namespace Bacola.Core.Entities
{
    public class ParentComment : BaseEntity
    {
        [Required]
        public string Text { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; } = null!;
        public string AspNetUsersId { get; set; }
        public AppUser AppUser { get; set; } = null!;
        public List<Reply> Replies { get; set; } = new List<Reply>();

    }
    public class Reply : BaseEntity
    {
        [Required]
        public string Text { get; set; }

        public int ParentCommentId { get; set; }
        public ParentComment ParentComment { get; set; }

        public string AspNetUsersId { get; set; }
        public AppUser AppUser { get; set; }

    }
}

