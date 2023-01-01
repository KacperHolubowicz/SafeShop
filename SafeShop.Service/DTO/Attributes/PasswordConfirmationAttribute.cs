using SafeShop.Service.DTO.User;
using System.ComponentModel.DataAnnotations;

namespace SafeShop.Application.ViewModels.Attributes
{
    public class PasswordConfirmationAttribute : ValidationAttribute
    {
        public string GetErrorMessage()
        {
            return "Podane hasła nie zgadzają się";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string passwordConfirmation = (string)value;
            UserPostDTO registerData = (UserPostDTO)validationContext.ObjectInstance;
            var validation = passwordConfirmation == registerData.Password
                ? ValidationResult.Success : new ValidationResult(GetErrorMessage());

            return validation;
        }
    }
}
