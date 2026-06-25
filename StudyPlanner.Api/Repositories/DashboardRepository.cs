using Microsoft.EntityFrameworkCore;
using StudyPlanner.Api.Data;
using StudyPlanner.Api.DTOs.Dashboard;
using StudyPlanner.Api.Enums;

namespace StudyPlanner.Api.Repositories;

public class DashboardRepository : IDashboardRepository
{
    private readonly StudyPlannerDbContext _context;

    public DashboardRepository(StudyPlannerDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardDto> GetDashboardAsync(int userId)
    {
        var now = DateTime.UtcNow;

        var nextExam = await _context.Exams
            .Include(e => e.Subject)
            .Where(e => e.Subject.UserId == userId && e.ExamDate >= now)
            .OrderBy(e => e.ExamDate)
            .FirstOrDefaultAsync();

        return new DashboardDto
        {
            SubjectCount = await _context.Subjects
                .CountAsync(s => s.UserId == userId),

            ExamCount = await _context.Exams
                .CountAsync(e => e.Subject.UserId == userId),

            PendingTaskCount = await _context.StudyTasks
                .CountAsync(t => t.Subject.UserId == userId &&
                                 t.Status != StudyTaskStatus.Completed),

            CompletedTaskCount = await _context.StudyTasks
                .CountAsync(t => t.Subject.UserId == userId &&
                                 t.Status == StudyTaskStatus.Completed),

            NextExamTitle = nextExam?.Title,
            NextExamDate = nextExam?.ExamDate,
            NextExamSubjectName = nextExam?.Subject.Name
        };
    }
}