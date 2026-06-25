using MediatR;
using StudyPlanner.Api.DTOs.StudyPlans;

namespace StudyPlanner.Api.Features.StudyPlans.GenerateStudyPlan;

public record GenerateStudyPlanCommand(
    string Title,
    DateTime StartDate,
    DateTime EndDate,
    string? Description,
    int UserId)
    : IRequest<StudyPlanReadDto?>;