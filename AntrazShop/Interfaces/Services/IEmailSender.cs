namespace AntrazShop.Interfaces.Services
{
	public interface IEmailSender
	{
		/// <summary>
		/// Gửi email
		/// </summary>
		/// <param name="toEmail">Email cần gửi đến</param>
		/// <param name="subject">Tiêu đề của Email</param>
		/// <param name="body">Nội dung email</param>
		/// <returns></returns>
		Task SendEmailAsync(string toEmail, string subject, string body);
	}
}
