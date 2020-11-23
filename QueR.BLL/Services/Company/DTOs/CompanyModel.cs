using FluentValidation;
using Newtonsoft.Json;

namespace QueR.BLL.Services.Company.DTOs
{
    public class CompanyModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class CompanyValidator : AbstractValidator<CompanyModel>
    {
        public CompanyValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Company name must not be empty.");
            RuleFor(c => c.Name).MaximumLength(50).WithMessage("Company name ust not be longer than 50 charachters.");
            RuleFor(c => c.Address).NotEmpty().WithMessage("Company address must not be empty.");
            RuleFor(c => c.Address).MinimumLength(5).MaximumLength(75).WithMessage("Company address must be between 5 and 75 characters.");
        }
    }
}
