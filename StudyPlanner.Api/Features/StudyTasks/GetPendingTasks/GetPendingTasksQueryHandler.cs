using MediatR;
using StudyPlanner.Api.DTOs.StudyTasks;
using StudyPlanner.Api.Mappings;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.StudyTasks.GetPendingTasks;

public class GetPendingTasksQueryHandler
    : IRequestHandler<GetPendingTasksQuery, List<StudyTaskReadDto>>
{
    private readonly IStudyTaskRepository _repository;

    public GetPendingTasksQueryHandler(IStudyTaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<StudyTaskReadDto>> Handle(
        GetPendingTasksQuery request,
        CancellationToken cancellationToken)
    {
        var tasks =
            await _repository.GetPendingByUserIdAsync(request.UserId);

        return tasks.ToDtoList();
    }
}