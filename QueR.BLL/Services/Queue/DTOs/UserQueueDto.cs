using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueR.BLL.Services.Queue.DTOs
{
    public class UserQueueDto
    {
        public int Id { get; set; }
        public string QueueType { get; set; }
        public int NumOfActiveTickets { get; set; }
    }

    public class UserQueueDtoProfile : Profile
    {
        public UserQueueDtoProfile()
        {
            CreateMap<Domain.Entities.Queue, UserQueueDto>()
                .ForMember(dest => dest.QueueType, opt => opt.MapFrom(src => src.Type != null ? src.Type.Name : "-"))
                .ForMember(dest => dest.NumOfActiveTickets, opt => opt.MapFrom(src => src.Tickets.Where(t => !t.Called).Count()));
        }
    }
}
