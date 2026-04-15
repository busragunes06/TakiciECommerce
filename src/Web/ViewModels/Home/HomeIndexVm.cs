using Web.ViewModels.Catalog;

namespace Web.ViewModels.Home;

public class HomeIndexVm
{
    public IReadOnlyList<ProductListItemVm> Featured { get; set; } = Array.Empty<ProductListItemVm>();
    public IReadOnlyList<ProductListItemVm> Newest { get; set; } = Array.Empty<ProductListItemVm>();
}

