using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyPlanner.Api.DTOs.StudyPlans;

using StudyPlanner.Api.Features.StudyPlans.GetStudyPlans;
using StudyPlanner.Api.Features.StudyPlans.GetStudyPlanById;
using StudyPlanner.Api.Features.StudyPlans.CreateStudyPlan;
using StudyPlanner.Api.Features.StudyPlans.UpdateStudyPlan;
using StudyPlanner.Api.Features.StudyPlans.DeleteStudyPlan;
using StudyPlanner.Api.Features.StudyPlans.GenerateStudyPlan;

namespace StudyPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StudyPlansController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudyPlansController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private int GetCurrentUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    [HttpGet]
    public async Task<ActionResult<List<StudyPlanReadDto>>> GetAll()
    {
        var result = await _mediator.Send(
            new GetStudyPlansQuery(GetCurrentUserId()));

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudyPlanReadDto>> GetById(int id)
    {
        var result = await _mediator.Send(
            new GetStudyPlanByIdQuery(id, GetCurrentUserId()));

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<StudyPlanReadDto>> Create(StudyPlanCreateDto dto)
    {
        var command = new CreateStudyPlanCommand(
            dto.Title,
            dto.StartDate,
            dto.EndDate,
            dto.Description,
            GetCurrentUserId());

        var result = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    [HttpPost("generate")]
    public async Task<ActionResult<StudyPlanReadDto>> Generate(StudyPlanGenerateDto dto)
    {
        var command = new GenerateStudyPlanCommand(
            dto.Title,
            dto.StartDate,
            dto.EndDate,
            dto.Description,
            GetCurrentUserId());

        var result = await _mediator.Send(command);

        if (result == null)
            return BadRequest("Study plan could not be generated. Check dates or pending tasks without a study plan.");

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, StudyPlanUpdateDto dto)
    {
        var command = new UpdateStudyPlanCommand(
            id,
            dto.Title,
            dto.StartDate,
            dto.EndDate,
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
        var command = new DeleteStudyPlanCommand(
            id,
            GetCurrentUserId());

        var success = await _mediator.Send(command);

        if (!success)
            return NotFound();

        return NoContent();
    }
}