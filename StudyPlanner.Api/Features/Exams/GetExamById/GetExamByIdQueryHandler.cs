using MediatR;
using StudyPlanner.Api.DTOs.Exams;
using StudyPlanner.Api.Mappings;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.Exams.GetExamById;

public class GetExamByIdQueryHandler : IRequestHandler<GetExamByIdQuery, ExamReadDto?>
{
    private readonly IExamRepository _examRepository;

    public GetExamByIdQueryHandler(IExamRepository examRepository)
    {
        _examRepository = examRepository;
    }

    public async Task<ExamReadDto?> Handle(GetExamByIdQuery request, CancellationToken cancellationToken)
    {
        var exam = await _examRepository.GetByIdAsync(request.Id, request.UserId);
        return exam?.ToDto();
    }
}

