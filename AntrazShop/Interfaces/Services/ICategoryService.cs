using AntrazShop.Data;
using AntrazShop.Helper;
using AntrazShop.Models.DTOModels;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Interfaces.Services
{
	public interface ICategoryService
	{
		Task<ServiceResponse<IEnumerable<CategoryVM>>> GetCategories();
		Task<ServiceResponse<CategoryVM>> GetCategory(int id);
		Task<ServiceResponse<CategoryVM>> CreateCategory(CategoryDTO dto);
		Task<ServiceResponse<CategoryVM>> UpdateCategory(int id, CategoryDTO dto);
		Task<ServiceResponse<string>> DeleteCategory(int id);
	}
}
