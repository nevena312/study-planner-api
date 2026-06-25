using Microsoft.EntityFrameworkCore;
using StudyPlanner.Api.Data;
using StudyPlanner.Api.Models;

namespace StudyPlanner.Api.Repositories;

public class StudyPlanRepository : IStudyPlanRepository
{
    private readonly StudyPlannerDbContext _context;

    public StudyPlanRepository(StudyPlannerDbContext context)
    {
        _context = context;
    }

    public async Task<List<StudyPlan>> GetAllByUserIdAsync(int userId)
    {
        return await _context.StudyPlans
            .Include(sp => sp.StudyTasks)
            .Where(sp => sp.UserId == userId)
            .ToListAsync();
    }

    public async Task<StudyPlan?> GetByIdAsync(int id, int userId)
    {
        return await _context.StudyPlans
            .Include(sp => sp.StudyTasks)
            .FirstOrDefaultAsync(sp => sp.Id == id && sp.UserId == userId);
    }

    public async Task<StudyPlan> AddAsync(StudyPlan studyPlan)
    {
        _context.StudyPlans.Add(studyPlan);
        await _context.SaveChangesAsync();

        return studyPlan;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Delete(StudyPlan studyPlan)
    {
        _context.StudyPlans.Remove(studyPlan);
    }
}