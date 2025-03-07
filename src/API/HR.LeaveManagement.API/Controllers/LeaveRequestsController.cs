using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using HR.LeaveManagement.Application.Reponses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LeaveRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestDto>>> Get()
        {
            var leaveRequests = await _mediator.Send(new GetLeaveRequestListRequest());
            return Ok(leaveRequests);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<LeaveRequestDto>> GetById([FromRoute] int id)
        {
            var leaveRequests = await _mediator.Send(new GetLeaveRequestDetailRequest { Id = id });
            return Ok(leaveRequests);
        }


        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveRequestDto leaveRequest)
        {
            var command = new CreateLeaveRequestCommand { LeaveRequestDto = leaveRequest };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateLeaveRequestDto leaveRequest)
        {
            if (leaveRequest == null)
            {
                return BadRequest("Leave request data cannot be null.");
            }

            var command = new UpdateLeaveRequestCommand { Id = id, LeaveRequestDto = leaveRequest };
            var response = await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("changeapproval/{id:int}")]
        public async Task<ActionResult> ChangeApproval([FromRoute] int id, [FromBody] ChangeLeaveRequestApprovalDto changeLeaveRequestApproval)
        {
            if (changeLeaveRequestApproval == null)
            {
                return BadRequest("Approval data cannot be null.");
            }

            var command = new UpdateLeaveRequestCommand { Id = id, ChangeLeaveRequestApprovalDto = changeLeaveRequestApproval };
            var response = await _mediator.Send(command);
            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _mediator.Send(new DeleteLeaveRequestCommand { Id = id });
            return NoContent();
        }
    }
}