namespace Web.ViewModels.Admin;

public class AdminUserDetailsVm
{
    public string Id { get; set; } = "";
    public string Email { get; set; } = "";
    public string? UserName { get; set; }
    public string? PhoneNumber { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool IsAdmin { get; set; }
    public int OrderCount { get; set; }
    public decimal TotalSpent { get; set; }
    public DateTime? LastOrderUtc { get; set; }
    public List<AdminUserOrderSummaryVm> Orders { get; set; } = new();
}
