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

			// Review -> ProductReviewVM
			CreateMap<Review, ProductReviewVM>()
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name));

			// ColorCapacity -> ProductColorCapacityVM
			CreateMap<ColorCapacity, ProductColorCapacityVM>()
				.ForMember(dest => dest.ColorName, opt => opt.MapFrom(src => src.Color.NameColor))
				.ForMember(dest => dest.CapacityValue, opt => opt.MapFrom(src => src.Capacity.Value))
				.ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews));


			CreateMap<Product, ProductVM>()
				.ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
				.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
				.ForMember(dest => dest.FolderImage, opt => opt.MapFrom(src => src.ImageFolder))
				.ForMember(dest => dest.ProductCCs, opt => opt.MapFrom(src => src.ColorCapacities))
				.ForMember(dest => dest.MinPrice, opt => opt.MapFrom(src =>
					src.ColorCapacities.Any() ? src.ColorCapacities.Min(x => x.Price) : 0))
				.ForMember(dest => dest.MaxPrice, opt => opt.MapFrom(src =>
					src.ColorCapacities.Any() ? src.ColorCapacities.Max(x => x.Price) : 0))
				.ForMember(dest => dest.TotalStock, opt => opt.MapFrom(src =>
					src.ColorCapacities.Sum(cc => cc.Stock)))
				.ForMember(dest => dest.SoldAmount, opt => opt.MapFrom(src =>
					src.ColorCapacities.Sum(cc => cc.SoldAmount)))
				.ForMember(dest => dest.Rating, opt => opt.MapFrom(src =>
					Math.Round(src.ColorCapacities
						.SelectMany(cc => cc.Reviews)
						.Select(r => r.Rating)
						.DefaultIfEmpty(0)
						.Average(), 1)))
				.AfterMap((src, dest) =>
				{
					var colorCapacities = src.ColorCapacities?.ToList() ?? new List<ColorCapacity>();
					if (colorCapacities.Any())
					{
						var status = colorCapacities.Max(cc => cc.Status);
						if (dest.TotalStock == 0) status = 2;
						dest.Status = status;
					}
				});

			CreateMap<Cart, CartItemVM>()
			   .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ColorCapacity.Product.Name))
			   .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ColorCapacity.Product.Id))
			   .ForMember(dest => dest.DiscountAmount, opt => opt.MapFrom(src => src.ColorCapacity.Product.DiscountAmount))
				.ForMember(dest => dest.FolderImage, opt => opt.MapFrom(src => src.ColorCapacity.Product.ImageFolder))
			   .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.ColorCapacity.Image))
			   .ForMember(dest => dest.ColorName, opt => opt.MapFrom(src => src.ColorCapacity.Color.NameColor))
			   .ForMember(dest => dest.CapacityValue, opt => opt.MapFrom(src => src.ColorCapacity.Capacity.Value))
			   .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ColorCapacity.Price))
			   .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.ColorCapacity.Stock));

			CreateMap<OrderDetail, CartItemVM>()
		   .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ColorCapacity.Product.Name))
			.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ColorCapacity.Product.Id))
		   .ForMember(dest => dest.DiscountAmount, opt => opt.MapFrom(src => src.ColorCapacity.Product.DiscountAmount))
		   .ForMember(dest => dest.FolderImage, opt => opt.MapFrom(src => src.ColorCapacity.Product.ImageFolder))
		   .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.ColorCapacity.Image))
		   .ForMember(dest => dest.ColorName, opt => opt.MapFrom(src => src.ColorCapacity.Color.NameColor))
		   .ForMember(dest => dest.CapacityValue, opt => opt.MapFrom(src => src.ColorCapacity.Capacity.Value))
		   .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ColorCapacity.Price))
		   .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.ColorCapacity.Stock))
		   .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));


			CreateMap<OrderStatusLog, OrderStatusLogVM>()
				.ForMember(dest => dest.Status, opt => opt!.MapFrom(src => src.Status))
				.ForMember(dest => dest.CreateAt, opt => opt!.MapFrom(src => src.CreateAt));

			CreateMap<Order, OrderVM>()
		   .ForMember(dest => dest.OrderCode, opt => opt.MapFrom(src => src.OrderCode))
		   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
		   .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => src.CreatedAt))
		   .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Adress))
		   .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
		   .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
		   .ForMember(dest => dest.LastStatusDate, opt => opt.MapFrom(src => src.OrderStatusLogs.Any() ? src.OrderStatusLogs.OrderByDescending(osl => osl.CreateAt).First().CreateAt : (DateTime?)null))
		   .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
		   .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails))
			.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
			.ForMember(dest => dest.OrderStatusLogVMs, opt => opt.MapFrom(src => src.OrderStatusLogs));
		}
	}
}