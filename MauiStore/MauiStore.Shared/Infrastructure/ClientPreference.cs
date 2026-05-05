using System.Linq;

namespace MauiStore.Infrastructure
{
    public record ClientPreference : IPreference
    {
        public Guid? Id { get; set; }
        public bool IsDarkMode { get; set; } = false;
        public bool IsFirstVisit { get; set; } = true;
    }
}