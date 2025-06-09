using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Interfaces.Services;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;
using System.Data;

namespace AntrazShop.Services
{
	public class RoleService : IRoleService
	{
		private readonly IRoleResponsive _roleResponsive;
		private readonly IPermissionRepository _permissionRepository;
		public RoleService(IRoleResponsive roleResponsive, IPermissionRepository permissionRepository)
		{
			_roleResponsive = roleResponsive;
			_permissionRepository = permissionRepository;
		}

		public async Task<ServiceResponse<RoleVM>> CreateRole(RoleDTO roleDTO)
		{
			var response = new ServiceResponse<RoleVM>();
			try
			{
				var isExistRole = await _roleResponsive.CheckExistRole(roleDTO.Name);
				if (isExistRole)
				{
					response.IsSuccess = false;
					response.Errors.Add("Role đã tồn tại!");
					return response;
				}

				var role = new Role
				{
					Name = roleDTO.Name,
					Description = roleDTO.Description
				};
				role = await _roleResponsive.CreateRole(role);

				try
				{
					foreach (int permissionId in roleDTO.PermissionIds)
					{
						var rolePermission = new RolePermission
						{
							RoleId = role.Id,
							PermissionId = permissionId
						};
						await _roleResponsive.AddRolePermission(rolePermission);
					}
				}
				catch
				{
					response.IsSuccess = false;
					response.Errors.Add("Không thể thêm Permissions cho Role");
					return response;
				}

				role = await _roleResponsive.GetRole(role.Id);
				ICollection<string> userNames = role.UserRoles
														.Select(ur => ur.User)
														.Select(u => u.Name)
														.ToList();
				ICollection<Permission> permissions = role.RolePermissions
													 .Select(rp => rp.Permission)
													 .ToList();

				var permissionVMs = permissions.Select(p => new PermissionVM
				{
					Id = p.Id,
					Name = p.Name,
					Description = p.Description,
					NameController = p.NameController
				}).ToList();

				var roleVM = new RoleVM
				{
					Id = role.Id,
					Name = role.Name,
					Description = role.Description,
					CountUser = userNames.Count,
					UserNames = userNames,
					Permissions = permissionVMs
				};

				response.Data = roleVM;
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add("Lỗi: " + ex.Message);
			}
			return response;
		}

		public async Task<ServiceResponse<RoleVM>> EditRole(int id, RoleDTO dTO)
		{
			var response = new ServiceResponse<RoleVM>();

			var role = await _roleResponsive.GetRole(id);
			if(role.Name.ToLower() != dTO.Name.ToLower())
			{
				var isExistRole = await _roleResponsive.CheckExistRole(dTO.Name);
				if (isExistRole)
				{
					response.IsSuccess = false;
					response.Errors.Add("Role đã tồn tại!");
					return response;
				}
			}
			
			try
			{
				var roleUpdate = new Role
				{
					Name = dTO.Name,
					Description = dTO.Description
				};

				role = await _roleResponsive.EditRole(id, roleUpdate);
				response.Data = new RoleVM
				{
					Id = role.Id,
					Name = role.Name,
					Description = role.Description
				};
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add("Lỗi: " + ex.Message);
			}
			return response;
		}

		public async Task<(ServiceResponse<List<RoleVM>>, Paginate)> GetRoles(int pg, int take)
		{
			var response = new ServiceResponse<List<RoleVM>>();
			try
			{
				var count = await _roleResponsive.GetRoleCount();
				var pagination = new Paginate(count, pg, take);
				int recskip = (pg - 1) * take;
				var roles = await _roleResponsive.GetRoles(recskip, take);
				var roleVMs = new List<RoleVM>();
				foreach (var role in roles)
				{
					ICollection<string> userNames = role.UserRoles
														.Select(ur => ur.User)
														.Select(u => u.Name)
														.ToList();
					ICollection<Permission> permissions = role.RolePermissions
														 .Select(rp => rp.Permission)
														 .ToList();
					var permissionVMs = permissions.Select(p => new PermissionVM
					{
						Id = p.Id,
						Name = p.Name,
						Description = p.Description,
						NameController = p.NameController
					}).ToList();

					var countUser = userNames.Count();

					var roleVM = new RoleVM
					{
						Id = role.Id,
						Name = role.Name,
						Description = role.Description,
						CountUser = countUser,
						UserNames = userNames,
						Permissions = permissionVMs
					};
					roleVMs.Add(roleVM);
				}
				response.Data = roleVMs;
				return (response, pagination);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add("Lỗi: " + ex.Message);
				return (response, null); ;
			}
		}

		public async Task<ServiceResponse<RoleVM>> GetRole(int id)
		{
			var response = new ServiceResponse<RoleVM>();
			try
			{
				var role = await _roleResponsive.GetRole(id);
				ICollection<string> userNames = role.UserRoles
														.Select(ur => ur.User)
														.Select(u => u.Name)
														.ToList();
				ICollection<Permission> permissions = role.RolePermissions
													 .Select(rp => rp.Permission)
													 .ToList();

				var permissionVMs = permissions.Select(p => new PermissionVM
				{
					Id = p.Id,
					Name = p.Name,
					Description = p.Description,
					NameController = p.NameController
				}).ToList();

				var roleVM = new RoleVM
				{
					Id = role.Id,
					Name = role.Name,
					Description = role.Description,
					CountUser = userNames.Count,
					UserNames = userNames,
					Permissions = permissionVMs
				};

				response.Data = roleVM;
				return response;
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add("Lỗi: " + ex.Message);
				return response;
			}
		}

		public async Task<ServiceResponse<string>> DeleteRole(int id)
		{
			var response = new ServiceResponse<string>();
			try
			{
				await _roleResponsive.DeleteRole(id);
				response.Data = "Xoá thành công!";
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add("Lỗi: " + ex.Message);
			}
			return response;
		}

		public async Task<ServiceResponse<RoleVM>> EditRolePermission(int roleId, List<int> permissionIds)
		{
			var response = new ServiceResponse<RoleVM>();
			try
			{
				var oldPermissions = await _permissionRepository.GetRolePermissions(roleId);
				var newPermissions = permissionIds.Except(oldPermissions).ToList();
				var permissionsDelete = oldPermissions.Except(permissionIds).ToList();

				try
				{
					await _roleResponsive.AddRolePermissions(roleId, newPermissions);
				}
				catch
				{
					response.IsSuccess = false;
					response.Errors.Add("Lỗi không thêm được quyền hạn mới");
					return response;
				}

				try
				{
					await _roleResponsive.DeleteRolePermission(roleId, permissionsDelete);
				}
				catch
				{
					response.IsSuccess = false;
					response.Errors.Add("Lỗi không xoá được quyền hạn loại bỏ");
					return response;
				}
				var role = await _roleResponsive.GetRole(roleId);
				ICollection<string> userNames = role.UserRoles
														.Select(ur => ur.User)
														.Select(u => u.Name)
														.ToList();
				ICollection<Permission> permissions = role.RolePermissions
													 .Select(rp => rp.Permission)
													 .ToList();

				var permissionVMs = permissions.Select(p => new PermissionVM
				{
					Id = p.Id,
					Name = p.Name,
					Description = p.Description,
					NameController = p.NameController
				}).ToList();

				var roleVM = new RoleVM
				{
					Id = role.Id,
					Name = role.Name,
					Description = role.Description,
					CountUser = userNames.Count,
					UserNames = userNames,
					Permissions = permissionVMs
				};

				response.Data = roleVM;
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
