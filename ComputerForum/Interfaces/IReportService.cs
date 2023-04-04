using ComputerForum.Models;

namespace ComputerForum.Interfaces
{
    public interface IReportService
    {
        void AddReport(Report report);
        void DeleteReport(int reportId);
        Report? GetReport(int reportId)
        IList<Report> GetReports();
        void AcceptReport(int reportId);
    }
}