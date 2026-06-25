using MediatR;
using StudyPlanner.Api.DTOs.StudyPlans;
using StudyPlanner.Api.Mappings;
using StudyPlanner.Api.Models;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.StudyPlans.GenerateStudyPlan;

public class GenerateStudyPlanCommandHandler
    : IRequestHandler<GenerateStudyPlanCommand, StudyPlanReadDto?>
{
    private readonly IStudyPlanRepository _studyPlanRepository;
    private readonly IStudyTaskRepository _studyTaskRepository;

    public GenerateStudyPlanCommandHandler(
        IStudyPlanRepository studyPlanRepository,
        IStudyTaskRepository studyTaskRepository)
    {
        _studyPlanRepository = studyPlanRepository;
        _studyTaskRepository = studyTaskRepository;
    }

    public async Task<StudyPlanReadDto?> Handle(
        GenerateStudyPlanCommand request,
        CancellationToken cancellationToken)
    {
        if (request.EndDate <= request.StartDate)
            return null;

        var pendingTasks =
            await _studyTaskRepository.GetPendingWithoutPlanByUserIdAsync(request.UserId);

        if (!pendingTasks.Any())
            return null;

        var plan = new StudyPlan
        {
            Title = request.Title,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
            UserId = request.UserId
        };

        var createdPlan = await _studyPlanRepository.AddAsync(plan);

        foreach (var task in pendingTasks)
        {
            task.StudyPlanId = createdPlan.Id;
        }

        await _studyTaskRepository.SaveChangesAsync();

        createdPlan.StudyTasks = pendingTasks;

        return createdPlan.ToDto();
    }
}