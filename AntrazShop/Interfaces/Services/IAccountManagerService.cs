using AntrazShop.Helper;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Interfaces.Services
{
	public interface IAccountManagerService
	{
		Task<(ServiceResponse<List<AccountVM>>, Paginate?)> GetUsers(int pg, int take);
		Task<ServiceResponse<AccountVM>> CreateAccount(AccountDTO dto);
	}
}
