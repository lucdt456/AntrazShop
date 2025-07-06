using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Interfaces.Services;
using AntrazShop.Models.DTOModels;

namespace AntrazShop.Services
{
	public class EmailService : IEmailService
	{
		private readonly IAccountRepository _accountRepository;
		private readonly IEmailRepository _emailRepository;
		private readonly IEmailSender _emailSender;
		private readonly IAccountService _accountService;
		public EmailService(IEmailSender emailSender,
			IAccountRepository accountRepository,
			IEmailRepository emailRepository,
			IAccountService accountService)
		{
			_accountRepository = accountRepository;
			_emailRepository = emailRepository;
			_emailSender = emailSender;
			_accountService = accountService;
		}

		public async Task<ServiceResponse<string>> SendEmailCode(string email)
		{
			var response = new ServiceResponse<string>();

			try
			{
				Random random = new Random();
				var user = await _accountRepository.FindUser(email);

				var emailCode = new EmailCode()
				{
					UserId = user.Id,
					Code = random.Next(100000, 1000000).ToString(),
					CreatedAt = DateTime.Now,
					ExpiresAt = DateTime.Now.AddMinutes(15),
					IsUsed = false
				};

				await _emailRepository.CreateEmailCode(emailCode);

				var emailBody = $@"
                <h2>Mã xác nhận của bạn</h2>
                <p>Mã: <strong style='font-size: 24px; color: blue;'>{emailCode.Code}</strong></p>
                <p>Mã này hết hạn sau 15 phút.</p>";

				// Gửi email mã xác nhận
				await _emailSender.SendEmailAsync(email, "Mã xác nhận - AntrazShop", emailBody);

				response.Data = "Gửi mail thành công!";
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}

			return response;
		}


		public async Task<ServiceResponse<string>> VerifyCodeRequest(VerifyCodeDTO dto)
		{
			var response = new ServiceResponse<string>();
			try
			{
				var user = await _accountRepository.FindUser(dto.Email);
				var ec = await _emailRepository.GetEmailCode(user.Id, dto.Code);
				if(ec == null)
				{
					response.IsSuccess = false;
					response.Errors.Add("Bạn đã nhập sai mã xác nhận. Vui lòng thử lại!");
					return response;
				}

				if(ec.ExpiresAt < DateTime.Now)
				{
					response.IsSuccess = false;
					response.Errors.Add("Mã xác nhận đã hết hạn");
					return response;
				}

				var random = new Random();
				const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
				var newPassword = new string(Enumerable.Repeat(chars, 8)
					.Select(s => s[random.Next(s.Length)]).ToArray());

				var responseSetPassword = await _accountService.SetPassword(user.Id, newPassword);

				if(responseSetPassword.IsSuccess == false)
				{
					response.IsSuccess = false;
					response.Errors = responseSetPassword.Errors;
					return response;
				}


				var emailBody = $@"
                <h2>Đổi mật khẩu thành công!</h2>
                <p>Mật khẩu mới của bạn là: <strong style='font-size: 24px; color: blue;'>{newPassword}</strong></p>
                <p>Vui lòng đổi lại mật khẩu trong cài đặt để đảm bảo an toàn.</p>";

				// Gửi email mã xác nhận
				await _emailSender.SendEmailAsync(dto.Email, "Xác nhận đổi mật khẩu thành công", emailBody);
				response.Data = "Mật khẩu đã được gửi về email!";

			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}

			return response;
		}
	}
}
