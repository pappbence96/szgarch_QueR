using FluentValidation;
using QueR.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueR.BLL.Services.User.DTOs
{
    public class UpdateWorkerModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public Gender? GenderEnum
        {
            get
            {
                if (Enum.TryParse(typeof(Gender), Gender, out object parsedGender))
                {
                    return (Gender)parsedGender;
                }
                return null;
            }
        }
    }

    public class UpdateWorkerValidator : AbstractValidator<UpdateWorkerModel>
    {
        public UpdateWorkerValidator()
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email must not be empty.");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Email must be a valid email address.");
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("First name must not be empty.");
            RuleFor(u => u.FirstName).MaximumLength(30).WithMessage("First name must not be longer than 30 characters");
            RuleFor(u => u.LastName).NotEmpty().WithMessage("Last name must not be empty.");
            RuleFor(u => u.LastName).MaximumLength(30).WithMessage("Last name must not be longer than 30 characters");
            RuleFor(u => u.Address).NotEmpty().WithMessage("Address must not be empty.");
            RuleFor(u => u.Address).MinimumLength(5).MaximumLength(75).WithMessage("Address must be between 5 and 75 characters");
            RuleFor(u => u.Gender).NotNull().IsEnumName(typeof(Gender), caseSensitive: false).WithMessage("Gender must not be \"Male\", \"Female\" or \"Other\"");
        }
    }
}
