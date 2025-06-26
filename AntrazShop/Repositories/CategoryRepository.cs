using AntrazShop.Data;
using AntrazShop.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AntrazShop.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly ShopDbContext _context;
		public CategoryRepository(ShopDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Category>> GetCategories()
		{
			return await _context.Categories
				.AsNoTracking()
				.Include(c => c.Products)
				.ToListAsync();
		}

		public async Task<Category> GetCategory(int id)
		{
			var category = await _context.Categories
				.AsNoTracking()
				.Include(c => c.Products)
				.FirstOrDefaultAsync(c => c.Id == id);

			if (category == null) throw new Exception("Không tìm thấy danh mục");

			return category;
		}

		public async Task<Category> CreateCategory(Category category)
		{
			_context.Categories.Add(category);
			await _context.SaveChangesAsync();
			return category;
		}

		public async Task<Category> UpdateCategory(Category categoryUpdate)
		{
			var category = await _context.Categories.FindAsync(categoryUpdate.Id);
			if (category == null) throw new Exception("Không tìm thấy danh mục");

			category.Name = categoryUpdate.Name;
			category.Description = categoryUpdate.Description;
			category.Image = categoryUpdate.Image;

			await _context.SaveChangesAsync();
			return category;
		}

		public async Task DeletecCategory(int id)
		{
			var category = await _context.Categories.FindAsync(id);
			if (category == null) throw new Exception("Không tìm thấy danh mục");

			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();
		}
	}
}
