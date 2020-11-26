using FluentValidation;
using Newtonsoft.Json;
using QueR.Domain;

namespace QueR.BLL.Services.User.DTOs
{
    public class CreateUserModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }


    }

    public class UserValidator : AbstractValidator<CreateUserModel>
    {
        public UserValidator()
        {
            RuleFor(u => u.UserName).NotEmpty().WithMessage("Username must not be empty.");
            RuleFor(u => u.UserName).MaximumLength(30).WithMessage("Username must not be longer than 30 characters");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email must not be empty.");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Email must be a valid email address.");
        }
    }

    public class WorkerValidator : AbstractValidator<CreateUserModel>
    {
        public WorkerValidator()
        {
            Include(new UserValidator());
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("First name must not be empty.");
            RuleFor(u => u.FirstName).MaximumLength(30).WithMessage("First name must not be longer than 30 characters");
            RuleFor(u => u.LastName).NotEmpty().WithMessage("Last name must not be empty.");
            RuleFor(u => u.LastName).MaximumLength(30).WithMessage("Last name must not be longer than 30 characters");
            RuleFor(u => u.Address).NotEmpty().WithMessage("Address must not be empty.");
            RuleFor(u => u.Address).MinimumLength(5).MaximumLength(75).WithMessage("Address must be between 5 and 75 characters");
        }
    }

    public class PasswordValidator : AbstractValidator<CreateUserModel>
    {
        public PasswordValidator()
        {
            RuleFor(u => u.Password).NotEmpty().WithMessage("Password name must not be empty.");
        }
    }
}