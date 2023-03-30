using ComputerForum.Interfaces;
using ComputerForum.Models;

namespace ComputerForum.Service
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ReportService(IReportRepository reportRepository, IHttpContextAccessor httpContextAccessor)
        {
            _reportRepository = reportRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<Report> GetReports()
        {
            return _reportRepository.GetReports().ToList();
        }

        public Report GetReport(int reportId)
        {
            return _reportRepository.GetReport(reportId);
        }

        public void AddReport(Report report)
        {
            _reportRepository.AddReport(report);
        }
        public void DeleteReport(Report report)
        {
            _reportRepository.DeleteReport(report);
        }
    }
}
