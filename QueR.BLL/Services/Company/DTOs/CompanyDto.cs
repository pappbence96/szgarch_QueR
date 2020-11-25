using AutoMapper;
using QueR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueR.BLL.Services.Company.DTOs
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string AdminName { get; set; }
        public int? AdminId { get; set; }
        public int NumberOfSites { get; set; }
        public int NumberOfEmployees { get; internal set; }
    }

    public class CompanyDtoProfile : Profile
    {
        public CompanyDtoProfile()
        {
            CreateMap<Domain.Entities.Company, CompanyDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.MailingAddress))
                .ForMember(dest => dest.AdminName, opt => opt.MapFrom(src => src.Administrator != null ? src.Administrator.UserName : "-"))
                .ForMember(dest => dest.AdminId, opt => opt.MapFrom(src => src.AdministratorId))
                .ForMember(dest => dest.NumberOfSites, opt => opt.MapFrom(src => src.Sites.Count))
                .ForMember(dest => dest.NumberOfEmployees, opt => opt.MapFrom(src => src.Users.Count));
        }
    }
}
