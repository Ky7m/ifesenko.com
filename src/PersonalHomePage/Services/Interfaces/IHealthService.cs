using System.Threading.Tasks;
using PersonalHomePage.Services.Implementation.HealthService.Model;

namespace PersonalHomePage.Services.Interfaces
{
    public interface IHealthService
    {
        Task<Summary> GetTodaysSummaryAsync();
        Task<Profile> GetProfileAsync();
        Task<SleepActivity> GetTodaysSleepActivityAsync();
    }
}
