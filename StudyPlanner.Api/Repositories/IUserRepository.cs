using StudyPlanner.Api.Models;

namespace StudyPlanner.Api.Repositories;

public interface IUserRepository
{
    Task<bool> EmailExistsAsync(string email);
    Task<User?> GetByEmailAsync(string email);
    Task<User> AddAsync(User user);
}