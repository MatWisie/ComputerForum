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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRoleValidation _roleValidation;
        private readonly ILogger<ReportController> _logger;
        public ReportController(IReportService reportService, IRoleValidation roleValidation, IHttpContextAccessor httpContextAccessor, ILogger<ReportController> logger)
        {
            _reportService = reportService;
            _roleValidation = roleValidation;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
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
        public IActionResult DeleteReport(int reportId)
        {
            if (_roleValidation.CheckIfAdmin() != true)
            {
                return Unauthorized();
            }
            if (reportId == 0 || reportId == null)
            {
                return NotFound();
            }
            var report = _reportService.GetReport(reportId);
            if (report == null)
            {
                return NotFound();
            }
            if (Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value) == report.ReportedUserId)
            {
                return Unauthorized();
            }

            _reportService.DeleteReport(report);
            _logger.LogInformation("Report " + report.Id + " deleted");
            return new JsonResult(Ok());
        }

        [Authorize]
        [HttpPost]
        public IActionResult AcceptReport(int reportId)
        {
            if (_roleValidation.CheckIfAdmin() != true)
            {
                _logger.LogError("Report accept error, not authorized");
                return Unauthorized();
            }
            if (reportId == 0 || reportId == null)
            {
                _logger.LogError("Report accept error, not found");
                return NotFound();
            }
            var report = _reportService.GetReport(reportId);
            if (report == null)
            {
                _logger.LogError("Report accept error, not found");
                return NotFound();
            }
            if (Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value) == report.ReportedUserId)
            {
                _logger.LogError("Report accept " + report.Id + " error, unauthorized");
                return Unauthorized();
            }

            _reportService.AcceptReport(reportId);
            _reportService.DeleteReport(report);
            _logger.LogInformation("Report " + report.Id + " accepted");
            return new JsonResult(Ok());
        }
    }
}
