using AutoMapper;
using QueR.Domain;
using QueR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueR.BLL.Services.User.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string Company { get; set; }
        public int? CompanyId { get; set; }
        public string Worksite { get; set; }
        public int? WorksiteId { get; set; }
        public string AssignedQueue { get; set; }
        public int? AssignedQueueId { get; set; }
    }

    public class EmployeeDtoProfile : Profile
    {
        public EmployeeDtoProfile()
        {
            CreateMap<ApplicationUser, EmployeeDto>()
                .ForMember(dest => dest.AssignedQueue, opt => opt.MapFrom(src => src.AssignedQueue != null ? src.AssignedQueue.Type.Name : "-"))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company != null ? src.Company.Name : "-"))
                .ForMember(dest => dest.Worksite, opt => opt.MapFrom(src => src.Worksite != null ? src.Worksite.Name : "-"));
        }
    }
}
