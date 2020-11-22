using QueR.Domain;
using QueR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueR.Application.DTOs
{
    public class ApplicationUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string AssignedQueue { get; set; }
        public string AdministratedCompany { get; set; }
        public string ManagedWorksite { get; set; }
        public string Company { get; set; }
    }
}
