using System;
using System.Collections.Generic;
using System.Text;

namespace QueR.Domain.Entities
{
    public class Queue : BaseEntity
    {
        public virtual QueueType Type { get; set; }
        public int TypeId { get; set; }
        public virtual Site Site { get; set; }
        public int? SiteId { get; set; }
        public int NextNumber { get; set; }

        public virtual ICollection<ApplicationUser> AssignedEmployees { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
