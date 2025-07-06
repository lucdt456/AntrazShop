using AntrazShop.Helper;
using AntrazShop.Models.DTOModels;

namespace AntrazShop.Interfaces.Services
{
	public interface IEmailService
	{
		Task<ServiceResponse<string>> SendEmailCode(string email);
		Task<ServiceResponse<string>> VerifyCodeRequest(VerifyCodeDTO dto);
	}
}
