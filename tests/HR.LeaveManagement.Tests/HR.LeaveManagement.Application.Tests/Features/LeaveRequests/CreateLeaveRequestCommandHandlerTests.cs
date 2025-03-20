using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Domain;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace HR.LeaveManagement.Tests.HR.LeaveManagement.Application.Tests.Features.LeaveRequests
{
    public class CreateLeaveRequestCommandHandlerTests
    {

        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IEmailSender> _mockEmailSender;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly CreateLeaveRequestCommandHandler _handler;

        public CreateLeaveRequestCommandHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockEmailSender = new Mock<IEmailSender>();
            _mockMapper = new Mock<IMapper>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            // Mock HttpContext User Claims
            var claims = new List<Claim> { new Claim("uid", "test-user-id"), new Claim(ClaimTypes.Email, "test@example.com") };
            var identity = new ClaimsIdentity(claims, "testAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(ctx => ctx.User).Returns(claimsPrincipal);
            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext).Returns(mockHttpContext.Object);

            // Mock LeaveTypeRepository
            var mockLeaveTypeRepository = new Mock<ILeaveTypeRepository>();
            _mockUnitOfWork.Setup(uow => uow.LeaveTypeRepository).Returns(mockLeaveTypeRepository.Object);

            // Mock LeaveAllocationRepository
            var mockLeaveAllocationRepository = new Mock<ILeaveAllocationRepository>();
            mockLeaveAllocationRepository.Setup(repo => repo.GetUserAllocations(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new LeaveAllocation { NumberOfDays = 5 });
            _mockUnitOfWork.Setup(uow => uow.LeaveAllocationRepository).Returns(mockLeaveAllocationRepository.Object);

            // Mock LeaveRequestRepository
            var mockLeaveRequestRepository = new Mock<ILeaveRequestRepository>();
            _mockUnitOfWork.Setup(uow => uow.LeaveRequestRepository).Returns(mockLeaveRequestRepository.Object);

            _handler = new CreateLeaveRequestCommandHandler(
                _mockUnitOfWork.Object,
                _mockMapper.Object,
                _mockEmailSender.Object,
                _mockHttpContextAccessor.Object
            );
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Arrange
            var request = new CreateLeaveRequestCommand
            {
                LeaveRequestDto = new CreateLeaveRequestDto
                {
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(3),
                    LeaveTypeId = 1
                }
            };

            // Mock the LeaveTypeRepository to return true for the LeaveTypeId
            _mockUnitOfWork.Setup(uow => uow.LeaveTypeRepository.Exists(It.IsAny<int>()))
                .ReturnsAsync(true);

            var leaveRequest = new LeaveRequest { Id = 1 };
            _mockMapper.Setup(m => m.Map<LeaveRequest>(It.IsAny<CreateLeaveRequestDto>())).Returns(leaveRequest);
            _mockUnitOfWork.Setup(uow => uow.LeaveRequestRepository.Add(It.IsAny<LeaveRequest>()))
                .ReturnsAsync(leaveRequest);
            _mockUnitOfWork.Setup(uow => uow.Save()).Returns(Task.CompletedTask);

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeTrue();
            response.Message.Should().Be("Creation successful");
            response.Id.Should().Be(1);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ReturnsFailureResponse()
        {
            // Arrange
            var request = new CreateLeaveRequestCommand
            {
                LeaveRequestDto = new CreateLeaveRequestDto
                {
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(10), // Exceeds allocation
                    LeaveTypeId = 1
                }
            };

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeFalse();
            response.Message.Should().Be("Creation failed");
            response.Errors.Should().Contain("You do not have enough days for this request");
        }
    }
}
