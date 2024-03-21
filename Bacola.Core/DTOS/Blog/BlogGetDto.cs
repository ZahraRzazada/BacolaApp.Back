using System;
using Bacola.Core.Entities;

namespace Bacola.Core.DTOS
{
	public record BlogGetDto
	{
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Info { get; set; } = null!;
        public string Quotes { get; set; } = null!;
        public string SubTitle { get; set; } = null!;
        public string SubInfo { get; set; } = null!;
        public CategoryGetDto CategoryGetDto { get; set; }=null!;
        public IEnumerable<TagGetDto> Tags { get; set; } = null!;
        public List<ParentComment> Comments { get; set; }
        public DateTime Date { get; set; }
    }
}

