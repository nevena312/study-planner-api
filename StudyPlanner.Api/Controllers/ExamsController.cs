using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyPlanner.Api.Data;
using StudyPlanner.Api.DTOs.Exams;
using StudyPlanner.Api.Models;

namespace StudyPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExamsController : ControllerBase
{
    private readonly StudyPlannerDbContext _context;

    public ExamsController(StudyPlannerDbContext context)
    {
        _context = context;
    }

    private int GetCurrentUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    [HttpGet]
    public async Task<ActionResult<List<ExamReadDto>>> GetAll()
    {
        var userId = GetCurrentUserId();

        var exams = await _context.Exams
            .Include(e => e.Subject)
            .Where(e => e.Subject.UserId == userId)
            .Select(e => new ExamReadDto
            {
                Id = e.Id,
                Title = e.Title,
                ExamDate = e.ExamDate,
                Description = e.Description,
                Type = e.Type,
                SubjectId = e.SubjectId,
                SubjectName = e.Subject.Name
            })
            .ToListAsync();

        return Ok(exams);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExamReadDto>> GetById(int id)
    {
        var userId = GetCurrentUserId();

        var exam = await _context.Exams
            .Include(e => e.Subject)
            .Where(e => e.Id == id && e.Subject.UserId == userId)
            .Select(e => new ExamReadDto
            {
                Id = e.Id,
                Title = e.Title,
                ExamDate = e.ExamDate,
                Description = e.Description,
                Type = e.Type,
                SubjectId = e.SubjectId,
                SubjectName = e.Subject.Name
            })
            .FirstOrDefaultAsync();

        if (exam == null)
            return NotFound();

        return Ok(exam);
    }

    [HttpPost]
    public async Task<ActionResult<ExamReadDto>> Create(ExamCreateDto dto)
    {
        var userId = GetCurrentUserId();

        var subject = await _context.Subjects
            .FirstOrDefaultAsync(s => s.Id == dto.SubjectId && s.UserId == userId);

        if (subject == null)
            return BadRequest("Subject does not exist or does not belong to current user.");

        var exam = new Exam
        {
            Title = dto.Title,
            ExamDate = dto.ExamDate,
            Description = dto.Description,
            Type = dto.Type,
            SubjectId = dto.SubjectId
        };

        _context.Exams.Add(exam);
        await _context.SaveChangesAsync();

        var result = new ExamReadDto
        {
            Id = exam.Id,
            Title = exam.Title,
            ExamDate = exam.ExamDate,
            Description = exam.Description,
            Type = exam.Type,
            SubjectId = exam.SubjectId,
            SubjectName = subject.Name
        };

        return CreatedAtAction(nameof(GetById), new { id = exam.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ExamUpdateDto dto)
    {
        var userId = GetCurrentUserId();

        var exam = await _context.Exams
            .Include(e => e.Subject)
            .FirstOrDefaultAsync(e => e.Id == id && e.Subject.UserId == userId);

        if (exam == null)
            return NotFound();

        var newSubject = await _context.Subjects
            .FirstOrDefaultAsync(s => s.Id == dto.SubjectId && s.UserId == userId);

        if (newSubject == null)
            return BadRequest("Subject does not exist or does not belong to current user.");

        exam.Title = dto.Title;
        exam.ExamDate = dto.ExamDate;
        exam.Description = dto.Description;
        exam.Type = dto.Type;
        exam.SubjectId = dto.SubjectId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = GetCurrentUserId();

        var exam = await _context.Exams
            .Include(e => e.Subject)
            .FirstOrDefaultAsync(e => e.Id == id && e.Subject.UserId == userId);

        if (exam == null)
            return NotFound();

        _context.Exams.Remove(exam);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}