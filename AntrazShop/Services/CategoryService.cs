using AntrazShop.Data;
using AntrazShop.Repositories.Interfaces;
using AntrazShop.Services.Interfaces;

namespace AntrazShop.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;
		public CategoryService(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public async Task CreateCategory(Category category)
		{
			await _categoryRepository.CreateCategory(category);
		}

		public async Task<bool> DeleteCategory(int id)
		{
			return await _categoryRepository.DeleteCategory(id);
		}

		public async Task<Category> GetCategory(int id)
		{
			return await _categoryRepository.GetCategory(id);
		}

		public async Task<IEnumerable<Category>> GetCategorys()
		{
			return await _categoryRepository.GetCategorys();
		}

		public async Task<Category> UpdateCategory(int id, Category newCategory)
		{
			return await _categoryRepository.UpdateCategory(id, newCategory);
		}
	}
}
