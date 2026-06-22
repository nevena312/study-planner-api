using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyPlanner.Api.Data;
using StudyPlanner.Api.DTOs.StudyPlans;
using StudyPlanner.Api.Models;

namespace StudyPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StudyPlansController : ControllerBase
{
    private readonly StudyPlannerDbContext _context;

    public StudyPlansController(StudyPlannerDbContext context)
    {
        _context = context;
    }

    private int GetCurrentUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    [HttpGet]
    public async Task<ActionResult<List<StudyPlanReadDto>>> GetAll()
    {
        var userId = GetCurrentUserId();

        var plans = await _context.StudyPlans
            .Where(sp => sp.UserId == userId)
            .Select(sp => new StudyPlanReadDto
            {
                Id = sp.Id,
                Title = sp.Title,
                StartDate = sp.StartDate,
                EndDate = sp.EndDate,
                Description = sp.Description,
                CreatedAt = sp.CreatedAt,
                TaskCount = sp.StudyTasks.Count
            })
            .ToListAsync();

        return Ok(plans);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudyPlanReadDto>> GetById(int id)
    {
        var userId = GetCurrentUserId();

        var plan = await _context.StudyPlans
            .Where(sp => sp.Id == id && sp.UserId == userId)
            .Select(sp => new StudyPlanReadDto
            {
                Id = sp.Id,
                Title = sp.Title,
                StartDate = sp.StartDate,
                EndDate = sp.EndDate,
                Description = sp.Description,
                CreatedAt = sp.CreatedAt,
                TaskCount = sp.StudyTasks.Count
            })
            .FirstOrDefaultAsync();

        if (plan == null)
            return NotFound();

        return Ok(plan);
    }

    [HttpPost]
    public async Task<ActionResult<StudyPlanReadDto>> Create(StudyPlanCreateDto dto)
    {
        var userId = GetCurrentUserId();

        var plan = new StudyPlan
        {
            Title = dto.Title,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow,
            UserId = userId
        };

        _context.StudyPlans.Add(plan);
        await _context.SaveChangesAsync();

        var result = new StudyPlanReadDto
        {
            Id = plan.Id,
            Title = plan.Title,
            StartDate = plan.StartDate,
            EndDate = plan.EndDate,
            Description = plan.Description,
            CreatedAt = plan.CreatedAt,
            TaskCount = 0
        };

        return CreatedAtAction(nameof(GetById), new { id = plan.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, StudyPlanUpdateDto dto)
    {
        var userId = GetCurrentUserId();

        var plan = await _context.StudyPlans
            .FirstOrDefaultAsync(sp => sp.Id == id && sp.UserId == userId);

        if (plan == null)
            return NotFound();

        plan.Title = dto.Title;
        plan.StartDate = dto.StartDate;
        plan.EndDate = dto.EndDate;
        plan.Description = dto.Description;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = GetCurrentUserId();

        var plan = await _context.StudyPlans
            .FirstOrDefaultAsync(sp => sp.Id == id && sp.UserId == userId);

        if (plan == null)
            return NotFound();

        _context.StudyPlans.Remove(plan);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}