using System.Collections;
using System.Collections.Generic;

namespace QueR.Domain.Entities
{
    public class Site : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual Company Company { get; set; }
        public int CompanyId { get; set; }
        public virtual ApplicationUser Manager { get; set; }
        public int? ManagerId { get; set; }

        public virtual ICollection<Queue> Queues { get; set; }
        public virtual ICollection<ApplicationUser> Employees { get; set; }

    }
}