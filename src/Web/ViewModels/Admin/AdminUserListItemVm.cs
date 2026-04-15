namespace Web.ViewModels.Admin;

public class AdminUserListItemVm
{
    public string Id { get; set; } = "";
    public string Email { get; set; } = "";
    public string? PhoneNumber { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool IsAdmin { get; set; }
    public int OrderCount { get; set; }
    public decimal TotalSpent { get; set; }
    public DateTime? LastOrderUtc { get; set; }
}
