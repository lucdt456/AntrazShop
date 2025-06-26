using AntrazShop.Data;
using AntrazShop.Interfaces.Repositories;
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
			return await _context.Brands
				.AsNoTracking()
				.Include(b => b.Products)
				.ToListAsync();
		}

		public async Task<Brand> GetBrand(int id)
		{
			var brand = await _context.Brands
				.AsNoTracking()
				.Include(b => b.Products)
				.FirstOrDefaultAsync(b => b.Id == id);

			if (brand == null) throw new Exception("Không tìm thấy thương hiệu");

			return brand;
		}

		public async Task<Brand> CreateBrand(Brand brand)
		{
			_context.Brands.Add(brand);
			await _context.SaveChangesAsync();
			return brand;
		}

		public async Task<Brand> UpdateBrand(Brand brandUpdate)
		{
			var brand = await _context.Brands.FindAsync(brandUpdate.Id);
			if (brand == null) throw new Exception("Không tìm thấy thương hiệu");

			brand.Name = brandUpdate.Name;
			brand.Description = brandUpdate.Description;
			brand.Logo = brandUpdate.Logo;

			await _context.SaveChangesAsync();
			return brand;
		}

		public async Task DeleteBrand(int id)
		{
			var brand = await _context.Brands.FindAsync(id);
			if (brand == null) throw new Exception("Không tìm thấy thương hiệu");

			_context.Remove(brand);
			await _context.SaveChangesAsync();
		}
	}
}
