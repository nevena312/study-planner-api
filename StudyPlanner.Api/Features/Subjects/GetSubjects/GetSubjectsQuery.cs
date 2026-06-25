using MediatR;
using StudyPlanner.Api.DTOs.Subjects;

namespace StudyPlanner.Api.Features.Subjects.GetSubjects;

public record GetSubjectsQuery(int UserId) : IRequest<List<SubjectReadDto>>;