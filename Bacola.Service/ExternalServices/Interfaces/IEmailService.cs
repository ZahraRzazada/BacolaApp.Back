using System;
namespace Bacola.Service.ExternalServices.Interfaces
{
	public interface IEmailService
	{
        public Task SendEmail(string to, string subject, string body);
    }
}

