using JobApplicationTracker;
using JobApplicationTracker.Data;
using JobApplicationTracker.Models;
using JobApplicationTracker.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using X.PagedList;
using X.PagedList.Extensions;


namespace JobApplicationTracker.Controllers
{
    [Authorize]
    public class JobApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public JobApplicationsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: JobApplications
        public async Task<IActionResult> Index(string searchString, string statusFilter, int? page)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applications = _context.JobApplications
                .Where(a=> a.UserId==userId)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
                applications = applications.Where(a => a.CompanyName.Contains(searchString));

            if (!string.IsNullOrEmpty(statusFilter))
                applications = applications.Where(a => a.Status == statusFilter);

            var pagedList = applications.OrderByDescending(a => a.DateApplied)
                                        .ToPagedList(page ?? 1, 6); // page size = 6

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentStatusFilter"] = statusFilter;

            return View(pagedList);
        }



        // GET: JobApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        { 

            if (id == null)
            {
                return NotFound();
            }
            var userId = _userManager.GetUserId(User);
            var jobApplication = await _context.JobApplications
     .Include(j => j.ApplicationHistories)
     .FirstOrDefaultAsync(m => m.Id == id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            return View(jobApplication);
        }

        // GET: JobApplications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobApplication jobApplication, IFormFile ResumeFile)
        {
            if (ModelState.IsValid)
            {
                
                jobApplication.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // replace later with real logged-in user

                if (ResumeFile != null && ResumeFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/resumes");

                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ResumeFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ResumeFile.CopyToAsync(stream);
                    }

                    jobApplication.ResumePath = "/resumes/" + fileName;
                }

                _context.Add(jobApplication);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(jobApplication);
        }

        // GET: JobApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var userId = _userManager.GetUserId(User);
            var jobApplication = await _context.JobApplications
                .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
            if (jobApplication == null)
            {
                return NotFound();
            }
            return View(jobApplication);
        }

        // POST: JobApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, JobApplication jobApplication, IFormFile? ResumeFile, bool RemoveCV = false)
{
    if (id != jobApplication.Id)
        return NotFound();

    var existingApp = await _context.JobApplications.FindAsync(id);
    if (existingApp == null)
        return NotFound();

    if (ModelState.IsValid)
    {
       
        jobApplication.UserId = existingApp.UserId;

        
        if (existingApp.Status != jobApplication.Status)
        {
            var history = new ApplicationStatusHistory
            {
                Status = jobApplication.Status,
                ChangedDate = DateTime.Now,
                JobApplicationId = existingApp.Id
            };

            _context.ApplicationStatusHistories.Add(history);
        }

       
        existingApp.CompanyName = jobApplication.CompanyName;
        existingApp.Role = jobApplication.Role;
        existingApp.Status = jobApplication.Status;
        existingApp.DateApplied = jobApplication.DateApplied;
        existingApp.InterviewDate = jobApplication.InterviewDate;
        existingApp.Notes = jobApplication.Notes;

        string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/resumes");

        // Remove CV
        if (RemoveCV && !string.IsNullOrEmpty(existingApp.ResumePath))
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingApp.ResumePath.TrimStart('/'));
            if (System.IO.File.Exists(fullPath))
                System.IO.File.Delete(fullPath);

            existingApp.ResumePath = null;
        }

        // Upload new CV
        if (ResumeFile != null && ResumeFile.Length > 0)
        {
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ResumeFile.FileName);
            string filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ResumeFile.CopyToAsync(stream);
            }

            existingApp.ResumePath = "/resumes/" + fileName;
        }

        _context.Update(existingApp);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    return View(jobApplication);
}
        // GET: JobApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = _userManager.GetUserId(User);
            var jobApplication = await _context.JobApplications
                .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
            if (jobApplication == null)
            {
                return NotFound();
            }

            return View(jobApplication);
        }

        // POST: JobApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobApplication = await _context.JobApplications.FindAsync(id);
            if (jobApplication != null)
            {
                _context.JobApplications.Remove(jobApplication);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobApplicationExists(int id)
        {
            return _context.JobApplications.Any(e => e.Id == id);
        }

        public ApplicationStatus Status { get; set; }
    }
}
