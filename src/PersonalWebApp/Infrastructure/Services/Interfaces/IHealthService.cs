using System.Threading.Tasks;
using PersonalWebApp.Infrastructure.Services.Implementation.HealthService.Model;

namespace PersonalWebApp.Infrastructure.Services.Interfaces
{
    public interface IHealthService
    {
        Task<Summary> GetTodaysSummaryAsync();
        Task<SleepActivity> GetTodaysSleepActivityAsync();
    }
}
