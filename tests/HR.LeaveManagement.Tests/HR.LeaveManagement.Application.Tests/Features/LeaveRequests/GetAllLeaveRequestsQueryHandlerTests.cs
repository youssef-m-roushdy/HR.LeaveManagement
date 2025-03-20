using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Queries;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Domain;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace HR.LeaveManagement.Application.Tests.Features.LeaveRequests.Handlers.Queries
{
    public class GetLeaveRequestListRequestHandlerTests
    {
        private readonly Mock<ILeaveRequestRepository> _mockLeaveRequestRepository;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetLeaveRequestListRequestHandler _handler;

        public GetLeaveRequestListRequestHandlerTests()
        {
            _mockLeaveRequestRepository = new Mock<ILeaveRequestRepository>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockUserService = new Mock<IUserService>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetLeaveRequestListRequestHandler(
                _mockLeaveRequestRepository.Object,
                _mockMapper.Object,
                _mockHttpContextAccessor.Object,
                _mockUserService.Object
            );
        }

        [Fact]
        public async Task Handle_WhenIsLoggedInUserIsTrue_ReturnsLeaveRequestsForLoggedInUser()
        {
            // Arrange
            var userId = "user-123";
            var employee = new Employee { Id = userId, FirstName = "John", LastName = "Doe" };
            var leaveRequests = new List<LeaveRequest>
            {
                new LeaveRequest { Id = 1, RequestingEmployeeId = userId },
                new LeaveRequest { Id = 2, RequestingEmployeeId = userId }
            };
            var leaveRequestDtos = new List<LeaveRequestListDto>
            {
                new LeaveRequestListDto { Id = 1, Employee = employee },
                new LeaveRequestListDto { Id = 2, Employee = employee }
            };

            var request = new GetLeaveRequestListRequest { IsLoggedInUser = true };

            // Mock HttpContext to return the userId
            var claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim("uid", userId)
            };
            var identity = new System.Security.Claims.ClaimsIdentity(claims);
            var principal = new System.Security.Claims.ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext { User = principal };
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

            // Mock repository to return leave requests for the user
            _mockLeaveRequestRepository.Setup(repo => repo.GetLeaveRequestsWithDetails(userId))
                .ReturnsAsync(leaveRequests);

            // Mock user service to return employee details
            _mockUserService.Setup(service => service.GetEmployee(userId))
                .ReturnsAsync(employee);

            // Mock mapper to return DTOs
            _mockMapper.Setup(mapper => mapper.Map<List<LeaveRequestListDto>>(leaveRequests))
                .Returns(leaveRequestDtos);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(leaveRequestDtos);
            foreach (var req in result)
            {
                req.Employee.Should().BeEquivalentTo(employee);
            }
        }

        [Fact]
        public async Task Handle_WhenIsLoggedInUserIsFalse_ReturnsAllLeaveRequests()
        {
            // Arrange
            var userId1 = "user-123";
            var userId2 = "user-456";
            var employee1 = new Employee { Id = userId1, FirstName = "John", LastName = "Doe" };
            var employee2 = new Employee { Id = userId2, FirstName = "Jane", LastName = "Doe" };

            var leaveRequests = new List<LeaveRequest>
            {
                new LeaveRequest { Id = 1, RequestingEmployeeId = userId1 },
                new LeaveRequest { Id = 2, RequestingEmployeeId = userId2 }
            };

            var leaveRequestDtos = new List<LeaveRequestListDto>
            {
                new LeaveRequestListDto { Id = 1, RequestingEmployeeId = userId1, Employee = employee1 },
                new LeaveRequestListDto { Id = 2, RequestingEmployeeId = userId2, Employee = employee2 }
            };

            var request = new GetLeaveRequestListRequest { IsLoggedInUser = false };

            // Mock repository to return all leave requests
            _mockLeaveRequestRepository.Setup(repo => repo.GetLeaveRequestsWithDetails())
                .ReturnsAsync(leaveRequests);

            // Mock user service to return employee details for each user
            _mockUserService.Setup(service => service.GetEmployee(userId1))
                .ReturnsAsync(employee1);
            _mockUserService.Setup(service => service.GetEmployee(userId2))
                .ReturnsAsync(employee2);

            // Mock mapper to return DTOs
            _mockMapper.Setup(mapper => mapper.Map<List<LeaveRequestListDto>>(leaveRequests))
                .Returns(leaveRequestDtos);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(leaveRequestDtos, options => options
                .Excluding(x => x.Employee) // Exclude Employee from the initial comparison
            );

            // Verify Employee properties separately
            result[0].Employee.Should().BeEquivalentTo(employee1);
            result[1].Employee.Should().BeEquivalentTo(employee2);
        }
    }
}