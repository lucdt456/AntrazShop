using AntrazShop.Data;
using AntrazShop.Models;
using AntrazShop.Models.ViewModels;

namespace AntrazShop.Services.Interfaces
{
	public interface ICategoryService
	{
		Task<(IEnumerable<CategoryVM>, Paginate)> GetCategories(int pg, int size);
		Task<Category> GetCategory(int id);
		Task CreateCategory(Category category);
		Task<Category> UpdateCategory(int id, Category newCategory);
		Task<bool> DeleteCategory(int id);
	}
}
