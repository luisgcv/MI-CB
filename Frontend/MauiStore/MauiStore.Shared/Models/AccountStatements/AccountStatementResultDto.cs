namespace MauiStore.Shared.Models.AccountStatements;

public class AccountStatementResultDto
{
    public decimal TotalDebt { get; set; }
    public List<AccountStatementSummaryDto> Documents { get; set; } = new();
}