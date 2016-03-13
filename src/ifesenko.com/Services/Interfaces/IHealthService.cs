using System.Threading.Tasks;
using IfesenkoDotCom.Services.Implementation.HealthService.Model;

namespace IfesenkoDotCom.Services.Interfaces
{
    public interface IHealthService
    {
        Task<Summary> GetTodaysSummaryAsync();
        Task<SleepActivity> GetTodaysSleepActivityAsync();
    }
}
