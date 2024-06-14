using System.ComponentModel.DataAnnotations;

namespace NoteNet.ViewModels;
public class RegisterViewModel
{
    [Required]
    [Display(Name = "Username")]
    public required string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public required string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public required string ConfirmPassword { get; set; }

    [EmailAddress]
    [Display(Name = "Email (optional)")]
    public string? Email { get; set; }
}
