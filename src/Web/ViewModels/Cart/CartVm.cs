namespace Web.ViewModels.Cart;

public class CartVm
{
    public IReadOnlyList<CartItemVm> Items { get; set; } = Array.Empty<CartItemVm>();
    public decimal Total => Items.Sum(x => x.LineTotal);
}

