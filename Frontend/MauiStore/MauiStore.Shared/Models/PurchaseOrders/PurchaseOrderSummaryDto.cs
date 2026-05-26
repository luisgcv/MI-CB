namespace MauiStore.Shared.Models.PurchaseOrders;

/// <summary>Resumen de una orden de compra para la lista.</summary>
public class PurchaseOrderSummaryDto
{
    public string Id { get; set; } = string.Empty;
    public string Branch { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? RequiredAt { get; set; }
    public decimal Total { get; set; }
}