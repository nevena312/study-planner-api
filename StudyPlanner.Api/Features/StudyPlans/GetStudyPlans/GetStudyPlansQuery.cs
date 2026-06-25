using MediatR;
using StudyPlanner.Api.DTOs.StudyPlans;

namespace StudyPlanner.Api.Features.StudyPlans.GetStudyPlans;

public record GetStudyPlansQuery(int UserId)
    : IRequest<List<StudyPlanReadDto>>;