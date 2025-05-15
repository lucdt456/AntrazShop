namespace AntrazShop.Helper
{
	public class Paginate
	{
		public int TotalItems { get; set; }
		public int PageSize { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPage { get; set; }
		public int StartPage { get; set; }
		public int MaxPagesToShow { get; set; }
		public int EndPage { get; set; }

		public Paginate()
		{

		}

		public Paginate(int totalItems, int currentPage, int pageSize)
		{
			if (currentPage < 1)
			{
				currentPage = 1;
			}

			

			TotalItems = totalItems;
			PageSize = pageSize;
			

			TotalPage = (int)Math.Ceiling((double)totalItems / pageSize);
			if (currentPage > TotalPage)
			{
				currentPage = TotalPage;
			}

			CurrentPage = currentPage;
			MaxPagesToShow = 4;
			int halfPagesToShow = MaxPagesToShow / 2;
			StartPage = currentPage - halfPagesToShow;
			EndPage = currentPage + halfPagesToShow;

			if (StartPage < 1)
			{
				StartPage = 1;
				EndPage = StartPage + MaxPagesToShow - 1;

				if (EndPage > TotalPage)
				{
					EndPage = TotalPage;
				}
			}

			if (EndPage > TotalPage)
			{
				EndPage = TotalPage;
				StartPage = EndPage - MaxPagesToShow + 1;
				if (StartPage < 1)
				{
					StartPage = 1;
				}
			}

		}
	}
}
