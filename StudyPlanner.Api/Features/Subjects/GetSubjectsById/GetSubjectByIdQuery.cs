using MediatR;
using StudyPlanner.Api.DTOs.Subjects;

namespace StudyPlanner.Api.Features.Subjects.GetSubjectById;

public record GetSubjectByIdQuery(int Id, int UserId) : IRequest<SubjectReadDto?>;