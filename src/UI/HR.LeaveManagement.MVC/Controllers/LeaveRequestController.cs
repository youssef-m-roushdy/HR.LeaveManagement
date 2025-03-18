using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace HR.LeaveManagement.MVC.Controllers
{
    [Authorize]
    public class LeaveRequestsController : Controller
    {
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly IMapper _mapper;

        public LeaveRequestsController(ILeaveTypeService leaveTypeService, ILeaveRequestService leaveRequestService,
            IMapper mapper)
        {
            this._leaveTypeService = leaveTypeService;
            this._leaveRequestService = leaveRequestService;
            this._mapper = mapper;
        }

        // GET: LeaveRequest/Create
        public async Task<ActionResult> Create()
        {
            var leaveTypes = await _leaveTypeService.GetLeaveTypes();
            var leaveTypeItems = new SelectList(leaveTypes, "Id", "Name");
            var model = new CreateLeaveRequestVM
            {
                LeaveTypes = leaveTypeItems
            };
            return View(model);
        }

        // POST: LeaveRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLeaveRequestVM leaveRequest)
        {
            ModelState.Remove("LeaveTypes");
            if (ModelState.IsValid)
            {
                var response = await _leaveRequestService.CreateLeaveRequest(leaveRequest);
                Console.WriteLine(response.Message);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                if (response.ValidationError != null)
                {
                    ModelState.AddModelError("", response.ValidationError);
                }
            }

            var leaveTypes = await _leaveTypeService.GetLeaveTypes();
            var leaveTypeItems = new SelectList(leaveTypes, "Id", "Name");
            leaveRequest.LeaveTypes = leaveTypeItems;

            return View(leaveRequest);
        }

        // GET: LeaveRequest/Index
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> AdminDashboard()
        {
            var model = await _leaveRequestService.GetAdminLeaveRequestList();
            return View("AdminIndex", model);
        }

        [Authorize]
        public async Task<ActionResult> UserDashboard()
        {
            var userModel = await _leaveRequestService.GetUserLeaveRequests();
            return View("UserIndex", userModel);
        }

        // GET: LeaveRequest/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await _leaveRequestService.GetLeaveRequest(id);
            return View(model);
        }

        // POST: LeaveRequest/ApproveRequest/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> ApproveRequest(int id, bool approved)
        {
            try
            {
                await _leaveRequestService.ApproveLeaveRequest(id, approved);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: LeaveRequest/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _leaveRequestService.GetLeaveRequest(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: LeaveRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _leaveRequestService.DeleteLeaveRequest(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}