using FluentValidation;
using Newtonsoft.Json;

namespace QueR.BLL.Services.QueueType.DTOs
{
    public class QueueTypeModel
    {
        public string Name { get; set; }
    }

    public class QueueTypeValidator : AbstractValidator<QueueTypeModel>
    {
        public QueueTypeValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("Site name must not be empty.");
            RuleFor(u => u.Name).MaximumLength(50).WithMessage("Site name must not be longer than 50 characters.");
        }
    }
}