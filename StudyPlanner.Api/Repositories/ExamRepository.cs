using Microsoft.EntityFrameworkCore;
using StudyPlanner.Api.Data;
using StudyPlanner.Api.Models;

namespace StudyPlanner.Api.Repositories;

public class ExamRepository : IExamRepository
{
    private readonly StudyPlannerDbContext _context;

    public ExamRepository(StudyPlannerDbContext context)
    {
        _context = context;
    }

    public async Task<List<Exam>> GetAllByUserIdAsync(int userId)
    {
        return await _context.Exams
            .Include(e => e.Subject)
            .Where(e => e.Subject.UserId == userId)
            .ToListAsync();
    }

    public async Task<Exam?> GetByIdAsync(int id, int userId)
    {
        return await _context.Exams
            .Include(e => e.Subject)
            .FirstOrDefaultAsync(e => e.Id == id && e.Subject.UserId == userId);
    }

    public async Task<List<Exam>> GetUpcomingByUserIdAsync(int userId)
    {
        var now = DateTime.UtcNow;

        return await _context.Exams
            .Include(e => e.Subject)
            .Where(e => e.Subject.UserId == userId && e.ExamDate >= now)
            .OrderBy(e => e.ExamDate)
            .ToListAsync();
    }

    public async Task<Exam> AddAsync(Exam exam)
    {
        _context.Exams.Add(exam);
        await _context.SaveChangesAsync();

        return exam;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Delete(Exam exam)
    {
        _context.Exams.Remove(exam);
    }
}