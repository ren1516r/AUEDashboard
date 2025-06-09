using AUEDashboard.Data.Models.Domain;

namespace AUEDashboard.Data.Repository
{
    public interface IAssignmentRepository
    {
        Task<int> CreateAssignmentAsync(Assignment assignment);
        Task<IEnumerable<Assignment>> GetAllAssignmentsByFacultyId(string fid);
        Task<IEnumerable<Assignment>> GetAllAssessmentDetailsByStudentd(string fid);
    }
}