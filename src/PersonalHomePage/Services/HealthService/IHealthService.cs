using System.Threading.Tasks;
using PersonalHomePage.Services.HealthService.Model;
using PersonalHomePage.Services.HealthService.Model.Requests;
using PersonalHomePage.Services.HealthService.Model.Responses;

namespace PersonalHomePage.Services.HealthService
{
    public interface IHealthService
    {
        Task<SummariesResponse> GetTodaysSummaryAsync();
        Task<Profile> GetProfileAsync();
        Task<ActivitiesResponse> GetActivitiesAsync(ActivitiesRequest request = null);
    }
}
