namespace SafeShop.Repository.Filters
{
    public class ProductPagingFilter
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public ProductPagingFilter(int currentPage, int pageSize)
        {
            CurrentPage = currentPage > 0 ? currentPage : 1;
            PageSize = pageSize > 0 ? pageSize : 10;
        }
    }
}
