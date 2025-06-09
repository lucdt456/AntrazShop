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
		public async Task<(ServiceResponse<List<AccountVM>>, Paginate?)> GetUsers(int pg, int take)
		{
			var response = new ServiceResponse<List<AccountVM>>();
			try
			{
				var usersCount = await _accountManagerRepository.GetCountUsers();
				var pagination = new Paginate(usersCount, pg, take);
				int recskip = (pg - 1) * take;

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

		//2025/5/19 Hàm tạo tài khoản cho nhân sự
		public async Task<ServiceResponse<AccountVM>> CreateAccount(AccountDTO dto)
		{
			var response = new ServiceResponse<AccountVM>();
			try
			{
				var isExistEmail = await _accountManagerRepository.CheckExistEmail(dto.Email);
				if (isExistEmail)
				{
					response.IsSuccess = false;
					response.Errors.Add("Email đã tồn tại!");
					return response;
				}
				var avatarNameFile = "defaultAvt.png";


				avatarNameFile = FileNameHelper.EmailToSlug(dto.Email) + Path.GetExtension(dto.Avatar.FileName);

				var imageAvtPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "imgs", "avatar", avatarNameFile);

				try
				{
					using (var stream = new FileStream(imageAvtPath, FileMode.Create))
					{
						await dto.Avatar.CopyToAsync(stream);
					}
				}
				catch
				{
					response.IsSuccess = false;
					response.Errors.Add("Không thể lưu file ảnh");
					return response;
				}


				var passwordHasher = new PasswordHasher<User>();
				var user = new User
				{
					Name = dto.Name,
					Gender = dto.Gender,
					Email = dto.Email,
					PasswordHash = passwordHasher.HashPassword(new User(), dto.Password),
					PhoneNumber = dto.PhoneNumber,
					Avatar = avatarNameFile,
					Birthday = dto.Birthday,
					Hometown = dto.Hometown,
					workerAccount = dto.IsWorkerAccount,
				};

				try
				{
					user = await _accountManagerRepository.CreateAccount(user);
				}
				catch
				{
					response.IsSuccess = false;
					response.Errors.Add("Không thể tạo được người dùng");
					return response;
				}

				try
				{
					await _accountManagerRepository.AddRoles(user.Id, dto.Roles);
				}
				catch
				{
					response.IsSuccess = false;
					response.Errors.Add("Không thể thêm role cho người dùng!");
					return response;
				}

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
