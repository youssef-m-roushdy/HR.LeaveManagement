using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using Moq;
using Xunit;

namespace HR.LeaveManagement.Tests.HR.LeaveManagement.Application.Tests.Validators.LeaveRequestValidatorTests
{
    public class CreateLeaveRequestDtoValidatorTests
    {
        private readonly CreateLeaveRequestDtoValidator _validator;
        private readonly Mock<ILeaveTypeRepository> _mockLeaveTypeRepository;

        public CreateLeaveRequestDtoValidatorTests()
        {
            // Arrange: Create a mock of ILeaveTypeRepository
            _mockLeaveTypeRepository = new Mock<ILeaveTypeRepository>();

            // Set up the mock to return true for any leave type ID
            _mockLeaveTypeRepository
                .Setup(repo => repo.Exists(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Instantiate the validator with the mocked repository
            _validator = new CreateLeaveRequestDtoValidator(_mockLeaveTypeRepository.Object);
        }

        [Fact]
        public async Task Should_Have_Error_When_LeaveTypeId_Does_Not_Exist()
        {
            // Arrange
            var model = new CreateLeaveRequestDto { LeaveTypeId = 99 };

            // Set up the mock to return false for this specific leave type ID
            _mockLeaveTypeRepository
                .Setup(repo => repo.Exists(99))
                .ReturnsAsync(false);

            // Act
            var result = await _validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(dto => dto.LeaveTypeId)
                  .WithErrorMessage("Leave Type Id does not exist."); // Use the actual message
        }

        [Fact]
        public async Task Should_Not_Have_Error_When_LeaveTypeId_Exists()
        {
            // Arrange
            var model = new CreateLeaveRequestDto { LeaveTypeId = 1 };

            // Set up the mock to return true for this specific leave type ID
            _mockLeaveTypeRepository
                .Setup(repo => repo.Exists(1))
                .ReturnsAsync(true);

            // Act
            var result = await _validator.TestValidateAsync(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(dto => dto.LeaveTypeId);
        }
    }
}