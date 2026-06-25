using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyPlanner.Api.DTOs.StudyTasks;

using StudyPlanner.Api.Features.StudyTasks.GetStudyTasks;
using StudyPlanner.Api.Features.StudyTasks.GetStudyTaskById;
using StudyPlanner.Api.Features.StudyTasks.GetPendingTasks;
using StudyPlanner.Api.Features.StudyTasks.GetTasksBySubject;
using StudyPlanner.Api.Features.StudyTasks.GetTasksByPlan;
using StudyPlanner.Api.Features.StudyTasks.CreateStudyTask;
using StudyPlanner.Api.Features.StudyTasks.UpdateStudyTask;
using StudyPlanner.Api.Features.StudyTasks.DeleteStudyTask;
using StudyPlanner.Api.Features.StudyTasks.CompleteStudyTask;

namespace StudyPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StudyTasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudyTasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private int GetCurrentUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    [HttpGet]
    public async Task<ActionResult<List<StudyTaskReadDto>>> GetAll()
    {
        var result = await _mediator.Send(
            new GetStudyTasksQuery(GetCurrentUserId()));

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudyTaskReadDto>> GetById(int id)
    {
        var result = await _mediator.Send(
            new GetStudyTaskByIdQuery(id, GetCurrentUserId()));

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("pending")]
    public async Task<ActionResult<List<StudyTaskReadDto>>> GetPending()
    {
        var result = await _mediator.Send(
            new GetPendingTasksQuery(GetCurrentUserId()));

        return Ok(result);
    }

    [HttpGet("by-subject/{subjectId}")]
    public async Task<ActionResult<List<StudyTaskReadDto>>> GetBySubject(int subjectId)
    {
        var result = await _mediator.Send(
            new GetTasksBySubjectQuery(subjectId, GetCurrentUserId()));

        return Ok(result);
    }

    [HttpGet("by-plan/{planId}")]
    public async Task<ActionResult<List<StudyTaskReadDto>>> GetByPlan(int planId)
    {
        var result = await _mediator.Send(
            new GetTasksByPlanQuery(planId, GetCurrentUserId()));

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<StudyTaskReadDto>> Create(StudyTaskCreateDto dto)
    {
        var command = new CreateStudyTaskCommand(
            dto.Title,
            dto.Description,
            dto.Status,
            dto.Priority,
            dto.Deadline,
            dto.EstimatedDurationMinutes,
            dto.SubjectId,
            dto.StudyPlanId,
            GetCurrentUserId());

        var result = await _mediator.Send(command);

        if (result == null)
            return BadRequest("Subject or study plan does not exist or does not belong to current user.");

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, StudyTaskUpdateDto dto)
    {
        var command = new UpdateStudyTaskCommand(
            id,
            dto.Title,
            dto.Description,
            dto.Status,
            dto.Priority,
            dto.Deadline,
            dto.EstimatedDurationMinutes,
            dto.SubjectId,
            dto.StudyPlanId,
            GetCurrentUserId());

        var success = await _mediator.Send(command);

        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpPatch("{id}/complete")]
    public async Task<IActionResult> Complete(int id)
    {
        var command = new CompleteStudyTaskCommand(
            id,
            GetCurrentUserId());

        var success = await _mediator.Send(command);

        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteStudyTaskCommand(
            id,
            GetCurrentUserId());

        var success = await _mediator.Send(command);

        if (!success)
            return NotFound();

        return NoContent();
    }
}