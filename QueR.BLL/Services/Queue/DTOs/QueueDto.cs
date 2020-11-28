using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueR.BLL.Services.Queue.DTOs
{
    public class QueueDto
    {
        public int Id { get; set; }
        public string QueueType { get; set; }
        public int QueueTypeId { get; set; }
        public int NextNumber { get; set; }
        public string Worksite { get; set; }
        public string Prefix { get; set; }
        public int Step { get; set; }
        public int WorksiteId { get; set; }
        public int NumOfAssignedEmployees { get; set; }
        public int NumOfActiveTickets { get; set; }
        public int MaxActiveTicketsPerUser { get; set; }
    }

    public class QueueDtoProfile : Profile
    {
        public QueueDtoProfile()
        {
            CreateMap<Domain.Entities.Queue, QueueDto>()
                .ForMember(dest => dest.QueueType, opt => opt.MapFrom(src => src.Type != null ? src.Type.Name : "-"))
                .ForMember(dest => dest.QueueTypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.Worksite, opt => opt.MapFrom(src => src.Site != null ? src.Site.Name : "-"))
                .ForMember(dest => dest.WorksiteId, opt => opt.MapFrom(src => src.SiteId))
                .ForMember(dest => dest.NumOfAssignedEmployees, opt => opt.MapFrom(src => src.AssignedEmployees.Count))
                .ForMember(dest => dest.NumOfActiveTickets, opt => opt.MapFrom(src => src.Tickets.Where(t => !t.Called).ToList().Count));
        }
    }
}
