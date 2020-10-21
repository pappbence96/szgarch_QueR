using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueR.Domain.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }

        public virtual Company AdministratedCompany { get; set; }
        public int? AdministratedCompanyId { get; set; }
        public virtual Site ManagedSite { get; set; }
        public int? ManagedSiteId { get; set; }
        public virtual Site Worksite { get; set; }
        public int? WorksiteId { get; set; }
        public virtual Queue AssignedQueue { get; set; }
        public int? AssignedQueueId { get; set; }
    }
}
