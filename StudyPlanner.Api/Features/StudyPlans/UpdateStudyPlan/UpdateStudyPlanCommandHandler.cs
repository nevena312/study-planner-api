using MediatR;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.StudyPlans.UpdateStudyPlan;

public class UpdateStudyPlanCommandHandler
    : IRequestHandler<UpdateStudyPlanCommand, bool>
{
    private readonly IStudyPlanRepository _repository;

    public UpdateStudyPlanCommandHandler(IStudyPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        UpdateStudyPlanCommand request,
        CancellationToken cancellationToken)
    {
        var plan = await _repository.GetByIdAsync(request.Id, request.UserId);

        if (plan == null)
            return false;

        plan.Title = request.Title;
        plan.StartDate = request.StartDate;
        plan.EndDate = request.EndDate;
        plan.Description = request.Description;

        await _repository.SaveChangesAsync();

        return true;
    }
}