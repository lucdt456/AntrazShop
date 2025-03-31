using AntrazShop.Data;
using AntrazShop.Interfaces.Repositories;
using AntrazShop.Interfaces.Services;
using AntrazShop.Models;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;
namespace AntrazShop.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;

		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<(IEnumerable<ProductVM>, Paginate)> GetProducts(int pg, int size)
		{
			var totalProducts = await _productRepository.GetTotalProductCount();

			var pagination = new Paginate(totalProducts, pg, size);
			int recSkip = (pg - 1) * size;
			var products = await _productRepository.GetProducts(recSkip, size);

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
				Stock = p.Stock,
				status = p.status
			});
			return (productVMs, pagination);
		}

		public async Task<(IEnumerable<ProductVM>, Paginate)> SearchProducts(string search, int pg, int size)
		{
			var count = await _productRepository.GetTotalProductCountSearch(search);
			var panigation = new Paginate(count, pg, size);
			int recSkip = (pg - 1) * size;
			var products = await _productRepository.SearchProducts(search, recSkip, size);
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
				Stock = p.Stock,
				status = p.status
			});
			return (productVMs, panigation);
		}

		public async Task<ProductVM> GetProduct(int id)
		{
			var product = await _productRepository.GetProduct(id);
			if(product != null)
			{
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
					Stock = product.Stock,
					status = product.status
				};
				return productVM;
			}
			return null;
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
			return await _productRepository.AddProduct(product);
		}
		public async Task<Product> UpdateProduct(int id, ProductDTO productUpdate)
		{
			return await _productRepository.UpdateProduct(id, productUpdate);
		}

		public async Task<bool> DeleteProduct(int id)
		{
			return await _productRepository.DeleteProduct(id);
		}
	}
}
