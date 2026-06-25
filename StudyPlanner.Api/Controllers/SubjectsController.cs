using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyPlanner.Api.Data;
using StudyPlanner.Api.DTOs.Subjects;
using StudyPlanner.Api.Models;

using MediatR;
using StudyPlanner.Api.Features.Subjects.GetSubjects;
using StudyPlanner.Api.Features.Subjects.GetSubjectById;
using StudyPlanner.Api.Features.Subjects.CreateSubject;
using StudyPlanner.Api.Features.Subjects.UpdateSubject;
using StudyPlanner.Api.Features.Subjects.DeleteSubject;

namespace StudyPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SubjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private int GetCurrentUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    [HttpGet]
    public async Task<ActionResult<List<SubjectReadDto>>> GetAll()
    {
        var result = await _mediator.Send(
            new GetSubjectsQuery(GetCurrentUserId()));

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SubjectReadDto>> GetById(int id)
    {
        var result = await _mediator.Send(
            new GetSubjectByIdQuery(id, GetCurrentUserId()));

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<SubjectReadDto>> Create(SubjectCreateDto dto)
    {
        var command = new CreateSubjectCommand(
            dto.Name,
            dto.Description,
            GetCurrentUserId());

        var result = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, SubjectUpdateDto dto)
    {
        var command = new UpdateSubjectCommand(
            id,
            dto.Name,
            dto.Description,
            GetCurrentUserId());

        var success = await _mediator.Send(command);

        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteSubjectCommand(
            id,
            GetCurrentUserId());

        var success = await _mediator.Send(command);

        if (!success)
            return NotFound();

        return NoContent();
    }
}