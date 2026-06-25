using MediatR;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.StudyTasks.UpdateStudyTask;

public class UpdateStudyTaskCommandHandler
    : IRequestHandler<UpdateStudyTaskCommand, bool>
{
    private readonly IStudyTaskRepository _taskRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IStudyPlanRepository _studyPlanRepository;

    public UpdateStudyTaskCommandHandler(
        IStudyTaskRepository taskRepository,
        ISubjectRepository subjectRepository,
        IStudyPlanRepository studyPlanRepository)
    {
        _taskRepository = taskRepository;
        _subjectRepository = subjectRepository;
        _studyPlanRepository = studyPlanRepository;
    }

    public async Task<bool> Handle(
        UpdateStudyTaskCommand request,
        CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id, request.UserId);

        if (task == null)
            return false;

        var subject = await _subjectRepository.GetByIdAsync(request.SubjectId, request.UserId);

        if (subject == null)
            return false;

        if (request.StudyPlanId != null)
        {
            var studyPlan = await _studyPlanRepository.GetByIdAsync(
                request.StudyPlanId.Value,
                request.UserId);

            if (studyPlan == null)
                return false;
        }

        task.Title = request.Title;
        task.Description = request.Description;
        task.Status = request.Status;
        task.Priority = request.Priority;
        task.Deadline = request.Deadline;
        task.EstimatedDurationMinutes = request.EstimatedDurationMinutes;
        task.SubjectId = request.SubjectId;
        task.StudyPlanId = request.StudyPlanId;

        await _taskRepository.SaveChangesAsync();

        return true;
    }
}