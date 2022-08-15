namespace SafeShop.Core.Model
{
    public class User
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string Login { get; set; } = string.Empty;
        public byte[] Password { get; set; } = default!;
        public byte[] Salt { get; set; } = default!;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

    }
}
