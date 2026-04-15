namespace Web.ViewModels.Catalog;

public class ProductDetailsVm
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public bool IsActive { get; set; }
    public string? ImageUrl { get; set; }

    public string CategoryName { get; set; } = "";
    public Guid CategoryId { get; set; }
}

