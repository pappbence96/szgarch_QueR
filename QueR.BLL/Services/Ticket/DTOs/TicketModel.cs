using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueR.BLL.Services.Ticket.DTOs
{
    public class TicketModel
    {
        public int queueId { get; set; }

    }

    public class TicketValidator : AbstractValidator<TicketModel>
    {
        public TicketValidator()
        {
            RuleFor(t => t.queueId).NotEmpty().WithMessage("Queue Id must not be empty.");
        }
    }
}
