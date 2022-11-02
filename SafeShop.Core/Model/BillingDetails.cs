namespace SafeShop.Core.Model
{
    public class BillingDetails
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public OrderDetails Order { get; set; }
        public Guid OrderID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}