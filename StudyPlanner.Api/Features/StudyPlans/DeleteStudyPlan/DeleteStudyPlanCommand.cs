using MediatR;

namespace StudyPlanner.Api.Features.StudyPlans.DeleteStudyPlan;

public record DeleteStudyPlanCommand(int Id, int UserId) : IRequest<bool>;