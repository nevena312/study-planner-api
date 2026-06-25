using MediatR;
using StudyPlanner.Api.DTOs.StudyTasks;
using StudyPlanner.Api.Mappings;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.StudyTasks.GetStudyTasks;

public class GetStudyTasksQueryHandler
    : IRequestHandler<GetStudyTasksQuery, List<StudyTaskReadDto>>
{
    private readonly IStudyTaskRepository _repository;

    public GetStudyTasksQueryHandler(IStudyTaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<StudyTaskReadDto>> Handle(
        GetStudyTasksQuery request,
        CancellationToken cancellationToken)
    {
        var tasks = await _repository.GetAllByUserIdAsync(request.UserId);

        return tasks.ToDtoList();
    }
}