using Blazored.LocalStorage;
using MauiStore.Constants;
using MauiStore.Infrastructure;

namespace MauiStore.Infrastructure.Interfaces
{
    public interface IPurchasePreferenceManager : IPreferenceManager
    {
        public Task SetSelectedStore(Guid id, string name);
        public Task<(Guid id, string Name)?> GetSelectedStore();
        public Task<List<Guid>> GetWishlist();
        public Task<bool> IsInWishlist(Guid productId);
        public Task AddToWishlist(Guid productId);
        public Task RemoveFromWishlist(Guid productId);
        public Task ClearWishlist();
    }
}
