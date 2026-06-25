using MediatR;
using StudyPlanner.Api.DTOs.Subjects;
using StudyPlanner.Api.Mappings;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.Subjects.GetSubjects;

public class GetSubjectsQueryHandler : IRequestHandler<GetSubjectsQuery, List<SubjectReadDto>>
{
    private readonly ISubjectRepository _subjectRepository;

    public GetSubjectsQueryHandler(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<List<SubjectReadDto>> Handle(GetSubjectsQuery request, CancellationToken cancellationToken)
    {
        var subjects = await _subjectRepository.GetAllByUserIdAsync(request.UserId);

        return subjects.ToDtoList();
    }
}