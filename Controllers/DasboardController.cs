using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobApplicationTracker.Data;
using System.Globalization;

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
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        var applications = _context.JobApplications
            .Where(a => a.UserId == userId)
            .ToList();

        int total = applications.Count();

        ViewBag.Total = total;
        ViewBag.Applied = applications.Count(a => a.Status == "Applied");
        ViewBag.Interview = applications.Count(a => a.Status == "Interview");
        ViewBag.Offer = applications.Count(a => a.Status == "Offer");
        ViewBag.Rejected = applications.Count(a => a.Status == "Rejected");

        // ✅ ADD THIS
        int interviews = ViewBag.Interview;
        int offers = ViewBag.Offer;

        double interviewRate = total > 0 ? (double)interviews / total * 100 : 0;
        double offerRate = total > 0 ? (double)offers / total * 100 : 0;

        ViewBag.InterviewRate = Math.Round(interviewRate, 1);
        ViewBag.OfferRate = Math.Round(offerRate, 1);

        // Charts (unchanged)
        var groupedData = applications
            .GroupBy(a => new { a.DateApplied.Year, a.DateApplied.Month })
            .Select(g => new
            {
                Month = new DateTime(g.Key.Year, g.Key.Month, 1),
                Count = g.Count()
            })
            .OrderBy(x => x.Month)
            .ToList();

        ViewBag.Dates = groupedData
            .Select(x => x.Month.ToString("MMM yyyy"))
            .ToList();

        ViewBag.Counts = groupedData
            .Select(x => x.Count)
            .ToList();

        return View();
    }
}