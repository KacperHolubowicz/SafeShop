using SafeShop.Application.ViewModels;

namespace SafeShop.Application.Requests.Auth
{
    public class RegisterRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public RegisterRequest(RegisterFormViewModel register)
        {
            Login = register.Login;
            Password = register.Password;
            Email = register.Email;
            FirstName = register.FirstName;
            LastName = register.LastName;
            RepeatPassword = register.RepeatPassword;
        }
    }
}
