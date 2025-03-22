using AntrazShop.Data;

namespace AntrazShop.Services.Interfaces
{
	public interface ICategoryService
	{
		Task<IEnumerable<Category>> GetCategorys();
		Task<Category> GetCategory(int id);
		Task CreateCategory(Category category);
		Task<Category> UpdateCategory(int id, Category newCategory);
		Task<bool> DeleteCategory(int id);
	}
}
