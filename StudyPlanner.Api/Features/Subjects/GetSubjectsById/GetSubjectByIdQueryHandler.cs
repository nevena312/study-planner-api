using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using StudyPlanner.Api.DTOs.Subjects;
using StudyPlanner.Api.Mappings;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.Subjects.GetSubjectById;

public class GetSubjectByIdQueryHandler : IRequestHandler<GetSubjectByIdQuery, SubjectReadDto?>
{
    private readonly ISubjectRepository _subjectRepository;

    public GetSubjectByIdQueryHandler(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<SubjectReadDto?> Handle(GetSubjectByIdQuery request, CancellationToken cancellationToken)
    {
        var subject = await _subjectRepository.GetByIdAsync(request.Id, request.UserId);

        if (subject == null)
            return null;

        return subject.ToDto();
    }
}