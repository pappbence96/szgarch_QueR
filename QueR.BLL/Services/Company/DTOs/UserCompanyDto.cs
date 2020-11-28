using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueR.BLL.Services.Company.DTOs
{
    public class UserCompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class UserCompanyDtoProfile : Profile
    {
        public UserCompanyDtoProfile()
        {
            CreateMap<Domain.Entities.Company, UserCompanyDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.MailingAddress));
        }
    }
}
