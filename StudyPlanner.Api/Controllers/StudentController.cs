using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyPlanner.Api.Data;
using StudyPlanner.Api.DTOs.Students;
using StudyPlanner.Api.Models;

namespace StudyPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StudentsController : ControllerBase
{
    private readonly StudyPlannerDbContext _context;

    public StudentsController(StudyPlannerDbContext context)
    {
        _context = context;
    }

    private int GetCurrentUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    [HttpGet]
    public async Task<ActionResult<List<StudentReadDto>>> GetAll()
    {
        var userId = GetCurrentUserId();

        var students = await _context.Students
            .Where(s => s.Id == userId)
            .Select(s => new StudentReadDto
            {
                Id = s.Id,
                Name = s.Name,
                LastName = s.LastName
            })
            .ToListAsync();

        return Ok(students);
    }

    [HttpPost]
    public async Task<ActionResult<StudentReadDto>> Create(StudentCreateDto dto)
    {
        var student = new Student
        {
            Name = dto.Name,
            LastName = dto.LastName
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        var result = new StudentReadDto
        {
            Id = student.Id,
            Name = student.Name,
            LastName = student.LastName
        };

        return CreatedAtAction(nameof(Create), new { id = student.Id }, result);
    }
}