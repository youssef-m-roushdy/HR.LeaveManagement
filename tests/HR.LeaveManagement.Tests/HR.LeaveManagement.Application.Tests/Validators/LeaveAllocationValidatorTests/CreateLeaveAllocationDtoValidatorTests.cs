using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HR.LeaveManagement.Domain;
using Moq;
using Xunit;

namespace HR.LeaveManagement.Tests.HR.LeaveManagement.Application.Tests.Validators.LeaveAllocationValidatorTests
{
    public class CreateLeaveAllocationDtoValidatorTests
    {
        private readonly Mock<ILeaveTypeRepository> _leaveTypeRepoMock;
        private readonly CreateLeaveAllocationDtoValidator _validator;

        public CreateLeaveAllocationDtoValidatorTests()
        {
            _leaveTypeRepoMock = new Mock<ILeaveTypeRepository>();
            _validator = new CreateLeaveAllocationDtoValidator(_leaveTypeRepoMock.Object);
        }

        [Fact]
        public async Task Validate_Should_ReturnValid_When_LeaveTypeExists()
        {
            // Arrange
            var createLeaveAllocationDto = new CreateLeaveAllocationDto
            {
                LeaveTypeId = 1 // Valid LeaveTypeId
            };

            // Mock LeaveTypeRepository to return true (LeaveType exists)
            _leaveTypeRepoMock.Setup(repo => repo.Exists(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            ValidationResult result = await _validator.ValidateAsync(createLeaveAllocationDto);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Validate_Should_ReturnInvalid_When_LeaveTypeDoesNotExist()
        {
            // Arrange
            var createLeaveAllocationDto = new CreateLeaveAllocationDto
            {
                LeaveTypeId = 99 // Invalid LeaveTypeId
            };

            // Mock LeaveTypeRepository to return false (LeaveType does not exist)
            _leaveTypeRepoMock.Setup(repo => repo.Exists(It.IsAny<int>()))
                .ReturnsAsync(false);

            // Act
            var result = await _validator.TestValidateAsync(createLeaveAllocationDto);

            // Assert
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(dto => dto.LeaveTypeId)
                .WithErrorMessage("Leave Type Id does not exist."); // Use the actual message
        }

        [Fact]
        public async Task Validate_Should_ReturnInvalid_When_LeaveTypeIdIsZeroOrNegative()
        {
            // Arrange
            var createLeaveAllocationDto = new CreateLeaveAllocationDto
            {
                LeaveTypeId = 0 // Invalid LeaveTypeId
            };

            // Act
            var result = await _validator.TestValidateAsync(createLeaveAllocationDto);

            // Assert
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(dto => dto.LeaveTypeId);
        }
    }
}