using Microsoft.AspNetCore.Identity;

namespace NoteNet.Models;
public class User : IdentityUser
{
public string Username { get; set; } = string.Empty;
}

