using ComputerForum.Interfaces;
using ComputerForum.Models;

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

        public void AddReport(Report report)
        {
            _reportRepository.AddReport(report);
        }
        public void DeleteReport(int reportId)
        {
            if(_reportRepository.GetReport(reportId) != null)
                _reportRepository.DeleteReport(reportId);
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
