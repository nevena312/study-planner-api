using MediatR;
using StudyPlanner.Api.DTOs.StudyPlans;
using StudyPlanner.Api.Mappings;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.StudyPlans.GetStudyPlanById;

public class GetStudyPlanByIdQueryHandler
    : IRequestHandler<GetStudyPlanByIdQuery, StudyPlanReadDto?>
{
    private readonly IStudyPlanRepository _repository;

    public GetStudyPlanByIdQueryHandler(IStudyPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task<StudyPlanReadDto?> Handle(
        GetStudyPlanByIdQuery request,
        CancellationToken cancellationToken)
    {
        var plan = await _repository.GetByIdAsync(request.Id, request.UserId);

        return plan?.ToDto();
    }
}