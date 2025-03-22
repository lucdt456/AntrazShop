using AntrazShop.Data;
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
		public async Task<IEnumerable<Brand>> GetBrands()
		{
			return await _context.Brands.ToListAsync();
		}

		public async Task<Brand> GetBrand(int id)
		{
			return await _context.Brands.FindAsync(id);
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
	}
}
