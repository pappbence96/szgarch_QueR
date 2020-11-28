using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueR.BLL.Services.Site.DTOs
{
    public class UserSiteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class UserSiteDtoProfile : Profile
    {
        public UserSiteDtoProfile()
        {
            CreateMap<Domain.Entities.Site, UserSiteDto>();
        }
    }
}
