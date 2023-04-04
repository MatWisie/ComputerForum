using ComputerForum.Data;
using ComputerForum.Interfaces;
using ComputerForum.Models;

namespace ComputerForum.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly ForumDbContext _context;
        public ReportRepository(ForumDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Report> GetReports()
        {
            return _context.Reports;
        }

        public Report? GetReport(int reportId)
        {
            return _context.Reports.FirstOrDefault(e => e.Id == reportId);
        }

        public void AddReport(Report report)
        {
            _context.Reports.Add(report);
            _context.SaveChanges();
        }
        public void DeleteReport(int reportId)
        {
            var report = GetReport(reportId);
            _context.Reports.Remove(report);
            _context.SaveChanges();
        }
    }
}
