using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueR.BLL.Services.Identity.DTOs
{
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterValidator : AbstractValidator<RegisterModel>
    {
        public RegisterValidator()
        {
            RuleFor(u => u.UserName).NotEmpty().WithMessage("Username must not be empty.");
            RuleFor(u => u.UserName).MaximumLength(30).WithMessage("Username must not be longer than 30 characters");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email must not be empty.");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Email must be a valid email address.");
        }
    }

    public class RegisterPasswordValidator : AbstractValidator<RegisterModel>
    {
        public RegisterPasswordValidator()
        {
            RuleFor(u => u.Password).NotEmpty().WithMessage("Password name must not be empty.");
        }
    }
}
