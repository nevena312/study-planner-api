using MediatR;
using StudyPlanner.Api.DTOs.StudyPlans;
using StudyPlanner.Api.Mappings;
using StudyPlanner.Api.Models;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.StudyPlans.CreateStudyPlan;

public class CreateStudyPlanCommandHandler
    : IRequestHandler<CreateStudyPlanCommand, StudyPlanReadDto>
{
    private readonly IStudyPlanRepository _repository;

    public CreateStudyPlanCommandHandler(IStudyPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task<StudyPlanReadDto> Handle(
        CreateStudyPlanCommand request,
        CancellationToken cancellationToken)
    {
        var plan = new StudyPlan
        {
            Title = request.Title,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
            UserId = request.UserId
        };

        var created = await _repository.AddAsync(plan);

        return created.ToDto();
    }
}