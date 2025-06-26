using AntrazShop.Data;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;
using AutoMapper;

namespace AntrazShop.Helper
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			// Role -> RoleVM
			CreateMap<Role, RoleVM>();

			// User -> AccountVM
			CreateMap<User, AccountVM>()
				.ForMember(dest => dest.Roles, opt => opt.MapFrom(src =>
					src.UserRoles.Select(ur => ur.Role).ToList()))
				.ForMember(dest => dest.isActive, opt => opt.MapFrom(src =>
					src.UserAuthInfo.IsActive));

			// BrandDTO -> Brand
			CreateMap<BrandDTO, Brand>();

			// Brand -> BrandVM
			CreateMap<Brand, BrandVM>()
				.ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src =>
					src.Products != null ? src.Products.Count : 0));

			// CategoryDTO -> Category
			CreateMap<CategoryDTO, Category>();

			// Category -> CategoryVM
			CreateMap<Category, CategoryVM>()
				.ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src =>
					src.Products != null ? src.Products.Count : 0));
		}
	}
}