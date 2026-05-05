using MudBlazor;
using System.Threading.Tasks;

namespace MauiStore.Infrastructure
{
    public interface IClientPreferenceManager : IPreferenceManager
    {
        Task<string> GetClientId();
        Task<bool> ToggleDarkModeAsync();
        Task ChangeFirstVisitAsync(bool isFirstVisit);
    }
}