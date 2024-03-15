using System;
namespace Bacola.Core.DTOS
{
	public class UpdateDto
	{
        public string Username { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}

