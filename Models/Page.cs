// Models/Page.cs
namespace NoteNet.Models;
public class Page
{
  public int Id { get; set; }
  public string Title { get; set; } = string.Empty;
  public string Content { get; set; } = string.Empty;
  public string UserId { get; set; } = string.Empty;
  public User? User { get; set; }
}
