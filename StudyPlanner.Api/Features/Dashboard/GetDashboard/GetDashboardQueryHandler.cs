using MediatR;
using StudyPlanner.Api.DTOs.Dashboard;
using StudyPlanner.Api.Repositories;

namespace StudyPlanner.Api.Features.Dashboard.GetDashboard;

public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, DashboardDto>
{
    private readonly IDashboardRepository _dashboardRepository;

    public GetDashboardQueryHandler(IDashboardRepository dashboardRepository)
    {
        _dashboardRepository = dashboardRepository;
    }

    public async Task<DashboardDto> Handle(
        GetDashboardQuery request,
        CancellationToken cancellationToken)
    {
        return await _dashboardRepository.GetDashboardAsync(request.UserId);
    }
}