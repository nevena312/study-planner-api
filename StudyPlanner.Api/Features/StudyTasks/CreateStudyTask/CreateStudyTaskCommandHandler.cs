using MediatR;
using StudyPlanner.Api.DTOs.StudyTasks;
using StudyPlanner.Api.Mappings;
using StudyPlanner.Api.Models;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.StudyTasks.CreateStudyTask;

public class CreateStudyTaskCommandHandler
    : IRequestHandler<CreateStudyTaskCommand, StudyTaskReadDto?>
{
    private readonly IStudyTaskRepository _taskRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IStudyPlanRepository _studyPlanRepository;

    public CreateStudyTaskCommandHandler(
        IStudyTaskRepository taskRepository,
        ISubjectRepository subjectRepository,
        IStudyPlanRepository studyPlanRepository)
    {
        _taskRepository = taskRepository;
        _subjectRepository = subjectRepository;
        _studyPlanRepository = studyPlanRepository;
    }

    public async Task<StudyTaskReadDto?> Handle(
        CreateStudyTaskCommand request,
        CancellationToken cancellationToken)
    {
        var subject = await _subjectRepository.GetByIdAsync(
            request.SubjectId,
            request.UserId);

        if (subject == null)
            return null;

        StudyPlan? studyPlan = null;

        if (request.StudyPlanId != null)
        {
            studyPlan = await _studyPlanRepository.GetByIdAsync(
                request.StudyPlanId.Value,
                request.UserId);

            if (studyPlan == null)
                return null;
        }

        var task = new StudyTask
        {
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
            Priority = request.Priority,
            Deadline = request.Deadline,
            EstimatedDurationMinutes = request.EstimatedDurationMinutes,
            SubjectId = request.SubjectId,
            StudyPlanId = request.StudyPlanId,
            Subject = subject,
            StudyPlan = studyPlan
        };

        var created = await _taskRepository.AddAsync(task);

        return created.ToDto();
    }
}