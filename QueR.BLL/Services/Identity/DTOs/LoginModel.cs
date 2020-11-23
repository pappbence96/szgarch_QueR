using FluentValidation;
using Newtonsoft.Json;

namespace QueR.BLL.Services.Identity
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(m => m.Username).NotEmpty().WithMessage("Username must not be empty.");
            RuleFor(m => m.Password).NotEmpty().WithMessage("Password must not be empty.");
        }
    }
}