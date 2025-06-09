using AUEDashboard.Data.DataAccess;
using AUEDashboard.Data.Models.Domain;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUEDashboard.Data.Repository
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly ISqlDataAccess _db;

        public AssignmentRepository(ISqlDataAccess db)
        {
            _db = db;
                
        }
        public async Task<int> CreateAssignmentAsync(Assignment assignment)
        {
            
                var parameters = new DynamicParameters();
                parameters.Add("@FacultyUserId", assignment.FacultyUserId);
                parameters.Add("@Title", assignment.Title);
                parameters.Add("@Description", assignment.Description);
                parameters.Add("@DueDate", assignment.DueDate);
                parameters.Add("@MaxMarks", assignment.MaxMarks);
                parameters.Add("@FilePath", assignment.FilePath);
                parameters.Add("@FileName", assignment.FileName);
                parameters.Add("@AssignmentId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await _db.SaveData("sp_create_assignment", parameters);

                return parameters.Get<int>("@AssignmentId");
            
        }
        public async Task<IEnumerable<Assignment>> GetAllAssignmentsByFacultyId(string fid)
        {
            IEnumerable<Assignment> result = await _db.GetData<Assignment, dynamic>("sp_get_assg_byFId", new { FacultyUserId = fid });
            return result; ;
        }
        public async Task<IEnumerable<Assignment>> GetAllAssessmentDetailsByStudentd(string studid)
        {
            IEnumerable<Assignment> result = await _db.GetData<Assignment, dynamic>("sp_view_asglist_bystudId", new { StudentId = studid });
            return result; ;
        }
    }
}
