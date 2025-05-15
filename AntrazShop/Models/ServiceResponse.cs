namespace AntrazShop.Models
{
	public class ServiceResponse<T>
	{
		public bool IsSuccess { get; set; } = true;
		public T? Data { get; set; }
		public List<string> Errors { get; set; } = new();
	}
}
	