using FluentValidation;
using Newtonsoft.Json;

namespace QueR.BLL.Services.Company
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
            RuleFor(c => c.Address).NotEmpty().WithMessage("Company address must not be empty.");
        }
    }
}
