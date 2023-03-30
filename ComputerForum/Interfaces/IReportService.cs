using ComputerForum.Models;

namespace ComputerForum.Interfaces
{
    public interface IReportService
    {
        void AddReport(Report report);
        void DeleteReport(Report report);
        Report GetReport(int reportId);
        IList<Report> GetReports();
    }
}