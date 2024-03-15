using System;
namespace Bacola.Core.DTOS
{
	public class ResetPasswordDto
	{
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}

