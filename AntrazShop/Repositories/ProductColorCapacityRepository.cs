using AntrazShop.Data;
using AntrazShop.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AntrazShop.Repositories
{
	public class ProductColorCapacityRepository : IProductColorCapacityRepository
	{
		private ShopDbContext _context;
		public ProductColorCapacityRepository(ShopDbContext context)
		{
			_context = context;
		}
		public async Task<int> AddCapacity(string capacityValue)
		{
			var capacity = await _context.Capacities.FirstOrDefaultAsync(c => c.Value == capacityValue);
			if (capacity != null)
			{
				return capacity.Id;
			}
			else
			{
				capacity = new Capacity
				{
					Value = capacityValue
				};
				await _context.Capacities.AddAsync(capacity);
				await _context.SaveChangesAsync();
				return capacity.Id;
			}
		}

		public async Task<int> AddColor(string colorName)
		{
			var color = await _context.Colors.FirstOrDefaultAsync(c => c.NameColor == colorName);
			if (color != null)
			{
				return color.Id;
			}
			else
			{
				color = new Color
				{
					NameColor = colorName
				};
				await _context.AddAsync(color);
				await _context.SaveChangesAsync();
				return color.Id;
			}
		}

		public async Task AddColorCapacity(ColorCapacity colorCapacity)
		{
			await _context.ColorCapacities.AddAsync(colorCapacity);
			await _context.SaveChangesAsync();
		}


		public async Task<ColorCapacity> EditColorCapacity(int id, ColorCapacity colorCapacityUpdate)
		{
			//var colorCapacity = await _context.ColorCapacities
			//	.Include(c => c.Color)
			//	.Include(ca => ca.Capacity)
			//	.Where(cc => cc.Id == id)
			//	.FirstOrDefaultAsync();

			var colorCapacity = await _context.ColorCapacities.FindAsync(id);

			if (colorCapacity != null)
			{
				colorCapacity.Stock = colorCapacityUpdate.Stock;
				colorCapacity.Price = colorCapacityUpdate.Price;
				colorCapacity.ColorId = colorCapacityUpdate.ColorId;
				colorCapacity.CapacityId = colorCapacityUpdate.CapacityId;
				colorCapacity.Status = colorCapacityUpdate.Status;
				colorCapacity.Image = colorCapacityUpdate.Image;


				_context.ColorCapacities.Update(colorCapacity);
				await _context.SaveChangesAsync();
				Console.WriteLine(colorCapacity);
				return colorCapacity;
			}
			else
			{
				throw new Exception("Không tìm thấy phân loại sản phẩm");
			}
		}

		public async Task<bool> DeleteCC(int id)
		{
			var colorCapacity = await _context.ColorCapacities.FindAsync(id);
			if (colorCapacity != null)
			{
				_context.Remove(colorCapacity);
				await _context.SaveChangesAsync();
				return true;
			}
			else return false;
		}

		public async Task<ColorCapacity> GetProductCC(int id)
		{
			var colorCapacikty = await _context.ColorCapacities.FindAsync(id);
			if (colorCapacikty != null)
			{
				return colorCapacikty;
			}
			else throw new Exception("Không tìm thấy phân loại sản phẩm");
		}
	}
}
