using MediatR;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.StudyPlans.DeleteStudyPlan;

public class DeleteStudyPlanCommandHandler
    : IRequestHandler<DeleteStudyPlanCommand, bool>
{
    private readonly IStudyPlanRepository _repository;

    public DeleteStudyPlanCommandHandler(IStudyPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        DeleteStudyPlanCommand request,
        CancellationToken cancellationToken)
    {
        var plan = await _repository.GetByIdAsync(request.Id, request.UserId);

        if (plan == null)
            return false;

        _repository.Delete(plan);
        await _repository.SaveChangesAsync();

        return true;
    }
}