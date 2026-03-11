using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JobApplicationTracker.Models;

namespace JobApplicationTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<JobApplication> JobApplications { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
