using MediatR;
using StudyPlanner.Api.DTOs.StudyTasks;

namespace StudyPlanner.Api.Features.StudyTasks.GetTasksByPlan;

public record GetTasksByPlanQuery(
    int PlanId,
    int UserId)
    : IRequest<List<StudyTaskReadDto>>;