using MediatR;
using StudyPlanner.Api.DTOs.StudyPlans;

namespace StudyPlanner.Api.Features.StudyPlans.CreateStudyPlan;

public record CreateStudyPlanCommand(
    string Title,
    DateTime StartDate,
    DateTime EndDate,
    string? Description,
    int UserId)
    : IRequest<StudyPlanReadDto>;