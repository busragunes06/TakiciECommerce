namespace Web.ViewModels.Admin;

public class AdminRecentOrderVm
{
    public Guid Id { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public string UserId { get; set; } = "";
    public string? UserEmail { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "";
}
