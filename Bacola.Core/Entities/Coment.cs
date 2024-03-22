using System;
using System.ComponentModel.DataAnnotations;
using Bacola.Core.DTOS;
using Bacola.Core.Entities.BaseEntities;

namespace Bacola.Core.Entities
{
        public class Coment : BaseEntity
        {

            public string Text { get; set; } = null!;

            public int BlogId { get; set; }
            public Blog Blog { get; set; } = null!;

            public int? ParentId { get; set; }
            public Coment? Parent { get; set; }

            public string AppUserId { get; set; }
            public AppUser AppUser { get; set; } = null!;
        }
}

