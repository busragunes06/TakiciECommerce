using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string UserId { get; set; } = "";

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    [Required, MaxLength(30)]
    public string Status { get; set; } = "Hazirlaniyor";

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    [Required, MaxLength(100)]
    public string ContactName { get; set; } = "";

    [Required, MaxLength(20)]
    public string PhoneNumber { get; set; } = "";

    [Required, MaxLength(500)]
    public string ShippingAddress { get; set; } = "";

    [Required, MaxLength(50)]
    public string City { get; set; } = "";

    [Required, MaxLength(50)]
    public string PaymentMethod { get; set; } = "";

    // Sadece bir tane Items listesi bıraktık
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}