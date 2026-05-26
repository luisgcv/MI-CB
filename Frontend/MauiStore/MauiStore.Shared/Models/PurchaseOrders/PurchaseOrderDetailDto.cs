namespace MauiStore.Shared.Models.PurchaseOrders;

/// <summary>Detalle completo de una orden incluyendo sus líneas.</summary>
public sealed class PurchaseOrderDetailDto : PurchaseOrderSummaryDto
{
    public List<PurchaseOrderLineDto> Lines { get; set; } = new();
}

public sealed class PurchaseOrderLineDto
{
    public string Sku { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public decimal QuantityOrdered { get; set; }
    public decimal QuantityReceived { get; set; }
    public decimal QuantityReturned { get; set; }
    public decimal QuantityBackorder { get; set; }
}