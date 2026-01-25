namespace CityInfo.Application.Common
{
    public class PaginationMetadata
    {
        #region [ Fields ]
        public int TotalItemCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; }
        #endregion

        #region [ Constructure ]
        public PaginationMetadata(int totalItemCount, int pageSize, int currentPage, int totalPages)
        {
            TotalItemCount = totalItemCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPages = totalPages;
        }
        #endregion
    }
}
