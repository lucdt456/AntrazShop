using AntrazShop.Data;

namespace AntrazShop.Interfaces.Repositories
{
	public interface IEmailRepository
	{
		Task<EmailCode> GetEmailCode(int userId, string code);
		Task CreateEmailCode(EmailCode ec);
		Task EditStatusEmailCode(int idCode, bool isUsed);
	}
}
