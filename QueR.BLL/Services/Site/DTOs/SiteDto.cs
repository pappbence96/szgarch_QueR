using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueR.BLL.Services.Site.DTOs
{
    public class SiteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ManagerName { get; set; }
        public int NumberOfEmployees { get; set; }
    }

    public class SiteDtoProfile : Profile
    {
        public SiteDtoProfile()
        {
            CreateMap<Domain.Entities.Site, SiteDto>()
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Manager != null ? src.Manager.UserName : "-"))
                .ForMember(dest => dest.NumberOfEmployees, opt => opt.MapFrom(src => src.Employees.Count));
        }
    }
}
