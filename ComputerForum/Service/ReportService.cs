using ComputerForum.Interfaces;
using ComputerForum.Models;
using ComputerForum.ViewModels;

namespace ComputerForum.Service
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IUserRepository _userRepository;
        public ReportService(IReportRepository reportRepository, IUserRepository userRepository)
        {
            _reportRepository = reportRepository;
            _userRepository = userRepository;
        }

        public IList<Report> GetReports()
        {
            return _reportRepository.GetReports().ToList();
        }

        public Report? GetReport(int reportId)
        {
            return _reportRepository.GetReport(reportId);
        }

        public void AddReport(ReportCreateVM report)
        {
            Report tmpreport = new Report()
            {
                Description = report.Description,
                ReportedUserId = report.ReportedUserId,
                ReportCreatorId = report.ReportCreatorId,
                TopicId = report.TopicId
            };
            _reportRepository.AddReport(tmpreport);
        }
        public void DeleteReport(Report report)
        {
            _reportRepository.DeleteReport(report);
        }

        public void AcceptReport(int reportId)
        {
            var report = _reportRepository.GetReport(reportId);
            if (report != null)
            {
                var user = _userRepository.GetUserById(report.ReportedUserId);
                if(user != null)
                {
                    user.Active = false;
                    _userRepository.UpdateUser(user);
                }
            }
        }
    }
}
