using Blazored.LocalStorage;
using MauiStore.Constants;
using MauiStore.Infrastructure;
using MauiStore.Infrastructure.Interfaces;

namespace MauiStore.Infrastructure
{
    public class PurchasePreferenceManager : IPurchasePreferenceManager
    {
        private readonly ILocalStorageService _localStorageService;

        public PurchasePreferenceManager(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task<List<Guid>> GetWishlist()
        {
            var pref = await GetPurchasePreference();
            return pref?.WishlistProductIds ?? new List<Guid>();
        }

        public async Task<bool> IsInWishlist(Guid productId)
        {
            var pref = await GetPurchasePreference();
            return pref?.WishlistProductIds?.Contains(productId) ?? false;
        }

        public async Task AddToWishlist(Guid productId)
        {
            var pref = await GetPurchasePreference() ?? new PurchasePreference();

            if (!pref.WishlistProductIds.Contains(productId))
            {
                pref.WishlistProductIds.Add(productId);
                await SetPurchasePreference(pref);
            }
        }

        public async Task RemoveFromWishlist(Guid productId)
        {
            var pref = await GetPurchasePreference();
            if (pref == null)
                return;

            if (pref.WishlistProductIds.Contains(productId))
            {
                pref.WishlistProductIds.Remove(productId);
                await SetPurchasePreference(pref);
            }
        }

        public async Task ClearWishlist()
        {
            var pref = await GetPurchasePreference() ?? new PurchasePreference();
            pref.WishlistProductIds = new List<Guid>();
            await SetPurchasePreference(pref);
        }

        public async Task SetPurchasePreference(PurchasePreference preference)
        {
            await _localStorageService.SetItemAsync(StorageConstants.Local.PurchasePreference, preference);
        }

        public async Task<PurchasePreference> GetPurchasePreference()
        {
            return await _localStorageService.GetItemAsync<PurchasePreference>(StorageConstants.Local.PurchasePreference);
        }

        public async Task SetSelectedStore(Guid id, string name)
        {
            await SetPurchasePreference(new PurchasePreference { SelecetdStoreId = id, SelectedStoreName = name });
        }

        public async Task<(Guid id, string Name)?> GetSelectedStore()
        {
            var purchasePreference = await GetPurchasePreference();
            if (purchasePreference == null || !purchasePreference.SelecetdStoreId.HasValue)
            {
                return null;
            }
            else
                return (purchasePreference.SelecetdStoreId.Value, purchasePreference.SelectedStoreName);
        }

        public async Task<IPreference> GetPreference()
        {
            return await _localStorageService.GetItemAsync<ClientPreference>(StorageConstants.Local.PurchasePreference) ?? new ClientPreference();
        }
        public async Task SetPreference(IPreference preference)
        {
            await _localStorageService.SetItemAsync(StorageConstants.Local.PurchasePreference, preference as ClientPreference);
        }
    }
}
