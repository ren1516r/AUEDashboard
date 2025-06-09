using System;
using AUEDashboard.Data.Models.Domain;
namespace AUEDashboard.Data.Repository
{
    public interface IUserRepository
    {
        Task<bool> AddAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task<IEnumerable<User>> GetAllByRole(string role);
        Task<User> GetByUserId(string username);
    }
}