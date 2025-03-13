using AntrazShop.Data;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Repositories.Interfaces
{
	public interface IProductRepository
	{
		Task<IEnumerable<ProductVM>> GetProducts();
		Task<ProductVM?> GetProduct(int id);
		Task<Product> AddProduct(ProductDTO newProduct);
		Task<Product?> UpdateProduct(int id, ProductDTO productUpdate);
		Task<bool> DeleteProduct(int id);
	}
}
