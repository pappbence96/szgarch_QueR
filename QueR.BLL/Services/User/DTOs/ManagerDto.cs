using AutoMapper;
using QueR.Domain;
using QueR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueR.BLL.Services.User.DTOs
{
    public class ManagerDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string Company { get; set; }
        public int? CompanyId { get; set; }
        public string ManagedWorksite { get; set; }
        public int? ManagedWorksiteId { get; set; }
    }

    public class ManagerDtoProfile : Profile
    {
        public ManagerDtoProfile()
        {
            CreateMap<ApplicationUser, ManagerDto>()
                .ForMember(dest => dest.ManagedWorksite, opt => opt.MapFrom(src => src.ManagedSite != null ? src.ManagedSite.Name : "-"))
                .ForMember(dest => dest.ManagedWorksiteId, opt => opt.MapFrom(src => src.ManagedSite != null ? src.ManagedSite.Id : (int?)null))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company != null ? src.Company.Name : "-"));


        }
    }

}
