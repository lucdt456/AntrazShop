using AntrazShop.Data;
using AntrazShop.Models.ViewModels;
using AntrazShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AntrazShop.Repositories
{
	public class BrandRepository : IBrandRepository
	{
		private readonly ShopDbContext _context;
		public BrandRepository(ShopDbContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<Brand>> GetBrands(int recskip, int take)
		{
			return await _context.Brands.Skip(recskip).Take(take).ToListAsync();
		}

		public async Task<BrandVM> GetBrand(int id)
		{
			var brand = await _context.Brands.FindAsync(id);
			var productCount = _context.Products.Count(p => p.BrandId == id);
			var brandVM = new BrandVM
			{
				Id = brand.Id,
				Name = brand.Name,
				Description = brand.Description,
				Logo = brand.Logo,
				ProductCount = productCount
			};

			return brandVM;
		}

		public async Task CreateBrand(Brand brand)
		{
			if (brand != null)
			{
				await _context.Brands.AddAsync(brand);
				await _context.SaveChangesAsync();
			}
		}
		public async Task<Brand> UpdateBrand(int id, Brand newBrand)
		{
			var brand = await _context.Brands.FindAsync(id);
			if (brand != null)
			{
				brand.Name = newBrand.Name;
				brand.Description = newBrand.Description;
				brand.Logo = newBrand.Logo;

				_context.Brands.Update(brand);
				await _context.SaveChangesAsync();
				return brand;
			}
			return null;
		}

		public async Task<bool> DeleteBrand(int id)
		{
			var brand = await _context.Brands.FindAsync(id);
			if (brand != null)
			{
				_context.Remove(brand);
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<int> GetTotalBrands()
		{
			return await _context.Brands.CountAsync();
		}

		public async Task<int> GetBrandProductCounts(int brandId)
		{
			return await _context.Products.CountAsync(p => p.BrandId == brandId);
		}
	}
}
