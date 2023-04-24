using ComputerForum.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComputerForum.Controllers
{
    public class ReportController : Controller
    {
        private IReportService _reportService;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRoleValidation _roleValidation;
        public ReportController(IReportService reportService, IRoleValidation roleValidation)
        {
            _reportService = reportService;
            _roleValidation = roleValidation;
        }

        [Authorize]
        public IActionResult Index()
        {
            if (_roleValidation.CheckIfAdmin() != true)
            {
                return Unauthorized();
            }

            var reports = _reportService.GetReports();
            return View(reports);
        }

        [Authorize]
        public IActionResult Details(int reportId)
        {
            if (_roleValidation.CheckIfAdmin() != true)
            {
                return Unauthorized();
            }
            if(reportId == 0 || reportId == null)
            {
                return NotFound();
            }
            var report = _reportService.GetReport(reportId);
            return View(report);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int reportId)
        {
            if (_roleValidation.CheckIfAdmin() != true)
            {
                return Unauthorized();
            }
            if (reportId == 0 || reportId == null)
            {
                return NotFound();
            }
            _reportService.DeleteReport(reportId);
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult AcceptReport(int reportId)
        {
            if (_roleValidation.CheckIfAdmin() != true)
            {
                return Unauthorized();
            }
            if (reportId == 0 || reportId == null)
            {
                return NotFound();
            }
            _reportService.AcceptReport(reportId);
            return RedirectToAction("Index");
        }
    }
}
