using MediatR;
using StudyPlanner.Api.DTOs.StudyTasks;
using StudyPlanner.Api.Mappings;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.StudyTasks.GetStudyTaskById;

public class GetStudyTaskByIdQueryHandler
    : IRequestHandler<GetStudyTaskByIdQuery, StudyTaskReadDto?>
{
    private readonly IStudyTaskRepository _repository;

    public GetStudyTaskByIdQueryHandler(IStudyTaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<StudyTaskReadDto?> Handle(
        GetStudyTaskByIdQuery request,
        CancellationToken cancellationToken)
    {
        var task = await _repository.GetByIdAsync(
            request.Id,
            request.UserId);

        return task?.ToDto();
    }
}