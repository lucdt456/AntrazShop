using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Interfaces.Services;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Services
{
	public class AccountManagerService : IAccountManagerService
	{
		private IAccountManagerRepository _accountManagerRepository;
		public AccountManagerService(IAccountManagerRepository accountManagerRepository)
		{
			_accountManagerRepository = accountManagerRepository;
		}

		public async Task<(IEnumerable<AccountVM>, Paginate)> GetUsers(int pg, int take)
		{
			var usersCount = await _accountManagerRepository.GetCountUsers();
			var pagination = new Paginate(usersCount, pg, take);
			int recskip = (pg - 1) * take;

			var users = await _accountManagerRepository.GetUsers(recskip, take);

			var usersVMs = new List<AccountVM>();

			foreach(var user in users)
			{
				List<string> userRoles = await _accountManagerRepository.GetUserRoles(user.Id);

				var userVM = new AccountVM
				{
					Id = user.Id,
					Name = user.Name,
					Email = user.Email,
					Gender = user.Gender,
					Avatar = user.Avatar,
					CreateAt = user.CreatedAt,
					Roles = userRoles
				};
				usersVMs.Add(userVM);
			}
			return (usersVMs, pagination);
		}
	}
}
