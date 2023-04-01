using ComputerForum.Models;

namespace ComputerForum.Interfaces
{
    public interface IReportRepository
    {
        void AddReport(Report report);
        void DeleteReport(Report report);
        Report? GetReport(int reportId);
        IEnumerable<Report> GetReports();
    }
}