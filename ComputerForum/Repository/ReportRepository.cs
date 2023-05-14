using ComputerForum.Data;
using ComputerForum.Interfaces;
using ComputerForum.Models;
using Microsoft.EntityFrameworkCore;

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
            return _context.Reports.Include(e => e.Topic).Include(e => e.ReportedUser).Include(e => e.ReportCreator);
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
        public void DeleteReport(Report report)
        {
            _context.Reports.Remove(report);
            _context.SaveChanges();
        }
    }
}
