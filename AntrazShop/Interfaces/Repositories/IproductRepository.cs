using AntrazShop.Data;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Interfaces.Repositories
{
	public interface IProductRepository
	{
		Task<int> GetTotalProductCount();
		Task<IEnumerable<Product>> SearchProducts(string search, int recSkip, int take);
		Task<Product> GetProduct(int id);
		Task<int> AddProduct(Product newProduct);
		Task<Product?> UpdateProduct(int id, ProductDTO productUpdate);
		Task<bool> DeleteProduct(int id);
		Task<IEnumerable<Product>> GetProductsWithDetails(int recSkip, int take);
		Task<int> GetTotalProductCountSearch(string search);
	}
}
