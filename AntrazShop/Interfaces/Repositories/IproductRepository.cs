using AntrazShop.Data;

namespace AntrazShop.Interfaces.Repositories
{
	public interface IProductRepository
	{
		Task<int> GetTotalProductCount();
		Task<IEnumerable<Product>> SearchProducts(string search, int recSkip, int take);
		Task<Product> GetProduct(int id);
		Task<int> AddProduct(Product newProduct);
		Task<Product> UpdateProduct(int id, Product productUpdate);
		Task DeleteProduct(int id);
		Task<IEnumerable<Product>> GetProductsWithDetails(int recSkip, int take);
		Task<int> GetTotalProductCountSearch(string search);
		Task<bool> CheckProductNameExist(string name);
	}
}
