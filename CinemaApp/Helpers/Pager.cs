namespace CinemaApp.Helpers
{
    public class Pager
    {
        public int TotalItens { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }



        public int TotalPages { get; set; }
        public int StartPages { get; set; }
        public int EndPages { get; set; }

        public Pager()
        {
            
        }

        public Pager(int totalItems, int currentPage, int pageSize = 10)
        {
            TotalItens = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;

            // Calculate the total pages
            TotalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);

            // Calculate the start and end page numbers
            int startPage = currentPage - 5;
            int endPage = currentPage + 4;

            // Ensure startPage and endPage are within bounds
            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }

            if (endPage > TotalPages)
            {
                endPage = TotalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            StartPages = startPage;
            EndPages = endPage;
        }

    }
}
