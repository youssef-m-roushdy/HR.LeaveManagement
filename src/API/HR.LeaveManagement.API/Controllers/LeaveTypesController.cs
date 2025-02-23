using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using HR.LeaveManagement.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    public class LeaveTypesController : Controller
    {
        private readonly IMediator _mediator;
        public LeaveTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var leaveTypes = await _mediator.Send(new GetLeaveTypeListRequest());
            return Ok(leaveTypes);
        }

        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var leaveTypes = await _mediator.Send(new GetLeaveTypeDetailRequest{Id = id});
            return Ok(leaveTypes);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateLeaveTypeDto leaveType)
        {
            var command = new CreateLeaveTypeCommand{ LeaveTypeDto = leaveType};
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Post([FromRoute]int id, [FromBody]LeaveTypeDto leaveType)
        {
            var command = new UpdateLeaveTypeCommand{ LeaveTypeDto = leaveType};
            var response = await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Post([FromRoute]int id)
        {
            await _mediator.Send(new DeleteLeaveTypeCommand{ Id = id});
            return NoContent();
        }
        
    }
}