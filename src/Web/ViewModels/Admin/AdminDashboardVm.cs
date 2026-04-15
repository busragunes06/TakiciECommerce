namespace Web.ViewModels.Admin;

public class AdminDashboardVm
{
    public int ProductCount { get; set; }
    public int OrderCount { get; set; }
    public int UserCount { get; set; }
    public int CategoryCount { get; set; }
    public int LowStockProductCount { get; set; }
    public decimal TotalRevenue { get; set; }
    public List<AdminRecentOrderVm> RecentOrders { get; set; } = new();
}
