using AntrazShop.Data;
using AntrazShop.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AntrazShop.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly ShopDbContext _context;

		public ProductRepository(ShopDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Product>> GetProductsWithDetails(int recSkip, int take)
		{
			List<Product> products = await _context.Products
				.AsNoTracking()
				.OrderBy(p => p.Id)
				.Skip(recSkip)
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
			return products;
		}

		public async Task<IEnumerable<Product>> SearchProducts(string search, int recSkip, int take)
		{
			List<Product> products = await _context.Products
				.AsNoTracking()
				.OrderBy(p => p.Id)
				.Where(p => p.Name.Contains(search) || p.Id.ToString() == search)
				.Skip(recSkip)
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
			return products;
		}

		public async Task<Product> GetProduct(int id)
		{
			var product = await _context.Products
				.Include(p => p.Brand)
				.Include(p => p.Category)
				.Include(p => p.ColorCapacities)
					.ThenInclude(cc => cc.Color)
				.Include(p => p.ColorCapacities)
					.ThenInclude(cc => cc.Capacity)
				.Include(p => p.ColorCapacities)
					.ThenInclude(cc => cc.Reviews)
						.ThenInclude(r => r.User)
				.FirstOrDefaultAsync(p => p.Id == id);

			return product;
		}

		public async Task<int> AddProduct(Product newProduct)
		{
			await _context.Products.AddAsync(newProduct);
			await _context.SaveChangesAsync();
			return newProduct.Id;
		}

		public async Task<Product> UpdateProduct(int id, Product productUpdate)
		{
			Product product = await _context.Products.FindAsync(id);
			if (product != null)
			{
				product.Name = productUpdate.Name;
				product.DiscountAmount = productUpdate.DiscountAmount;
				product.Description = productUpdate.Description;
				product.ImageView = productUpdate.ImageView;
				product.BrandId = productUpdate.BrandId;
				product.CategoryId = productUpdate.CategoryId;
				product.ImageFolder = productUpdate.ImageFolder;

				_context.Products.Update(product);
				await _context.SaveChangesAsync();
				return product;
			}
			else throw new Exception("Không tìm thấy sản phẩm");
		}

		public async Task DeleteProduct(int id)
		{
			var product = await _context.Products.FindAsync(id);
			if (product != null)
			{
				_context.Products.Remove(product);
				await _context.SaveChangesAsync();
			}
			else throw new Exception("Không tìm thấy sản phẩm");
		}

		public async Task<int> GetTotalProductCount()
		{
			return await _context.Products.CountAsync();
		}

		public async Task<int> GetTotalProductCountSearch(string search)
		{
			return await _context.Products.Where(p => p.Name.Contains(search)).CountAsync();
		}
	}
}
