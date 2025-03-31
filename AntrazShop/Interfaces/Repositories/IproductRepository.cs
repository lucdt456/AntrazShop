using AntrazShop.Data;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Interfaces.Repositories
{
	public interface IProductRepository
	{
		Task<int> GetTotalProductCount();
		Task<IEnumerable<Product>> GetProducts(int recSkip, int take);
		Task<int> GetTotalProductCountSearch(string search);
		Task<IEnumerable<Product>> SearchProducts(string search, int recSkip, int take);
		Task<Product> GetProduct(int id);
		Task<Product> AddProduct(Product newProduct);
		Task<Product?> UpdateProduct(int id, ProductDTO productUpdate);
		Task<bool> DeleteProduct(int id);
	}
}
