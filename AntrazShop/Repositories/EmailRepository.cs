using AntrazShop.Data;
using AntrazShop.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AntrazShop.Repositories
{
	public class EmailRepository : IEmailRepository
	{
		private readonly ShopDbContext _context;
		public EmailRepository(ShopDbContext context)
		{
			_context = context;
		}

		public async Task CreateEmailCode(EmailCode ec)
		{
			_context.EmailCodes.Add(ec);
			await _context.SaveChangesAsync();
		}

		public async Task EditStatusEmailCode(int idCode, bool isUsed)
		{
			var ec = await _context.EmailCodes.FindAsync(idCode);
			if (ec == null) throw new Exception("Không tìm thấy thông tin mã code");
			ec.IsUsed = isUsed;
			await _context.SaveChangesAsync();
		}

		public async Task<EmailCode> GetEmailCode(int userId, string code)
		{
			return await _context.EmailCodes.FirstOrDefaultAsync(ec => ec.UserId == userId && ec.Code == code && ec.IsUsed == false);
		}
	}
}
