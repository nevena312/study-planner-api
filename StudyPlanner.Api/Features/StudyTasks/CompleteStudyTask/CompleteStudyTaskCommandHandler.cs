using MediatR;
using StudyPlanner.Api.Enums;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.StudyTasks.CompleteStudyTask;

public class CompleteStudyTaskCommandHandler
    : IRequestHandler<CompleteStudyTaskCommand, bool>
{
    private readonly IStudyTaskRepository _taskRepository;

    public CompleteStudyTaskCommandHandler(IStudyTaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<bool> Handle(
        CompleteStudyTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id, request.UserId);

        if (task == null)
            return false;

        task.Status = StudyTaskStatus.Completed;

        await _taskRepository.SaveChangesAsync();

        return true;
    }
}