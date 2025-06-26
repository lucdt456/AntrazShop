using AntrazShop.Data;

namespace AntrazShop.Interfaces.Repositories
{
	public interface ICategoryRepository
	{
		Task<IEnumerable<Category>> GetCategories();
		Task<Category> GetCategory(int id);
		Task<Category> CreateCategory(Category category);
		Task<Category> UpdateCategory(Category category);
		Task DeletecCategory(int id);
	}
}
