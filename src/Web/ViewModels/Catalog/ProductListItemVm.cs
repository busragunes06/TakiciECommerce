namespace Web.ViewModels.Catalog;

public class ProductListItemVm
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string? ImageUrl { get; set; }
    public string CategoryName { get; set; } = "";
    public Guid CategoryId { get; set; }
}

