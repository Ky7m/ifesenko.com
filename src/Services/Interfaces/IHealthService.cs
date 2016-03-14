using System.Threading.Tasks;
using ifesenko.com.Services.Implementation.HealthService.Model;

namespace ifesenko.com.Services.Interfaces
{
    public interface IHealthService
    {
        Task<Summary> GetTodaysSummaryAsync();
        Task<SleepActivity> GetTodaysSleepActivityAsync();
    }
}
