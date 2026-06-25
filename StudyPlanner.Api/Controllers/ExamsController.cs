using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyPlanner.Api.DTOs.Exams;

using MediatR;
using StudyPlanner.Api.Features.Exams.GetExams;
using StudyPlanner.Api.Features.Exams.GetExamById;
using StudyPlanner.Api.Features.Exams.GetUpcomingExams;
using StudyPlanner.Api.Features.Exams.CreateExam;
using StudyPlanner.Api.Features.Exams.UpdateExam;
using StudyPlanner.Api.Features.Exams.DeleteExam;

namespace StudyPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExamsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExamsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private int GetCurrentUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    [HttpGet]
    public async Task<ActionResult<List<ExamReadDto>>> GetAll()
    {
        var result = await _mediator.Send(
            new GetExamsQuery(GetCurrentUserId()));

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExamReadDto>> GetById(int id)
    {
        var result = await _mediator.Send(
            new GetExamByIdQuery(id, GetCurrentUserId()));

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("upcoming")]
    public async Task<ActionResult<List<ExamReadDto>>> GetUpcoming()
    {
        var result = await _mediator.Send(
            new GetUpcomingExamsQuery(GetCurrentUserId()));

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ExamReadDto>> Create(ExamCreateDto dto)
    {
        var command = new CreateExamCommand(
            dto.Title,
            dto.ExamDate,
            dto.Description,
            dto.Type,
            dto.SubjectId,
            GetCurrentUserId());

        var result = await _mediator.Send(command);

        if (result == null)
            return BadRequest("Subject does not exist or does not belong to current user.");

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ExamUpdateDto dto)
    {
        var command = new UpdateExamCommand(
            id,
            dto.Title,
            dto.ExamDate,
            dto.Description,
            dto.Type,
            dto.SubjectId,
            GetCurrentUserId());

        var success = await _mediator.Send(command);

        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteExamCommand(
            id,
            GetCurrentUserId());

        var success = await _mediator.Send(command);

        if (!success)
            return NotFound();

        return NoContent();
    }
}