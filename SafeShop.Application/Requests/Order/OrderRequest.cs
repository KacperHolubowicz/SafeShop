using SafeShop.Application.ViewModels;

namespace SafeShop.Application.Requests.Order
{
    public class OrderRequest
    {
        public OrderRequest(Guid cartId, OrderDataViewModel orderVM)
        {
            CartID = cartId;
            Details = new OrderDetailsRequest()
            {
                Billing = new BillingRequest()
                {
                    FirstName = orderVM.FirstName,
                    LastName = orderVM.LastName,
                    Email = orderVM.Email,
                    PhoneNumber = orderVM.PhoneNumber
                },
                Shipping = new ShippingRequest()
                {
                    FirstName = orderVM.FirstName,
                    LastName = orderVM.LastName,
                    Street = orderVM.Street,
                    Town = orderVM.Town,
                    ZipCode = orderVM.ZipCode
                }
            };
        }

        public Guid CartID { get; set; }
        public OrderDetailsRequest Details { get; set; }
    }
}
