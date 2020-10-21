using QueR.Domain.Entities;
using System;

namespace QueR.Domain.Entities
{
    public class Ticket : BaseEntity
    {
        public int Number { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public int OwnerId { get; set; }
        public virtual ApplicationUser Handler { get; set; }
        public int HandlerId { get; set; }
        public DateTime Created { get; set; }
        public bool Called { get; set; }
        public virtual Queue Queue { get; set; }
        public int QueueId { get; set; }
    }
}