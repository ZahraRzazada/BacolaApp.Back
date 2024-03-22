using System;
using Microsoft.AspNetCore.Http;

namespace Bacola.Core.DTOS
{
	public record BlogPostDto
	{
        public string Title { get; set; } = null!;
        public IFormFile? ImageFile { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Info { get; set; } = null!;
        public string Quotes { get; set; } = null!;
        public string SubTitle { get; set; } = null!;
        public string SubInfo { get; set; } = null!;
        public int CategoryId { get; set; }
        public List<int> TagIds { get; set; } = null!;
    }
}

