using MediatR;
using StudyPlanner.Api.DTOs.Exams;
using StudyPlanner.Api.Mappings;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.Exams.GetExams;

public class GetExamsQueryHandler : IRequestHandler<GetExamsQuery, List<ExamReadDto>>
{
    private readonly IExamRepository _examRepository;

    public GetExamsQueryHandler(IExamRepository examRepository)
    {
        _examRepository = examRepository;
    }

    public async Task<List<ExamReadDto>> Handle(GetExamsQuery request, CancellationToken cancellationToken)
    {
        var exams = await _examRepository.GetAllByUserIdAsync(request.UserId);
        return exams.ToDtoList();
    }
}