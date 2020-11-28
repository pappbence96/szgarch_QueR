using AutoMapper;
using QueR.Domain;
using QueR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueR.BLL.Services.User.DTOs
{
    public class AdministratorDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string AdministratedCompany { get; set; }
        public int? AdministratedCompanyId { get; set; }
    }

    public class AdministratorDtoProfile : Profile
    {
        public AdministratorDtoProfile()
        {
            CreateMap<ApplicationUser, AdministratorDto>()
                .ForMember(dest => dest.AdministratedCompany, opt => opt.MapFrom(src => src.AdministratedCompany != null ? src.AdministratedCompany.Name : "-"))
                .ForMember(dest => dest.AdministratedCompanyId, opt => opt.MapFrom(src => src.AdministratedCompany != null ? src.AdministratedCompany.Id : (int?)null));
        }
    }
}
