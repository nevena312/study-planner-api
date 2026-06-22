using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyPlanner.Api.Data;
using StudyPlanner.Api.DTOs.StudyTasks;
using StudyPlanner.Api.Models;

namespace StudyPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StudyTasksController : ControllerBase
{
    private readonly StudyPlannerDbContext _context;

    public StudyTasksController(StudyPlannerDbContext context)
    {
        _context = context;
    }

    private int GetCurrentUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    [HttpGet]
    public async Task<ActionResult<List<StudyTaskReadDto>>> GetAll()
    {
        var userId = GetCurrentUserId();

        var tasks = await _context.StudyTasks
            .Include(t => t.Subject)
            .Include(t => t.StudyPlan)
            .Where(t => t.Subject.UserId == userId)
            .Select(t => new StudyTaskReadDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                Deadline = t.Deadline,
                EstimatedDurationMinutes = t.EstimatedDurationMinutes,
                SubjectId = t.SubjectId,
                SubjectName = t.Subject.Name,
                StudyPlanId = t.StudyPlanId,
                StudyPlanTitle = t.StudyPlan != null ? t.StudyPlan.Title : null
            })
            .ToListAsync();

        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudyTaskReadDto>> GetById(int id)
    {
        var userId = GetCurrentUserId();

        var task = await _context.StudyTasks
            .Include(t => t.Subject)
            .Include(t => t.StudyPlan)
            .Where(t => t.Id == id && t.Subject.UserId == userId)
            .Select(t => new StudyTaskReadDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                Deadline = t.Deadline,
                EstimatedDurationMinutes = t.EstimatedDurationMinutes,
                SubjectId = t.SubjectId,
                SubjectName = t.Subject.Name,
                StudyPlanId = t.StudyPlanId,
                StudyPlanTitle = t.StudyPlan != null ? t.StudyPlan.Title : null
            })
            .FirstOrDefaultAsync();

        if (task == null)
            return NotFound();

        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<StudyTaskReadDto>> Create(StudyTaskCreateDto dto)
    {
        var userId = GetCurrentUserId();

        var subject = await _context.Subjects
            .FirstOrDefaultAsync(s => s.Id == dto.SubjectId && s.UserId == userId);

        if (subject == null)
            return BadRequest("Subject does not exist or does not belong to current user.");

        StudyPlan? studyPlan = null;

        if (dto.StudyPlanId != null)
        {
            studyPlan = await _context.StudyPlans
                .FirstOrDefaultAsync(sp => sp.Id == dto.StudyPlanId && sp.UserId == userId);

            if (studyPlan == null)
                return BadRequest("Study plan does not exist or does not belong to current user.");
        }

        var task = new StudyTask
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status,
            Priority = dto.Priority,
            Deadline = dto.Deadline,
            EstimatedDurationMinutes = dto.EstimatedDurationMinutes,
            SubjectId = dto.SubjectId,
            StudyPlanId = dto.StudyPlanId
        };

        _context.StudyTasks.Add(task);
        await _context.SaveChangesAsync();

        var result = new StudyTaskReadDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            Priority = task.Priority,
            Deadline = task.Deadline,
            EstimatedDurationMinutes = task.EstimatedDurationMinutes,
            SubjectId = task.SubjectId,
            SubjectName = subject.Name,
            StudyPlanId = task.StudyPlanId,
            StudyPlanTitle = studyPlan?.Title
        };

        return CreatedAtAction(nameof(GetById), new { id = task.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, StudyTaskUpdateDto dto)
    {
        var userId = GetCurrentUserId();

        var task = await _context.StudyTasks
            .Include(t => t.Subject)
            .FirstOrDefaultAsync(t => t.Id == id && t.Subject.UserId == userId);

        if (task == null)
            return NotFound();

        var subject = await _context.Subjects
            .FirstOrDefaultAsync(s => s.Id == dto.SubjectId && s.UserId == userId);

        if (subject == null)
            return BadRequest("Subject does not exist or does not belong to current user.");

        if (dto.StudyPlanId != null)
        {
            var studyPlanExists = await _context.StudyPlans
                .AnyAsync(sp => sp.Id == dto.StudyPlanId && sp.UserId == userId);

            if (!studyPlanExists)
                return BadRequest("Study plan does not exist or does not belong to current user.");
        }

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.Status = dto.Status;
        task.Priority = dto.Priority;
        task.Deadline = dto.Deadline;
        task.EstimatedDurationMinutes = dto.EstimatedDurationMinutes;
        task.SubjectId = dto.SubjectId;
        task.StudyPlanId = dto.StudyPlanId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = GetCurrentUserId();

        var task = await _context.StudyTasks
            .Include(t => t.Subject)
            .FirstOrDefaultAsync(t => t.Id == id && t.Subject.UserId == userId);

        if (task == null)
            return NotFound();

        _context.StudyTasks.Remove(task);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}