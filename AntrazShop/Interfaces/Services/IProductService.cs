using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Interfaces.Services
{
	public interface IProductService
	{
		Task<ServiceResponse<(IEnumerable<ProductVM>, Paginate)>> GetProducts(int pg, int size);
		Task<ServiceResponse<(IEnumerable<ProductVM>, Paginate)>> SearchProducts(string? search, int pg, int size);
		Task<ServiceResponse<ProductVM>> GetProduct(int id);
		Task<ServiceResponse<string>> AddProduct(ProductDTO newProduct);
		Task<ServiceResponse<Product>> UpdateProduct(int id,  ProductDTO productUpdaTe);
		Task<ServiceResponse<bool>> DeleteProduct(int id);

	
	}
}
