using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyPlanner.Api.Data;
using StudyPlanner.Api.DTOs.Dashboard;
using StudyPlanner.Api.Enums;

namespace StudyPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly StudyPlannerDbContext _context;

    public DashboardController(StudyPlannerDbContext context)
    {
        _context = context;
    }

    private int GetCurrentUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    [HttpGet]
    public async Task<ActionResult<DashboardDto>> GetDashboard()
    {
        var userId = GetCurrentUserId();
        var now = DateTime.UtcNow;

        var nextExam = await _context.Exams
            .Include(e => e.Subject)
            .Where(e => e.Subject.UserId == userId && e.ExamDate >= now)
            .OrderBy(e => e.ExamDate)
            .FirstOrDefaultAsync();

        var dto = new DashboardDto
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

        return Ok(dto);
    }
}