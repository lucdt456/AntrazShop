using AntrazShop.Data;

namespace AntrazShop.Interfaces.Repositories
{
	public interface IProductColorCapacityRepository
	{
		Task<int> AddColor(string ColorName);
		Task<int> AddCapacity(string CapacityValue);
		Task AddColorCapacity(ColorCapacity colorCapacity);
		Task<ColorCapacity> EditColorCapacity(int id, ColorCapacity colorCapacityUpdate);
		Task<bool> DeleteCC(int id);
		Task<ColorCapacity> GetProductCC(int id);
	}
}
