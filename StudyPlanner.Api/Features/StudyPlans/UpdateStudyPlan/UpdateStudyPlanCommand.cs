using MediatR;

namespace StudyPlanner.Api.Features.StudyPlans.UpdateStudyPlan;

public record UpdateStudyPlanCommand(
    int Id,
    string Title,
    DateTime StartDate,
    DateTime EndDate,
    string? Description,
    int UserId)
    : IRequest<bool>;