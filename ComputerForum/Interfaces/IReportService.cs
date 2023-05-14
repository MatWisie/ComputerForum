using ComputerForum.Models;
using ComputerForum.ViewModels;

namespace ComputerForum.Interfaces
{
    public interface IReportService
    {
        void AddReport(ReportCreateVM report);
        void DeleteReport(Report report);
        Report? GetReport(int reportId);
        IList<Report> GetReports();
        void AcceptReport(int reportId);
    }
}