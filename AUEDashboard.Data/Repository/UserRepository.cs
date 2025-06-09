using AUEDashboard.Data.DataAccess;
using AUEDashboard.Data.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUEDashboard.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ISqlDataAccess _db;

        public UserRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddAsync(User user)
        {
            try
            {
                await _db.SaveData("sp_create_user", new { user.FirstName, user.LastName, user.PasswordHash, user.Role });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<User> GetByUserId(string username)
        {
                IEnumerable<User> result = await _db.GetData<User, dynamic>("sp_get_user", new { Username = username });
                return result.FirstOrDefault();           
           
        }
        public async Task<IEnumerable<User>> GetAllByRole(string role)
        {
            IEnumerable<User> result = await _db.GetData<User, dynamic>("sp_get_all_byrole", new { role = role });
            return result; ;

        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            string query = "sp_get_people";
            return await _db.GetData<User, dynamic>(query, new { });
        }
    }
}
