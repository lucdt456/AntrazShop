using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Interfaces.Services;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace AntrazShop.Services
{
	public class AccountManagerService : IAccountManagerService
	{
		private IAccountManagerRepository _accountManagerRepository;
		public AccountManagerService(IAccountManagerRepository accountManagerRepository)
		{
			_accountManagerRepository = accountManagerRepository;
		}

		// Hàm lấy danh sách người dùng
		public async Task<(ServiceResponse<List<AccountVM>>, Paginate)> GetUsers(int pg, int take)
		{
			var response = new ServiceResponse<List<AccountVM>>();
			try
			{
				var usersCount = await _accountManagerRepository.GetCountUsers();
				var pagination = new Paginate(usersCount, pg, take);
				int recskip = (pg - 1) * take;
				recskip = (recskip < 0) ? 1 : recskip;

				var users = await _accountManagerRepository.GetUsers(recskip, take);

				var usersVMs = new List<AccountVM>();

				foreach (var user in users)
				{
					List<string> userRoles = user.UserRoles
								  .Select(ur => ur.Role.Name)
								  .ToList();

					var userVM = new AccountVM
					{
						Id = user.Id,
						Name = user.Name,
						Email = user.Email,
						Gender = user.Gender,
						Address = user.Address,
						Birthday = user.Birthday,
						Hometown = user.Hometown,
						Avatar = user.Avatar,
						workerAccount = user.workerAccount,
						CreateAt = user.CreatedAt,
						Roles = userRoles
					};
					usersVMs.Add(userVM);
				}
				response.Data = usersVMs;
				return (response, pagination);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add("Lỗi: " + ex.Message);
				return (response, null);
			}
		}

		/// <summary>
		/// Hàm lấy-tìm kiếm danh sách người dùng
		/// </summary>
		/// <param name="pg"></param>
		/// <param name="take"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<(ServiceResponse<List<AccountVM>>, Paginate)> GetWorkerAccounts(string search, int pg, int take)
		{
			var response = new ServiceResponse<List<AccountVM>>();
			try
			{
				search ??= "";
				var usersCount = await _accountManagerRepository.GetCountSearchWorkerAccounts(search);
				var pagination = new Paginate(usersCount, pg, take);
				int recskip = (pg - 1) * take;
				recskip = (recskip < 0) ? 1 : recskip;

				var users = await _accountManagerRepository.SearchWorkerAccounts(search, recskip, take);

				var usersVMs = new List<AccountVM>();

				foreach (var user in users)
				{
					List<string> userRoles = user.UserRoles
								  .Select(ur => ur.Role.Name)
								  .ToList();

					var userVM = new AccountVM
					{
						Id = user.Id,
						Name = user.Name,
						Email = user.Email,
						Gender = user.Gender,
						Address = user.Address,
						Birthday = user.Birthday,
						Hometown = user.Hometown,
						Avatar = user.Avatar,
						workerAccount = user.workerAccount,
						CreateAt = user.CreatedAt,
						Roles = userRoles
					};
					usersVMs.Add(userVM);
				}
				response.Data = usersVMs;
				return (response, pagination);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add("Lỗi: " + ex.Message);
				return (response, null);
			}
		}

		/// <summary>
		/// Lấy - tìm kiếm danh sách tài khoản khách hàng
		/// </summary>
		/// <param name="search"></param>
		/// <param name="pg"></param>
		/// <param name="take"></param>
		/// <returns></returns>
		public async Task<(ServiceResponse<List<AccountVM>>, Paginate)> GetCustomerAccounts(string search, int pg, int take)
		{
			var response = new ServiceResponse<List<AccountVM>>();
			try
			{
				search ??= "";
				var usersCount = await _accountManagerRepository.GetCountSearchCustomerAccounts(search);
				var pagination = new Paginate(usersCount, pg, take);
				int recskip = (pg - 1) * take;
				recskip = (recskip < 0) ? 1 : recskip;

				var users = await _accountManagerRepository.SearchCustomerAccounts(search, recskip, take);

				var usersVMs = new List<AccountVM>();

				foreach (var user in users)
				{
					List<string> userRoles = user.UserRoles
								  .Select(ur => ur.Role.Name)
								  .ToList();

					var userVM = new AccountVM
					{
						Id = user.Id,
						Name = user.Name,
						Email = user.Email,
						Gender = user.Gender,
						Address = user.Address,
						Birthday = user.Birthday,
						Hometown = user.Hometown,
						Avatar = user.Avatar,
						workerAccount = user.workerAccount,
						CreateAt = user.CreatedAt,
						Roles = userRoles
					};
					usersVMs.Add(userVM);
				}
				response.Data = usersVMs;
				return (response, pagination);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add("Lỗi: " + ex.Message);
				return (response, null);
			}
		}

		//2025/5/19 Hàm tạo tài khoản cho nhân sự
		public async Task<ServiceResponse<AccountVM>> CreateAccount(AccountDTO dto)
		{
			var response = new ServiceResponse<AccountVM>();
			try
			{
				//Check Email tồn tại
				var isExistEmail = await _accountManagerRepository.CheckExistEmail(dto.Email);
				if (isExistEmail)
				{
					response.IsSuccess = false;
					response.Errors.Add("Email đã tồn tại!");
					return response;
				}

				//Tên avatar mặc định
				var avatarNameFile = "defaultAvt.png";


				//Nếu có ảnh avatar gửi vào
				if (dto.Avatar != null)
				{
					avatarNameFile = FileNameHelper.EmailToSlug(dto.Email) + Path.GetExtension(dto.Avatar.FileName);

					var imageAvtPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "imgs", "avatar", avatarNameFile);

					using (var stream = new FileStream(imageAvtPath, FileMode.Create))
					{
						await dto.Avatar.CopyToAsync(stream);
					}
				}

				//Mã hoá mật khẩu
				var passwordHasher = new PasswordHasher<User>();
				var user = new User
				{
					Name = dto.Name.Trim(),
					Gender = dto.Gender.Trim(),
					Email = dto.Email.Trim(),
					PasswordHash = passwordHasher.HashPassword(new User(), dto.Password),
					PhoneNumber = dto.PhoneNumber.Trim(),
					Avatar = avatarNameFile,
					Birthday = dto.Birthday,
					Address = dto.Address.Trim(),
					Hometown = dto.Hometown.Trim(),
					workerAccount = dto.IsWorkerAccount,
				};

				//Tạo tài khoản
				user = await _accountManagerRepository.CreateAccount(user);

				await _accountManagerRepository.AddRoles(user.Id, dto.Roles);

				//Truyền lại viewmodel
				var userVM = new AccountVM
				{
					Id = user.Id,
					Name = user.Name,
					Email = user.Email,
					Gender = user.Gender,
					Address = user.Address,
					Birthday = user.Birthday,
					Hometown = user.Hometown,
					Avatar = user.Avatar,
					workerAccount = user.workerAccount,
					CreateAt = user.CreatedAt
				};
				response.Data = userVM;
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add("Lỗi: " + ex.Message);
			}
			return response;
		}

		/// <summary>
		/// Hàm sửa Tài khoản
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="dTO"></param>
		/// <returns></returns>
		public async Task<ServiceResponse<AccountVM>> EditAccount(int userId, AccountDTO dTO)
		{
			// CHƯA FIX NẾU FILE ẢNH LÀ NULL MÀ TÊN NGƯỜI DÙNG THAY ĐỔI => ĐỔI TÊN
			var response = new ServiceResponse<AccountVM>();
			try
			{
				//Lấy tài khoản
				var user = await _accountManagerRepository.GetUser(userId);
				string avatarNameFile = user.Avatar;

				avatarNameFile = FileNameHelper.EmailToSlug(user.Email) + Path.GetExtension(dTO.Avatar.FileName);

				var imageAvtPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "imgs", "avatar", avatarNameFile);

				//xử lý ảnh
				if (dTO.Avatar != null)
				{
					//Nếu khác image mặc định thì xoá file ảnh
					if (user.Avatar != "defaultAvt.png")
					{
						File.Delete(imageAvtPath);
					}

					imageAvtPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "imgs", "avatar", avatarNameFile);

					using (var stream = new FileStream(imageAvtPath, FileMode.Create))
					{
						await dTO.Avatar.CopyToAsync(stream);
					}
				}


				//Cập nhật dữ liệu
				user.Name = dTO.Name;
				user.Gender = dTO.Gender;
				user.PhoneNumber = dTO.PhoneNumber;
				user.Avatar = avatarNameFile;
				user.Birthday = dTO.Birthday;
				user.Address = dTO.Address;
				user.Hometown = dTO.Hometown;
				user.workerAccount = dTO.IsWorkerAccount;

				user = await _accountManagerRepository.UpdateUser(userId, user);

				//Update roles
				var listCurrentRoles = user.UserRoles.Select(ur => ur.Role.Id);
				var newRoles = dTO.Roles.Except(listCurrentRoles).ToList();
				var rolesDelete = listCurrentRoles.Except(dTO.Roles).ToList();

				await _accountManagerRepository.DeleteRoles(userId, rolesDelete);
				await _accountManagerRepository.AddRoles(userId, newRoles);


				var userVM = new AccountVM
				{
					Id = user.Id,
					Name = user.Name,
					Email = user.Email,
					Gender = user.Gender,
					Address = user.Address,
					Birthday = user.Birthday,
					Hometown = user.Hometown,
					Avatar = user.Avatar,
					workerAccount = user.workerAccount,
					CreateAt = user.CreatedAt
				};
				response.Data = userVM;
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add("Lỗi: " + ex.Message);
			}
			return response;
		}


	}
}
