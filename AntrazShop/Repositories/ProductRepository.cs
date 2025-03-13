using AntrazShop.Data;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;
using AntrazShop.Repositories.Interfaces;
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

		public async Task<IEnumerable<ProductVM>> GetProducts()
		{
			List<Product> products = await _context.Products.Include(p => p.Brand).Include(p => p.Category).ToListAsync();
			var productVMs = products.Select(p => new ProductVM
			{
				Id = p.Id,
				Name = p.Name,
				Price = p.Price,
				DiscountAmount = p.DiscountAmount,
				Description = p.Description,
				ImageView = p.ImageView,
				Brand = p.Brand.Name,
				Category = p.Category.Name,
				Stock = p.Stock
			});
			return productVMs;
		}

		
		public async Task<ProductVM?> GetProduct(int id)
		{
			var product = await _context.Products.Include(p => p.Brand).Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
			var productVM = new ProductVM
			{
				Id = product.Id,
				Name = product.Name,
				Price = product.Price,
				DiscountAmount = product.DiscountAmount,
				Description = product.Description,
				ImageView = product.ImageView,
				Brand = product.Brand.Name,
				Category = product.Category.Name,
				Stock = product.Stock
			};
			return productVM;
		}

		public async Task<Product> AddProduct(ProductDTO newProduct)
		{
			var product = new Product
			{
				Name = newProduct.Name,
				Price = newProduct.Price,
				DiscountAmount = newProduct.DiscountAmount,
				Description = newProduct.Description,
				ImageView = newProduct.ImageView,
				BrandId = newProduct.BrandId,
				CategoryId = newProduct.CategoryId,
				status = newProduct.status,
				Stock = newProduct.Stock
			};

			await _context.Products.AddAsync(product);
			await _context.SaveChangesAsync();
			return product;
		}

		public async Task<Product?> UpdateProduct(int id, ProductDTO productUpdate)
		{
			Product product = await _context.Products.FindAsync(id);
			if (product != null)
			{
				product.Name = productUpdate.Name;
				product.Price = productUpdate.Price;
				product.DiscountAmount = productUpdate.DiscountAmount;
				product.Description = productUpdate.Description;
				product.ImageView = productUpdate.ImageView;
				product.BrandId = productUpdate.BrandId;
				product.CategoryId = productUpdate.CategoryId;
				product.status = productUpdate.status;
				product.Stock = productUpdate.Stock;

				_context.Products.Update(product);
				await _context.SaveChangesAsync();
				return product;
			}
			else return null;
		}

		public async Task<bool> DeleteProduct(int id)
		{
			var product = await _context.Products.FindAsync(id);
			if (product != null)
			{
				_context.Products.Remove(product);
				await _context.SaveChangesAsync();
				return true;
			}
			else return false;
		}

	}
}
