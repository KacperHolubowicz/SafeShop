using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Repository.Filters
{
    public class PagingWrapper<T> where T : class
    {
        public T PaginatedProperty { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }

        public PagingWrapper(T paginatedProperty, bool hasPrevious, bool hasNext)
        {
            PaginatedProperty = paginatedProperty;
            HasPreviousPage = hasPrevious;
            HasNextPage = hasNext;
        }
    }
}
