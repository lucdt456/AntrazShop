using AntrazShop.Data;

namespace AntrazShop.Interfaces.Repositories
{
	public interface ICategoryRepository
	{
		Task<IEnumerable<Category>> GetCategorys(int recskip, int take);
		Task<Category> GetCategory(int id);
		Task CreateCategory(Category category);
		Task<Category> UpdateCategory(int id, Category newCategory);
		Task<bool> DeleteCategory(int id);
		Task<int> getTotalCategories();
		Task<int> getCategoryProductCounts(int id);
	}
}
