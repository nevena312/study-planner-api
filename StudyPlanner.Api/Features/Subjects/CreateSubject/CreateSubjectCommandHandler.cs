using MediatR;
using StudyPlanner.Api.DTOs.Subjects;
using StudyPlanner.Api.Models;
using StudyPlanner.Api.Repositories;
using StudyPlanner.Api.Mappings;

namespace StudyPlanner.Api.Features.Subjects.CreateSubject;

public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, SubjectReadDto>
{
    private readonly ISubjectRepository _subjectRepository;

    public CreateSubjectCommandHandler(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<SubjectReadDto> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
    {
        var subject = new Subject
        {
            Name = request.Name,
            Description = request.Description,
            UserId = request.UserId
        };

        var created = await _subjectRepository.AddAsync(subject);

        return created.ToDto();
    }
}