namespace Web.ViewModels.Catalog;

public class CategoryListItemVm
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    
    public int ProductCount { get; set; }
}

