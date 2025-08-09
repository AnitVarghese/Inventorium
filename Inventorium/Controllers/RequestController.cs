using Inventorium.Data;
using Inventorium.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventorium.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public RequestController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Request/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Request/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Request request)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Unauthorized();
                }

                request.UserId = user.Id;
                request.UserEmail = user.Email;
                request.RequestedBy = user.UserName;
                request.RequestedAt = DateTime.UtcNow;

                _context.Requests.Add(request);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "✅ Request submitted successfully.";
                return RedirectToAction("Index", "Inventory");
            }

            return View(request);
        }

        // Admin-only inbox
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Inbox()
        {
            var requests = await _context.Requests
                .OrderByDescending(r => r.RequestedAt)
                .ToListAsync();

            return View(requests);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminInbox()
        {
            var allRequests = await _context.Requests
                .OrderByDescending(r => r.RequestedAt)
                .ToListAsync();

            return View(allRequests);
        }
    }
}
