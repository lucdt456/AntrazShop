using AntrazShop.Helper;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Interfaces.Services
{
	public interface IAccountManagerService
	{
		Task<(IEnumerable<AccountVM>, Paginate)> GetUsers(int pg, int take);
	}
}
