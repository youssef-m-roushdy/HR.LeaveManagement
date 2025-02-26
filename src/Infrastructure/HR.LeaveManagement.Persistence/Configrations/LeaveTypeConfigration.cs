using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HR.LeaveManagement.Persistence.Configrations
{
    public class LeaveTypeConfigration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasData(
                new LeaveType
                {
                    Id = 1,
                    Name = "Vacation",
                    DefaultDays = 10,
                    CreatedBy = "System",
                    LastModifiedBy = "System",
                    DateCreated = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                },
        new LeaveType
        {
            Id = 2,
            Name = "Sick Leave",
            DefaultDays = 5,
            CreatedBy = "System",
            LastModifiedBy = "System",
            DateCreated = DateTime.Now,
            LastModifiedDate = DateTime.Now
        }
            );
        }
    }
}