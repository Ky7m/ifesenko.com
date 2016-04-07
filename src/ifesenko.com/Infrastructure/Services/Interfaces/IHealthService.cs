using System.Threading.Tasks;
using ifesenko.com.Infrastructure.Services.Implementation.HealthService.Model;

namespace ifesenko.com.Infrastructure.Services.Interfaces
{
    public interface IHealthService
    {
        Task<Summary> GetTodaysSummaryAsync();
        Task<SleepActivity> GetTodaysSleepActivityAsync();
    }
}
