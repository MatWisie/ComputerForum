using ComputerForum.Models;

namespace ComputerForum.Interfaces
{
    public interface IReportRepository
    {
        void AddReport(Report report);
        void DeleteReport(int reportId);
        Report? GetReport(int reportId);
        IEnumerable<Report> GetReports();

    }
}