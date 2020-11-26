using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueR.BLL.Services.QueueType.DTOs
{
    public class QueueTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfQueues { get; set; }
    }

    public class QueueTypeDtoProfile : Profile
    {
        public QueueTypeDtoProfile()
        {
            CreateMap<Domain.Entities.QueueType, QueueTypeDto>()
                .ForMember(dest => dest.NumOfQueues, opt => opt.MapFrom(src => src.Queues != null ? src.Queues.Count : 0));
        }
    }
}
