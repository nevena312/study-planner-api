using StudyPlanner.Api.Models;

namespace StudyPlanner.Api.Repositories;

public interface ISubjectRepository
{
    Task<List<Subject>> GetAllByUserIdAsync(int userId);
    Task<Subject?> GetByIdAsync(int id, int userId);
    Task<Subject> AddAsync(Subject subject);
    Task SaveChangesAsync();
    void Delete(Subject subject);
}