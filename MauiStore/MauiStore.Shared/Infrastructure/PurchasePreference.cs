namespace MauiStore.Infrastructure
{
    public class PurchasePreference
    {
        public Guid? SelecetdStoreId { get; set; }
        public string? SelectedStoreName { get; set; }
        public List<Guid> WishlistProductIds { get; set; } = new();
    }
}
