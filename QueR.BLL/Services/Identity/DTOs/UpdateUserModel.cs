using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueR.BLL.Services.Identity.DTOs
{
    public class UpdateUserModel
    {
        public string Email { get; set; }
    }

    public class UpdateUserValidator : AbstractValidator<UpdateUserModel>
    {
        public UpdateUserValidator()
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email must not be empty.");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Email must be a valid email address.");
        }
    }
}
