using FluentValidation;
using Newtonsoft.Json;
using QueR.Domain;

namespace QueR.BLL.Services.User
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public int? CompanyId { get; set; }
        public Gender Gender { get; set; }
        
        [JsonIgnore]
        public bool IsValidWorker {
            get =>
                IsValidUser &&
                !string.IsNullOrWhiteSpace(FirstName) &&
                !string.IsNullOrWhiteSpace(LastName) &&
                !string.IsNullOrWhiteSpace(Address) &&
                !string.IsNullOrWhiteSpace(Gender.ToString());
        }

        [JsonIgnore]
        public bool IsValidUser
        {
            get =>
                !string.IsNullOrWhiteSpace(UserName) &&
                !string.IsNullOrWhiteSpace(Email);
        }

        [JsonIgnore]
        public bool IsValidPassword
        {
            get =>
                !string.IsNullOrWhiteSpace(Password);
        }
    }

    public class UserValidator: AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            RuleFor(u => u.UserName).NotEmpty().WithMessage("Username must not be empty.");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email must not be empty.");
        }
    }

    public class WorkerValidator: AbstractValidator<UserModel>
    {
        public WorkerValidator()
        {
            Include(new UserValidator());
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("First name must not be empty.");
            RuleFor(u => u.LastName).NotEmpty().WithMessage("Last name must not be empty.");
            RuleFor(u => u.Address).NotEmpty().WithMessage("Address must not be empty.");
        }
    }

    public class PasswordValidator: AbstractValidator<UserModel>
    {
        public PasswordValidator()
        {
            RuleFor(u => u.Password).NotEmpty().WithMessage("Password name must not be empty.");
        }
    }
}