using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.DTOs;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LeaveRequest, LeaveRequestDto>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestListDto>()
            .ForMember(dest => dest.DateRequested, opt => opt.MapFrom(src => src.DateCreated))
            .ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationDto>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
            CreateMap<CreateLeaveTypeDto, LeaveType>().ReverseMap();
            CreateMap<CreateLeaveRequestDto, LeaveRequest>().ReverseMap();
            CreateMap<UpdateLeaveRequestDto, LeaveRequest>().ReverseMap();
            CreateMap<CreateLeaveAllocationDto, LeaveAllocation>().ReverseMap();
            CreateMap<UpdateLeaveAllocationDto, LeaveAllocation>().ReverseMap();

        }
    }
}