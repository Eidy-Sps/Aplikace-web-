using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WEB.Data;
using WEB.Models;

namespace WEB.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        private readonly AppDbContext _context;

        public NotesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var notes = _context.Notes
                .Where(n => n.UserId == userId.Value)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();

            return View(notes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string title, string content)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
            {
                ViewBag.Error = "Vyplňte název i text poznámky.";
                return View();
            }

            var note = new Note
            {
                Title = title.Trim(),
                Content = content.Trim(),
                CreatedAt = DateTime.Now,
                UserId = userId.Value
            };

            _context.Notes.Add(note);
            _context.SaveChanges();

            TempData["Success"] = "Poznámka byla úspěšně uložena.";
            return RedirectToAction("Index");
        }

        private int? GetUserId()
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(userIdValue, out int userId))
            {
                return userId;
            }

            return null;
        }
    }
}