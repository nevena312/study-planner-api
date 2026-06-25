using MediatR;
using StudyPlanner.Api.DTOs.StudyTasks;
using StudyPlanner.Api.Mappings;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.StudyTasks.GetTasksByPlan;

public class GetTasksByPlanQueryHandler
    : IRequestHandler<GetTasksByPlanQuery, List<StudyTaskReadDto>>
{
    private readonly IStudyTaskRepository _repository;

    public GetTasksByPlanQueryHandler(IStudyTaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<StudyTaskReadDto>> Handle(
        GetTasksByPlanQuery request,
        CancellationToken cancellationToken)
    {
        var tasks =
            await _repository.GetByPlanAsync(
                request.PlanId,
                request.UserId);

        return tasks.ToDtoList();
    }
}