using ComputerForum.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComputerForum.Controllers
{
    public class ReportController : Controller
    {
        private IReportService _reportService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ReportController(IReportService reportService, IHttpContextAccessor httpContextAccessor)
        {
            _reportService = reportService;
            _httpContextAccessor = httpContextAccessor;
        }


        // GET: ReportController
        public IActionResult Index()
        {
            if (_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value != "Admin")
            {
                return Unauthorized();
            }

            var reports = _reportService.GetReports();
            return View(reports);
        }

        // GET: ReportController/Details/5
        public IActionResult Details(int reportId)
        {
            if (_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value != "Admin")
            {
                return Unauthorized();
            }
            var report = _reportService.GetReport(reportId);
            return View(report);
        }

        // POST: ReportController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int reportId)
        {
            if (_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value != "Admin")
            {
                return Unauthorized();
            }
            _reportService.DeleteReport(reportId);
            return RedirectToAction("Index");
        }

        public IActionResult AcceptReport(int reportId)
        {
            if (_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value != "Admin")
            {
                return Unauthorized();
            }
            _reportService.AcceptReport(reportId);
            return RedirectToAction("Index");
        }
    }
}
