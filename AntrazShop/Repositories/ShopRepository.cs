using AntrazShop.Data;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Models.DTOModels;
using Microsoft.EntityFrameworkCore;

namespace AntrazShop.Repositories
{
	public class ShopRepository : IShopRepository
	{
		private readonly ShopDbContext _context;

		public ShopRepository(ShopDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Product>> GetProducts(ProductFilter filter, int skip, int take)
		{
			switch (filter.AscendingPrice)
			{
				case true:
					return await _context.Products
						.AsNoTracking()
						.Include(p => p.Brand)
						.Include(p => p.Category)
						.Include(p => p.ColorCapacities)
							.ThenInclude(cc => cc.Color)
						.Include(p => p.ColorCapacities)
							.ThenInclude(cc => cc.Capacity)
						.Include(p => p.ColorCapacities)
							.ThenInclude(cc => cc.Reviews)
								.ThenInclude(r => r.User)
						.Where(p => !string.IsNullOrEmpty(filter.SearchText) ? p.Name.Contains(filter.SearchText) : true)
						.Where(p => filter.BrandIds.Any() ? filter.BrandIds.Contains(p.BrandId) : true)
						.Where(p => filter.CategoryIds.Any() ? filter.CategoryIds.Contains(p.CategoryId) : true)
						.Where(p => p.ColorCapacities.Any(cc => cc.Price <= filter.MaxPrice && cc.Price >= filter.MinPrice))
						.Select(p => new {
							Product = p,
							MinPrice = p.ColorCapacities
								.Where(cc => cc.Price <= filter.MaxPrice && cc.Price >= filter.MinPrice)
								.Min(cc => cc.Price)
						})
						.OrderBy(x => x.MinPrice)
						.Skip(skip)
						.Take(take)
						.Select(x => x.Product)
						.ToListAsync();

				case false:
					return await _context.Products
						.AsNoTracking()
						.Include(p => p.Brand)
						.Include(p => p.Category)
						.Include(p => p.ColorCapacities)
							.ThenInclude(cc => cc.Color)
						.Include(p => p.ColorCapacities)
							.ThenInclude(cc => cc.Capacity)
						.Include(p => p.ColorCapacities)
							.ThenInclude(cc => cc.Reviews)
								.ThenInclude(r => r.User)
						.Where(p => !string.IsNullOrEmpty(filter.SearchText) ? p.Name.Contains(filter.SearchText) : true)
						.Where(p => filter.BrandIds.Any() ? filter.BrandIds.Contains(p.BrandId) : true)
						.Where(p => filter.CategoryIds.Any() ? filter.CategoryIds.Contains(p.CategoryId) : true)
						.Where(p => p.ColorCapacities.Any(cc => cc.Price <= filter.MaxPrice && cc.Price >= filter.MinPrice))
						.Select(p => new {
							Product = p,
							MaxPrice = p.ColorCapacities
								.Where(cc => cc.Price <= filter.MaxPrice && cc.Price >= filter.MinPrice)
								.Max(cc => cc.Price)
						})
						.OrderByDescending(x => x.MaxPrice)
						.Skip(skip)
						.Take(take)
						.Select(x => x.Product)
						.ToListAsync();

				default:
					return await _context.Products
						.AsNoTracking()
						.Where(p => !string.IsNullOrEmpty(filter.SearchText) ? p.Name.Contains(filter.SearchText) : true)
						.Where(p => filter.BrandIds.Any() ? filter.BrandIds.Contains(p.BrandId) : true)
						.Where(p => filter.CategoryIds.Any() ? filter.CategoryIds.Contains(p.CategoryId) : true)
						.Where(p => p.ColorCapacities.Any(cc => cc.Price <= filter.MaxPrice && cc.Price >= filter.MinPrice))
						.OrderByDescending(p => p.Id)
						.Skip(skip)
						.Take(take)
						.Include(p => p.Brand)
						.Include(p => p.Category)
						.Include(p => p.ColorCapacities)
							.ThenInclude(cc => cc.Color)
						.Include(p => p.ColorCapacities)
							.ThenInclude(cc => cc.Capacity)
						.Include(p => p.ColorCapacities)
							.ThenInclude(cc => cc.Reviews)
								.ThenInclude(r => r.User)
						.ToListAsync();
			}
		}

		public async Task<int> GetTotalProductCountFilter(ProductFilter filter)
		{
			return await _context.Products
				.AsNoTracking()
				.Where(p => !string.IsNullOrEmpty(filter.SearchText) ? p.Name.Contains(filter.SearchText) : true)
				.Where(p => filter.BrandIds.Any() ? filter.BrandIds.Contains(p.BrandId) : true)
				.Where(p => filter.CategoryIds.Any() ? filter.CategoryIds.Contains(p.CategoryId) : true)
				.Where(p => p.ColorCapacities.Any(cc => cc.Price <= filter.MaxPrice && cc.Price >= filter.MinPrice))
				.CountAsync();
		}
	}
}