namespace Web.ViewModels.Admin;

public class AdminUserOrderSummaryVm
{
    public Guid Id { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public string Status { get; set; } = "";
    public decimal TotalAmount { get; set; }
    public int ItemCount { get; set; }
}
