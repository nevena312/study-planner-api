using MediatR;
using StudyPlanner.Api.DTOs.StudyPlans;
using StudyPlanner.Api.Mappings;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.StudyPlans.GetStudyPlans;

public class GetStudyPlansQueryHandler
    : IRequestHandler<GetStudyPlansQuery, List<StudyPlanReadDto>>
{
    private readonly IStudyPlanRepository _repository;

    public GetStudyPlansQueryHandler(IStudyPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<StudyPlanReadDto>> Handle(
        GetStudyPlansQuery request,
        CancellationToken cancellationToken)
    {
        var plans = await _repository.GetAllByUserIdAsync(request.UserId);

        return plans.ToDtoList();
    }
}