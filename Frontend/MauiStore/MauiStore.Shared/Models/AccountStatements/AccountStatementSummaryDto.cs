namespace MauiStore.Shared.Models.AccountStatements;

public class AccountStatementSummaryDto
{
    public string Id { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime DocumentDate { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
}