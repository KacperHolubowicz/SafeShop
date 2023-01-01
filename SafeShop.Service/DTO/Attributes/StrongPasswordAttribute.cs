using System.ComponentModel.DataAnnotations;

namespace SafeShop.Application.ViewModels.Attributes
{
    public class StrongPasswordAttribute : ValidationAttribute
    {
        public StrongPasswordAttribute(int minLength, int maxLength)
        {
            MinLength = minLength;
            MaxLength = maxLength;
        }

        public int MinLength { get; }
        public int MaxLength { get; }

        public string GetErrorMessage()
        {
            return $"Twoje hasło nie jest prawidłowe, musi zawierać od {MinLength} do " +
                $"{MaxLength} znaków, dodatkowo musi mieć co najmniej jedną wielką literę, " +
                $"cyfrę oraz znak specjalny";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string providedPassword = (string)value;
            int passwordLength = providedPassword.Length;
            if (passwordLength >= MinLength && passwordLength <= MaxLength &&
                providedPassword.Any(char.IsUpper) && providedPassword.Any(char.IsDigit)
                && providedPassword.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(GetErrorMessage());
            }
        }
    }
}
