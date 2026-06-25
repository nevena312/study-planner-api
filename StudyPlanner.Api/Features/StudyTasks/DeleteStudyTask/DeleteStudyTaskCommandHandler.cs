using MediatR;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.StudyTasks.DeleteStudyTask;

public class DeleteStudyTaskCommandHandler
    : IRequestHandler<DeleteStudyTaskCommand, bool>
{
    private readonly IStudyTaskRepository _taskRepository;

    public DeleteStudyTaskCommandHandler(IStudyTaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<bool> Handle(
        DeleteStudyTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id, request.UserId);

        if (task == null)
            return false;

        _taskRepository.Delete(task);
        await _taskRepository.SaveChangesAsync();

        return true;
    }
}