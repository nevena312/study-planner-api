using Microsoft.EntityFrameworkCore;
using StudyPlanner.Api.Data;
using StudyPlanner.Api.Enums;
using StudyPlanner.Api.Models;

namespace StudyPlanner.Api.Repositories;

public class StudyTaskRepository : IStudyTaskRepository
{
    private readonly StudyPlannerDbContext _context;

    public StudyTaskRepository(StudyPlannerDbContext context)
    {
        _context = context;
    }

    public async Task<List<StudyTask>> GetAllByUserIdAsync(int userId)
    {
        return await _context.StudyTasks
            .Include(t => t.Subject)
            .Include(t => t.StudyPlan)
            .Where(t => t.Subject.UserId == userId)
            .ToListAsync();
    }

    public async Task<StudyTask?> GetByIdAsync(int id, int userId)
    {
        return await _context.StudyTasks
            .Include(t => t.Subject)
            .Include(t => t.StudyPlan)
            .FirstOrDefaultAsync(t => t.Id == id && t.Subject.UserId == userId);
    }

    public async Task<List<StudyTask>> GetPendingByUserIdAsync(int userId)
    {
        return await _context.StudyTasks
            .Include(t => t.Subject)
            .Include(t => t.StudyPlan)
            .Where(t => t.Subject.UserId == userId &&
                        t.Status != StudyTaskStatus.Completed)
            .OrderBy(t => t.Deadline)
            .ToListAsync();
    }

    public async Task<List<StudyTask>> GetBySubjectAsync(int subjectId, int userId)
    {
        return await _context.StudyTasks
            .Include(t => t.Subject)
            .Include(t => t.StudyPlan)
            .Where(t => t.SubjectId == subjectId &&
                        t.Subject.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<StudyTask>> GetByPlanAsync(int planId, int userId)
    {
        return await _context.StudyTasks
            .Include(t => t.Subject)
            .Include(t => t.StudyPlan)
            .Where(t => t.StudyPlanId == planId &&
                        t.Subject.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<StudyTask>> GetPendingWithoutPlanByUserIdAsync(int userId)
    {
        return await _context.StudyTasks
            .Include(t => t.Subject)
            .Include(t => t.StudyPlan)
            .Where(t => t.Subject.UserId == userId &&
                        t.Status != StudyTaskStatus.Completed &&
                        t.StudyPlanId == null)
            .OrderBy(t => t.Deadline == null)
            .ThenBy(t => t.Deadline)
            .ThenByDescending(t => t.Priority)
            .ToListAsync();
    }

    public async Task<StudyTask> AddAsync(StudyTask task)
    {
        _context.StudyTasks.Add(task);
        await _context.SaveChangesAsync();

        return task;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Delete(StudyTask task)
    {
        _context.StudyTasks.Remove(task);
    }
}