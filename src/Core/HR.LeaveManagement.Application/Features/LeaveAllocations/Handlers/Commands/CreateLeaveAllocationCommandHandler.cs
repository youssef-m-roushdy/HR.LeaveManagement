using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Reponses;
using HR.LeaveManagement.Domain;
using MediatR;
using HR.LeaveManagement.Application.Contracts.Identity;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, BaseCommandResponse>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CreateLeaveAllocationCommandHandler(
            ILeaveAllocationRepository leaveAllocationRepository,
            IMapper mapper,
            ILeaveTypeRepository leaveTypeRepository,
            IUserService userService
            )
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _userService = userService;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveAllocationDtoValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var leaveType = await _leaveTypeRepository.Get(request.LeaveAllocationDto.LeaveTypeId);
                var employees = await _userService.GetEmployees();
                var period = DateTime.Now.Year;
                var allocations = new List<LeaveAllocation>();
                foreach (var emp in employees)
                {
                    if(await _leaveAllocationRepository.AllocationExists(emp.Id, leaveType.Id, period))
                        continue;
                    allocations.Add(new LeaveAllocation
                        {
                            EmployeeId = emp.Id,
                            LeaveTypeId = leaveType.Id,
                            NumberOfDays = leaveType.DefaultDays,
                            Period = period
                        }
                    );
                }

                await _leaveAllocationRepository.AddAllocations(allocations);

                response.Success = true;
                response.Message = "Allocations successful";
            }


            return response;
        }
    }
}