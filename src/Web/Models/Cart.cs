namespace Web.Models;

public class Cart
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = "";
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow; // Bu satırı ekledik
    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
    public virtual ICollection<CartItem> Items { get; set; } = new List<CartItem>();
}