using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteNet.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;


[Authorize]
public class PagesController : Controller
{
  private readonly AppDbContext _context;

  public PagesController(AppDbContext context)
  {
    _context = context;
  }

  public IActionResult Create()
  {
    return View();
  }

  public async Task<IActionResult> Edit(int? id)
  {
    if (id == null)
    {
      return NotFound();
    }

    var page = await _context.Pages.FindAsync(id);
    if (page == null || page.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
    {
      return NotFound();
    }

    return View(page);
  }
  public async Task<IActionResult> Index()
  {
    var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
    var pages = await _context.Pages
        .Where(p => p.UserId == userId)
        .ToListAsync();
    return View(pages);
  }

  public async Task<IActionResult> Details(int? id)
  {
    if (id == null)
    {
      return NotFound();
    }

    var page = await _context.Pages
        .FirstOrDefaultAsync(m => m.Id == id);
    if (page == null || page.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
    {
      return NotFound();
    }

    return View(page);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(Page page)
  {
    if (ModelState.IsValid)
    {
      var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
      if (userId == null)
      {
        return Unauthorized();
      }
      page.UserId = userId;
      page.User = await _context.Users.FindAsync(userId);

      _context.Add(page);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
    return View(page);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, Page page)
  {
    if (id != page.Id)
    {
      return NotFound();
    }

    if (ModelState.IsValid)
    {
      try
      {
        var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
          return Unauthorized();
        }
        page.UserId = userId;
        page.User = await _context.Users.FindAsync(userId);

        _context.Update(page);
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PageExists(page.Id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }
      return RedirectToAction(nameof(Index));
    }
    return View(page);
  }

  [HttpPost, ActionName("Delete")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteConfirmed(int id)
  {
    var page = await _context.Pages.FindAsync(id);
    if (page != null)
    {
      _context.Pages.Remove(page);
      await _context.SaveChangesAsync();
    }
    return RedirectToAction(nameof(Index));
  }

  private bool PageExists(int id)
  {
    return _context.Pages.Any(e => e.Id == id);
  }
}