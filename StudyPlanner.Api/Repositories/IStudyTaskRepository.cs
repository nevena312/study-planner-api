using StudyPlanner.Api.Models;

namespace StudyPlanner.Api.Repositories;

public interface IStudyTaskRepository
{
    Task<List<StudyTask>> GetAllByUserIdAsync(int userId);
    Task<StudyTask?> GetByIdAsync(int id, int userId);
    Task<List<StudyTask>> GetPendingByUserIdAsync(int userId);
    Task<List<StudyTask>> GetBySubjectAsync(int subjectId, int userId);
    Task<List<StudyTask>> GetByPlanAsync(int planId, int userId);

    Task<List<StudyTask>> GetPendingWithoutPlanByUserIdAsync(int userId);

    Task<StudyTask> AddAsync(StudyTask task);
    Task SaveChangesAsync();
    void Delete(StudyTask task);
}