using JobApplicationTracker.Models;
using JobApplicationTracker.Models.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<JobApplication> JobApplications { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationStatusHistory> ApplicationStatusHistories { get; set; }
    }
}
