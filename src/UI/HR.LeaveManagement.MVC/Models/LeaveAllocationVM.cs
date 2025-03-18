using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.LeaveManagement.MVC.Models
{
    public class LeaveAllocationVM
    {
        public int NumberOfDays { get; set; }
        public LeaveTypeVM LeaveType { get; set; }
        public int Period { get; set; }
    }
}