namespace Filmat.Models
{
	public class Pager
	{
		public int TotalItems { get; set; } // sa recorde jon n'total
		public int CurrentPage { get; set; } //faqja aktuale
		public int PageSize { get; set; } //sa rekorde munet mi majt 1 faqe
		public int TotalPages { get; set; } //sa faqe kan mu kon
		public int StartPage { get; set; } //
		public int EndPage { get; set; } //


		public Pager()
		{

		}

		public Pager(int totalItems, int page, int pageSize = 10)
		{
			int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
			int currentPage = page;

			int startPage = currentPage - 5;
			int endPage = currentPage + 4;

			if(startPage <= 0)
			{
				endPage = endPage - (startPage - 1);
				startPage = 1;
			}

			if(endPage > totalPages)
			{
				endPage = totalPages;
				if(endPage > 10)
				{
					startPage = endPage - 9;
				}
			}

			TotalItems = totalItems;
			CurrentPage = currentPage;
			PageSize = pageSize;
			TotalPages = totalPages;
			StartPage = startPage;
			EndPage = endPage;

		}


	}


}
