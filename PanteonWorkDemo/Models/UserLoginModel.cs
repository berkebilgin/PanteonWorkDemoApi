using FluentValidation;

namespace PanteonWorkDemo.Models
{
    public class UserLoginModel
    {
        public string username { get; set; }

        public string password { get; set; }
    }

    public class LoginValidator : AbstractValidator<UserLoginModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty()
                .WithMessage("Kullanıcı adı alanı zorunludur");

            RuleFor(x => x.password)
                .NotEmpty()
                .WithMessage("Parola alanı zorunludur");

        }
    }
}

