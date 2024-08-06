using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace PanteonWorkDemo.Models
{
    public class UserRegisterModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordAgain { get; set; }

    }

    public class UserRegisterValidator : AbstractValidator<UserRegisterModel>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Kullanıcı adı alanı zorunludur");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("E-Posta alanı zorunludur");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Şifre alanı zorunludur");

            RuleFor(x => x.PasswordAgain)
                .NotEmpty()
                .WithMessage("Şifre tekrarı alanı zorunludur");

            RuleFor(x => x.PasswordAgain)
                .Equal(x => x.Password)
                .When(x =>
                    !string.IsNullOrEmpty(x.UserName) &&
                    !string.IsNullOrEmpty(x.Email) &&
                    !string.IsNullOrEmpty(x.Password) &&
                    !string.IsNullOrEmpty(x.PasswordAgain)
                )
                .WithMessage("Şifreler eşleşmiyor.");
        }
    }
}
