using MediatR;
using StudyPlanner.Api.DTOs.Dashboard;

namespace StudyPlanner.Api.Features.Dashboard.GetDashboard;

public record GetDashboardQuery(int UserId) : IRequest<DashboardDto>;