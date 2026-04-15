using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Home;

public class ContactMessageVm
{
    [Required(ErrorMessage = "Lütfen adınızı girin.")]
    [StringLength(100, ErrorMessage = "Adınız en fazla 100 karakter olabilir.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Lütfen E-Posta adresinizi girin.")]
    [EmailAddress(ErrorMessage = "Lütfen geçerli bir e-posta adresi girin.")]
    [StringLength(150)]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Lütfen mesajınızın konusunu girin.")]
    [StringLength(200, ErrorMessage = "Konu en fazla 200 karakter olabilir.")]
    public string Subject { get; set; } = null!;

    [Required(ErrorMessage = "Lütfen mesajınızı girin.")]
    [StringLength(2000, ErrorMessage = "Mesajınız en fazla 2000 karakter olabilir.")]
    public string Body { get; set; } = null!;
}
