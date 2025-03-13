using AntrazShop.Data;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;
using AntrazShop.Repositories;
using AntrazShop.Repositories.Interfaces;
using AntrazShop.Services.Interfaces;

namespace AntrazShop.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;

		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<IEnumerable<ProductVM>> GetProducts()
		{
			return await _productRepository.GetProducts();
		}

		public async Task<ProductVM?> GetProduct(int id)
		{
			return await _productRepository.GetProduct(id);
		}

		public async Task<Product> AddProduct(ProductDTO newProduct)
		{
			return await _productRepository.AddProduct(newProduct);
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
