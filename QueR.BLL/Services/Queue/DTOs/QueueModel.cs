using FluentValidation;
using Newtonsoft.Json;

namespace QueR.BLL.Services.Queue.DTOs
{
    public class QueueModel
    {
        public int TypeId { get; set; } 
        public int NextNumber { get; set; }

        [JsonIgnore]
        public int SiteId { get; set; }
    }

    public class QueueValidator : AbstractValidator<QueueModel>
    {
        public QueueValidator()
        {
            RuleFor(u => u.TypeId).NotEmpty().WithMessage("Queue type Id must not be 0.");
            RuleFor(u => u.NextNumber).InclusiveBetween(1, 100000).WithMessage("Starting number must be between 1 and 100 000");
        }
    }
}