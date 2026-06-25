using StudyPlanner.Api.Models;

namespace StudyPlanner.Api.Repositories;

public interface IStudyPlanRepository
{
    Task<List<StudyPlan>> GetAllByUserIdAsync(int userId);
    Task<StudyPlan?> GetByIdAsync(int id, int userId);
    Task<StudyPlan> AddAsync(StudyPlan studyPlan);
    Task SaveChangesAsync();
    void Delete(StudyPlan studyPlan);
}