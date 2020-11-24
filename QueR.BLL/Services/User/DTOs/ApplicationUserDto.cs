using AutoMapper;
using QueR.Domain;
using QueR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueR.BLL.Services.User.DTOs
{
    public class ApplicationUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
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

    public class ApplicationUserDtoProfile : Profile
    {
        public ApplicationUserDtoProfile()
        {
            CreateMap<Domain.Entities.ApplicationUser, ApplicationUserDto>()
                .ForMember(dest => dest.AssignedQueue, opt => opt.MapFrom(src => src.AssignedQueue != null ? src.AssignedQueue.Type.Name : "-"))
                .ForMember(dest => dest.AdministratedCompany, opt => opt.MapFrom(src => src.AdministratedCompany != null ? src.AdministratedCompany.Name : "-"))
                .ForMember(dest => dest.ManagedWorksite, opt => opt.MapFrom(src => src.ManagedSite != null ? src.ManagedSite.Name : "-"))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company != null ? src.Company.Name : "-"));
        }
    }
}
