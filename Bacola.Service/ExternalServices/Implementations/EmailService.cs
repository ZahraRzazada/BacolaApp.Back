using System;
using System.Net;
using System.Net.Mail;
using Bacola.Service.ExternalServices.Interfaces;

namespace Bacola.Service.ExternalServices.Implementations
{
	public class EmailService:IEmailService
	{
        public async Task SendEmail(string to, string subject, string body)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("pm4283719@gmail.com", "ddzrblqxpzyxysdu");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("pm4283719@gmail.com", "EduHome App");
            mailMessage.To.Add(to);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;
            client.Send(mailMessage);
        }
    }
}

