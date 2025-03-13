using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Persistence.Repositories;

namespace HR.LeaveManagement.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LeaveManagementDbContext _context;
        public ILeaveAllocationRepository _leaveAllocationRepository;

        public ILeaveRequestRepository _leaveRequestRepository;

        public ILeaveTypeRepository _leaveTypeRepository;

        public UnitOfWork(LeaveManagementDbContext context)
        {
            _context = context;
        }

        public ILeaveAllocationRepository LeaveAllocationRepository => _leaveAllocationRepository ??= new LeaveAllocationRepository(_context);

        public ILeaveRequestRepository LeaveRequestRepository => _leaveRequestRepository ??= new LeaveRequestRepository(_context);

        public ILeaveTypeRepository LeaveTypeRepository => _leaveTypeRepository ??= new LeaveTypeRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}