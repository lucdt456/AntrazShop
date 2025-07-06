using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Interfaces.Services;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Services
{
	public class PermissionService : IPermissionService
	{
		private readonly IPermissionRepository _permissionRepository;

		public PermissionService(IPermissionRepository permissionRepository)
		{
			_permissionRepository = permissionRepository;
		}

		public async Task<(ServiceResponse<List<PermissionVM>>, Paginate)> GetPermissions(int pg, int take)
		{
			var response = new ServiceResponse<List<PermissionVM>>();
			try
			{
				var count = await _permissionRepository.GetPermissionCount();
				var pagination = new Paginate(count, pg, take);
				int recskip = (pg - 1) * take;
				var permissions = await _permissionRepository.GetPermissions(recskip, take);
				var permissionVMs = permissions.Select(p => new PermissionVM
				{
					Id = p.Id,
					Name = p.Name,
					Description = p.Description,
					NameController = p.NameController
				}).ToList();

				response.Data = permissionVMs;
				return (response, pagination);
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add("Lỗi: " + ex.Message);
				return (response, null); ;
			}
		}

		public async Task<ServiceResponse<PermissionVM>> CreatePermission(PermissionDTO newPermission)
		{
			var response = new ServiceResponse<PermissionVM>();
			try
			{
				var permission = new Permission
				{
					Name = newPermission.Name,
					Description = newPermission.Description,
					NameController = newPermission.NameController
				};

				permission = await _permissionRepository.CreatePermissions(permission);
				var permissionVM = new PermissionVM
				{
					Id = permission.Id,
					Name = permission.Name,
					Description = permission.Description,
					NameController = permission.NameController
				};

				response.Data = permissionVM;
			}
			catch (Exception ex)
			{
				response.IsSuccess = false;
				response.Errors.Add(ex.Message);
			}
			return response;
		}

		public async Task<ServiceResponse<List<PermissionGroupVM>>> GetPermissionGroups()
		{
			var response = new ServiceResponse<List<PermissionGroupVM>>();
			try
			{
				var groups = await _permissionRepository.GetPermissionGroup();
				var groupVMs = new List<PermissionGroupVM>();
				foreach (var group in groups)
				{
					var groupVM = new PermissionGroupVM()
					{
						IdGroups = group.Id,
						GroupName = group.GroupName
					};

					foreach (var permission in group.Permissions)
					{
						var permissionVM = new PermissionVM
						{
							Id = permission.Id,
							Name = permission.Name,
							Description = permission.Description,
							NameController = permission.NameController
						};
						groupVM.Permissions.Add(permissionVM);
					}
					groupVMs.Add(groupVM);
				}
				response.Data = groupVMs;
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
