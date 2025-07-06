using AntrazShop.Interfaces.Services;
using AntrazShop.Models;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;


namespace AntrazShop.Services
{
	public class EmailSender : IEmailSender
	{
		private readonly EmailSettings _emailSettings;

		public EmailSender(IOptions<EmailSettings> emailSettings)
		{
			_emailSettings = emailSettings.Value;
		}

		public async Task SendEmailAsync(string toEmail, string subject, string body)
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromEmail));
			message.To.Add(new MailboxAddress("", toEmail));
			message.Subject = subject;

			var bodyBuilder = new BodyBuilder
			{
				HtmlBody = body
			};

			message.Body = bodyBuilder.ToMessageBody();

			using (var client = new SmtpClient())
			{
				client.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
				{
					// Bỏ qua tất cả SSL certificate errors
					return true;
				};

				try
				{
					// Kết nối với SSL/TLS nhưng bỏ qua certificate validation
					await client.ConnectAsync(_emailSettings.SmtpHost, _emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);

					// Authenticate
					await client.AuthenticateAsync(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);

					// Gửi email
					await client.SendAsync(message);
					// Disconnect
					await client.DisconnectAsync(true);
				}
				catch (Exception ex)
				{
					// Log error nếu cần
					throw new Exception($"Failed to send email: {ex.Message}", ex);
				}
			}
		}

	}
}
