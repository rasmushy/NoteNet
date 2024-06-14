using System.ComponentModel.DataAnnotations;

namespace NoteNet.ViewModels;
public class AccountSettingsViewModel
{
  public string Username { get; set; } = string.Empty;

  [EmailAddress]
  public string? Email { get; set; }

  [DataType(DataType.Password)]
  public string? NewPassword { get; set; }

  [DataType(DataType.Password)]
  [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
  public string? ConfirmPassword { get; set; }
}
