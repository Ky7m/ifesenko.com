using System.Threading.Tasks;
using PersonalHomePage.Services.Implementation.HealthService.Model;
using PersonalHomePage.Services.Implementation.HealthService.Model.Requests;
using PersonalHomePage.Services.Implementation.HealthService.Model.Responses;

namespace PersonalHomePage.Services.Interfaces
{
    public interface IHealthService
    {
        Task<SummariesResponse> GetTodaysSummaryAsync();
        Task<Profile> GetProfileAsync();
        Task<ActivitiesResponse> GetActivitiesAsync(ActivitiesRequest request = null);
    }
}
