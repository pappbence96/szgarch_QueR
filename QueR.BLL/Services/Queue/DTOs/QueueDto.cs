using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueR.BLL.Services.Queue.DTOs
{
    public class QueueDto
    {
        public string QueueType { get; set; }
        public int NextNumber { get; set; }
        public string Worksite { get; set; }
        public int NumOfAssignedEmployees { get; set; }
        public int NumOfTickets { get; set; }
    }

    public class QueueDtoProfile : Profile
    {
        public QueueDtoProfile()
        {
            CreateMap<Domain.Entities.Queue, QueueDto>()
                .ForMember(dest => dest.QueueType, opt => opt.MapFrom(src => src.Type != null ? src.Type.Name : "-"))
                .ForMember(dest => dest.Worksite, opt => opt.MapFrom(src => src.Site != null ? src.Site.Name : "-"))
                .ForMember(dest => dest.NumOfAssignedEmployees, opt => opt.MapFrom(src => src.AssignedEmployees.Count))
                .ForMember(dest => dest.NumOfTickets, opt => opt.MapFrom(src => src.Tickets.Count));
        }
    }
}
