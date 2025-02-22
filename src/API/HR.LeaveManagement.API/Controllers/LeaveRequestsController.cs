using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HR.LeaveManagement.API.Controllers
{
    [Route("[controller]")]
    public class LeaveRequestsController : Controller
    {
        private readonly IMediator _mediator;
        public LeaveRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var leaveRequests = await _mediator.Send(new GetLeaveRequestListRequest());
            return Ok(leaveRequests);
        }

        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var leaveRequests = await _mediator.Send(new GetLeaveRequestDetailRequest{Id = id});
            return Ok(leaveRequests);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateLeaveRequestDto leaveRequest)
        {
            var command = new CreateLeaveRequestCommand{ LeaveRequestDto = leaveRequest};
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody]UpdateLeaveRequestDto leaveRequest)
        {
            var command = new UpdateLeaveRequestCommand{ LeaveRequestDto = leaveRequest};
            var response = await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("changeapproval/{id:int}")]
        public async Task<IActionResult> ChangeApproval([FromRoute]int id, [FromBody]ChangeLeaveRequestApprovalDto changeLeaveRequestApproval)
        {
            var command = new UpdateLeaveRequestCommand{ Id = id ,ChangeLeaveRequestApprovalDto = changeLeaveRequestApproval};
            var response = await _mediator.Send(command);
            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var response = await _mediator.Send(new DeleteLeaveRequestCommand{ Id = id});
            return NoContent();
        }
    }
}