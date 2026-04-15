namespace Web.ViewModels.Catalog;

public class ProductListVm
{
    public string? Query { get; set; }
    public string? Category { get; set; }
    public Guid? CategoryId { get; set; }
    public string? Sort { get; set; }

    public IReadOnlyList<ProductListItemVm> Items { get; set; } = Array.Empty<ProductListItemVm>();
}

