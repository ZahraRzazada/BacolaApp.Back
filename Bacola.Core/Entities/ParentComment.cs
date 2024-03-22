using System;
using System.ComponentModel.DataAnnotations;
using Bacola.Core.DTOS;
using Bacola.Core.Entities.BaseEntities;

namespace Bacola.Core.Entities
{
    
    public class ParentComment : BaseEntity
    {

        public string Text { get; set; } = null!;

        public int BlogId { get; set; }
        public Blog Blog { get; set; } = null!;

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;

        public List<Reply> Replies { get; set; } = new List<Reply>();
    }

    public class Reply : BaseEntity
    {
   
        public string Text { get; set; } = null!;

        public int BlogId { get; set; }
        public Blog Blog { get; set; } = null!;

        public int ParentCommentId { get; set; }
        public ParentComment ParentComment { get; set; } = null!;

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;
    }
}

