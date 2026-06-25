using StudyPlanner.Api.DTOs.Dashboard;

namespace StudyPlanner.Api.Repositories;

public interface IDashboardRepository
{
    Task<DashboardDto> GetDashboardAsync(int userId);
}