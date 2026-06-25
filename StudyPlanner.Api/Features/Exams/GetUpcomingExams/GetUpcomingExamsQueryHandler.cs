using MediatR;
using StudyPlanner.Api.DTOs.Exams;
using StudyPlanner.Api.Mappings;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.Exams.GetUpcomingExams;

public class GetUpcomingExamsQueryHandler : IRequestHandler<GetUpcomingExamsQuery, List<ExamReadDto>>
{
    private readonly IExamRepository _examRepository;

    public GetUpcomingExamsQueryHandler(IExamRepository examRepository)
    {
        _examRepository = examRepository;
    }

    public async Task<List<ExamReadDto>> Handle(GetUpcomingExamsQuery request, CancellationToken cancellationToken)
    {
        var exams = await _examRepository.GetUpcomingByUserIdAsync(request.UserId);
        return exams.ToDtoList();
    }
}