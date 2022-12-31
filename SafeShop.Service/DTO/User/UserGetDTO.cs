using SafeShop.Service.DTO.Cart;

namespace SafeShop.Service.DTO.User
{
    public class UserGetDTO
    {
        public Guid ID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CartGetDTO Cart { get; set; }
        public string Role { get; set; }
    }
}
