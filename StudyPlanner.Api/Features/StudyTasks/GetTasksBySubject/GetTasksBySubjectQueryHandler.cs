using MediatR;
using StudyPlanner.Api.DTOs.StudyTasks;
using StudyPlanner.Api.Mappings;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.StudyTasks.GetTasksBySubject;

public class GetTasksBySubjectQueryHandler
    : IRequestHandler<GetTasksBySubjectQuery, List<StudyTaskReadDto>>
{
    private readonly IStudyTaskRepository _repository;

    public GetTasksBySubjectQueryHandler(IStudyTaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<StudyTaskReadDto>> Handle(
        GetTasksBySubjectQuery request,
        CancellationToken cancellationToken)
    {
        var tasks =
            await _repository.GetBySubjectAsync(
                request.SubjectId,
                request.UserId);

        return tasks.ToDtoList();
    }
}