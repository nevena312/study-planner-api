using MediatR;
using StudyPlanner.Api.DTOs.StudyPlans;

namespace StudyPlanner.Api.Features.StudyPlans.GetStudyPlanById;

public record GetStudyPlanByIdQuery(int Id, int UserId)
    : IRequest<StudyPlanReadDto?>;