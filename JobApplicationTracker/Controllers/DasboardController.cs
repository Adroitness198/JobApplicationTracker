using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobApplicationTracker.Data;

[Authorize]
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public DashboardController(
        ApplicationDbContext context,
        UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        if (userId == null) return Challenge();

        var apps = _context.JobApplications.Where(a => a.UserId == userId);

        ViewBag.Total = await apps.CountAsync();
        ViewBag.Applied = await apps.CountAsync(a => a.Status == "Applied");
        ViewBag.Interview = await apps.CountAsync(a => a.Status == "Interview");
        ViewBag.Rejected = await apps.CountAsync(a => a.Status == "Rejected");
        ViewBag.Offer = await apps.CountAsync(a => a.Status == "Offer");

        return View();
    }
}
