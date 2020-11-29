using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueR.BLL.Services.Ticket.DTOs
{
    public class UserTicketDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string VisibleNumber { get; set; }
        public string Queue { get; set; }
        public int? QueueId { get; set; }
        public string Worksite { get; set; }
        public int? WorksiteId { get; set; }
        public string Company { get; set; }
        public int? CompanyId { get; set; }
        public int NumOfTicketsBeforeThis { get; set; }
    }

    public class UserTicketDtoProfile : Profile
    {
        public UserTicketDtoProfile()
        {
            CreateMap<Domain.Entities.Ticket, UserTicketDto>()
                .ForMember(dest => dest.Queue, opt => opt.MapFrom(src => src.Queue != null ? src.Queue.Type.Name : "-"))
                .ForMember(dest => dest.QueueId, opt => opt.MapFrom(src => src.Queue != null ? src.Queue.Id : (int?)null))
                .ForMember(dest => dest.Worksite, opt => opt.MapFrom(src => src.Queue.Site != null ? src.Queue.Site.Name : "-"))
                .ForMember(dest => dest.WorksiteId, opt => opt.MapFrom(src => src.Queue.Site != null ? src.Queue.Site.Id : (int?)null))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Queue.Site.Company != null ? src.Queue.Site.Company.Name : "-"))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Queue.Site.Company != null ? src.Queue.Site.Company.Id : (int?)null))
                .ForMember(dest => dest.NumOfTicketsBeforeThis, opt => opt.MapFrom(src => src.Queue.Tickets.Count(t => t.Number < src.Number && !t.Called)))
                .ForMember(dest => dest.VisibleNumber, opt => opt.MapFrom(src => src.Queue.Prefix + src.Number));
        }
    }
}
