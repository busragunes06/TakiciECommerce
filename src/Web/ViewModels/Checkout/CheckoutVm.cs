using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Checkout;

public class CheckoutVm
{
    [Required(ErrorMessage = "Ad Soyad alanı zorunludur.")]
    [Display(Name = "Ad Soyad")]
    public string ContactName { get; set; } = "";

    [Required(ErrorMessage = "Telefon numarası zorunludur.")]
    [Display(Name = "Telefon")]
    public string PhoneNumber { get; set; } = "";

    [Required(ErrorMessage = "Teslimat adresi zorunludur.")]
    [Display(Name = "Açık Adres")]
    public string ShippingAddress { get; set; } = "";

    [Required(ErrorMessage = "Şehir seçimi zorunludur.")]
    [Display(Name = "Şehir")]
    public string City { get; set; } = "";

    [Required]
    [Display(Name = "Ödeme Yöntemi")]
    public string PaymentMethod { get; set; } = "Kredi Kartı";

    public decimal CartTotal { get; set; }
}