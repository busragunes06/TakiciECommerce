namespace Web.ViewModels.Orders;

public class OrderDetailsVm
{
    public Guid Id { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public string Status { get; set; } = "";
    public decimal TotalAmount { get; set; }

    public string ContactName { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string ShippingAddress { get; set; } = "";
    public string City { get; set; } = "";
    public string PaymentMethod { get; set; } = "";

    public IReadOnlyList<OrderDetailsItemVm> Items { get; set; } = Array.Empty<OrderDetailsItemVm>();
}

public class OrderDetailsItemVm
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal => UnitPrice * Quantity;
}

