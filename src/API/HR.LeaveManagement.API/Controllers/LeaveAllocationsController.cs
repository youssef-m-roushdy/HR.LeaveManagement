using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
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
    public class LeaveAllocationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LeaveAllocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDto>>> Get(bool isLoggedInUser = false)
        {
            var leaveAllocations = await _mediator.Send(new GetLeaveAllocationListRequest() {IsLoggedInUser = isLoggedInUser});
            return Ok(leaveAllocations);
        }

        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<LeaveAllocationDto>> GetById([FromRoute]int id)
        {
            var leaveAllocations = await _mediator.Send(new GetLeaveAllocationDetailRequest{Id = id});
            return Ok(leaveAllocations);
        }


        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody]CreateLeaveAllocationDto leaveAllocation)
        {
            var command = new CreateLeaveAllocationCommand{ LeaveAllocationDto = leaveAllocation};
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update([FromRoute]int id, [FromBody]UpdateLeaveAllocationDto leaveAllocation)
        {
            var command = new UpdateLeaveAllocationCommand{ LeaveAllocationDto = leaveAllocation};
            var response = await _mediator.Send(command);
            return NoContent();
        }
  
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute]int id)
        {
            await _mediator.Send(new DeleteLeaveAllocationCommand{ Id = id});
            return NoContent();
        }
    }
}