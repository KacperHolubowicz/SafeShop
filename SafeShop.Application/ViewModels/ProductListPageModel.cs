namespace SafeShop.Application.ViewModels
{
    public class ProductListPageModel
    {
            public IEnumerable<ProductListViewModel> PaginatedProperty { get; set; }
            public bool HasPreviousPage { get; set; }
            public bool HasNextPage { get; set; }
    }
}
