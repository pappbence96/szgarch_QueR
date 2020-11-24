using FluentValidation;
using Newtonsoft.Json;

namespace QueR.BLL.Services.Site.DTOs
{
    public class SiteModel
    {
        public string Name { get; set; }
        public string Address { get; set; }

        [JsonIgnore]
        public int CompanyId { get; set; }
    }

    public class SiteValidator : AbstractValidator<SiteModel>
    {
        public SiteValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("Site name must not be empty.");
            RuleFor(u => u.Name).MaximumLength(50).WithMessage("Site name must not be longer than 50 characters.");
            RuleFor(u => u.Address).NotEmpty().WithMessage("Address must not be empty.");
            RuleFor(u => u.Address).MinimumLength(5).MaximumLength(75).WithMessage("Address must be between 5 and 75 characters.");
        }
    }
}