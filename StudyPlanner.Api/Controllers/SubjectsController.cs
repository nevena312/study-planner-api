using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyPlanner.Api.Data;
using StudyPlanner.Api.DTOs.Subjects;
using StudyPlanner.Api.Models;

namespace StudyPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SubjectsController : ControllerBase
{
    private readonly StudyPlannerDbContext _context;

    public SubjectsController(StudyPlannerDbContext context)
    {
        _context = context;
    }

    private int GetCurrentUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    [HttpGet]
    public async Task<ActionResult<List<SubjectReadDto>>> GetAll()
    {
        var userId = GetCurrentUserId();

        var subjects = await _context.Subjects
            .Where(s => s.UserId == userId)
            .Select(s => new SubjectReadDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description
            })
            .ToListAsync();

        return Ok(subjects);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SubjectReadDto>> GetById(int id)
    {
        var userId = GetCurrentUserId();

        var subject = await _context.Subjects
            .Where(s => s.Id == id && s.UserId == userId)
            .Select(s => new SubjectReadDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description
            })
            .FirstOrDefaultAsync();

        if (subject == null)
            return NotFound();

        return Ok(subject);
    }

    [HttpPost]
    public async Task<ActionResult<SubjectReadDto>> Create(SubjectCreateDto dto)
    {
        var userId = GetCurrentUserId();

        var subject = new Subject
        {
            Name = dto.Name,
            Description = dto.Description,
            UserId = userId
        };

        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync();

        var result = new SubjectReadDto
        {
            Id = subject.Id,
            Name = subject.Name,
            Description = subject.Description
        };

        return CreatedAtAction(nameof(GetById), new { id = subject.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, SubjectUpdateDto dto)
    {
        var userId = GetCurrentUserId();

        var subject = await _context.Subjects
            .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

        if (subject == null)
            return NotFound();

        subject.Name = dto.Name;
        subject.Description = dto.Description;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = GetCurrentUserId();

        var subject = await _context.Subjects
            .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

        if (subject == null)
            return NotFound();

        _context.Subjects.Remove(subject);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}