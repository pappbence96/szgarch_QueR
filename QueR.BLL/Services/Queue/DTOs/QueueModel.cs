using FluentValidation;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace QueR.BLL.Services.Queue.DTOs
{
    public class QueueModel
    {
        public int TypeId { get; set; } 
        public int NextNumber { get; set; }
        public string Prefix { get; set; }
        public int Step { get; set; }
    }

    public class CreateQueueValidator : AbstractValidator<QueueModel>
    {
        public CreateQueueValidator()
        {
            RuleFor(u => u.TypeId).NotEmpty().WithMessage("Queue type Id must not be 0.");
            RuleFor(u => u.NextNumber).InclusiveBetween(1, 100000).WithMessage("Starting number must be between 1 and 100 000");
            RuleFor(u => u.Prefix).MaximumLength(10).WithMessage("Ticket number prefixes cannot be longer than 10 characters.");
            RuleFor(u => u.Prefix).Matches(new Regex("[a-zA-Z0-9]*")).WithMessage("Ticket number prefix can only contain characters and digits.");
            RuleFor(u => u.Step).InclusiveBetween(1, 10).WithMessage("Ticket step must be positive and a maximum of 10.");
        }
    }

    public class UpdateQueueValidator : AbstractValidator<QueueModel>
    {
        public UpdateQueueValidator()
        {
            RuleFor(u => u.Prefix).MaximumLength(10).WithMessage("Ticket number prefixes cannot be longer than 10 characters.");
            RuleFor(u => u.Prefix).Matches(new Regex("[a-zA-Z0-9]*")).WithMessage("Ticket number prefix can only contain characters and digits.");
            RuleFor(u => u.Step).InclusiveBetween(1, 10).WithMessage("Ticket step must be positive and a maximum of 10.");
        }
    }
}