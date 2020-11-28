using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueR.BLL.Services.Ticket.DTOs
{
    public class CompanyTicketDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string VisibleNumber { get; set; }
        public int OwnerId { get; set; }
        public string OwnerUserName { get; set; }
        public DateTime Created { get; set; }

    }

    public class CompanyTicketDtoProfile : Profile
    {
        public CompanyTicketDtoProfile()
        {
            CreateMap<Domain.Entities.Ticket, CompanyTicketDto>()
                .ForMember(dest => dest.OwnerUserName, opt => opt.MapFrom(src => src.Owner != null ? src.Owner.UserName : "-"))
                .ForMember(dest => dest.VisibleNumber, opt => opt.MapFrom(src => src.Queue.Prefix + src.Number));
        }
    }


}
