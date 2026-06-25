using StudyPlanner.Api.Models;

namespace StudyPlanner.Api.Repositories;

public interface IExamRepository
{
    Task<List<Exam>> GetAllByUserIdAsync(int userId);
    Task<Exam?> GetByIdAsync(int id, int userId);
    Task<List<Exam>> GetUpcomingByUserIdAsync(int userId);
    Task<Exam> AddAsync(Exam exam);
    Task SaveChangesAsync();
    void Delete(Exam exam);
}