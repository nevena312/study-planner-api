using Microsoft.EntityFrameworkCore;
using StudyPlanner.Api.Data;
using StudyPlanner.Api.Models;

namespace StudyPlanner.Api.Repositories;

public class SubjectRepository : ISubjectRepository
{
    private readonly StudyPlannerDbContext _context;

    public SubjectRepository(StudyPlannerDbContext context)
    {
        _context = context;
    }

    public async Task<List<Subject>> GetAllByUserIdAsync(int userId)
    {
        return await _context.Subjects
            .Where(s => s.UserId == userId)
            .ToListAsync();
    }

    public async Task<Subject?> GetByIdAsync(int id, int userId)
    {
        return await _context.Subjects
            .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);
    }

    public async Task<Subject> AddAsync(Subject subject)
    {
        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync();

        return subject;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Delete(Subject subject)
    {
        _context.Subjects.Remove(subject);
    }
}