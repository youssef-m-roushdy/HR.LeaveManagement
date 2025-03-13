using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteLeaveRequestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {

            var leaveRequest = await _unitOfWork.LeaveRequestRepository.Get(request.Id);
            if(leaveRequest == null)
                throw new NotFoundException(nameof(LeaveRequest), request.Id);
            await _unitOfWork.LeaveRequestRepository.Delete(leaveRequest);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}