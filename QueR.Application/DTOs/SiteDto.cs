using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueR.Application.DTOs
{
    public class SiteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ManagerName { get; set; }
        public int NumberOfEmployees { get; set; }
    }
}
