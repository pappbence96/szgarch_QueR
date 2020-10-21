using System;
using System.Collections.Generic;
using System.Text;

namespace QueR.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string MailingAddress { get; set; }
        public virtual ApplicationUser Administrator { get; set; }
        public int? AdministratorId { get; set; }

        public virtual ICollection<QueueType> AvailableQueueTypes { get; set; }
        public virtual ICollection<Site> Sites { get; set; }

    }
}
