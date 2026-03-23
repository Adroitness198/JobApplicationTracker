using JobApplicationTracker.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplicationTracker
{
    public class JobApplication
    {

            public int Id { get; set; }

            [Required]
            public required string CompanyName { get; set; }

            [Required]
            public required string Role { get; set; }

            [Required]
            public required string Status { get; set; } // Applied, Interview, Rejected, Offer

            [DataType(DataType.Date)]
            public DateTime DateApplied { get; set; }

            public string? Notes { get; set; }

        public string? ResumePath { get; set; }

        public DateTime? InterviewDate { get; set; }

        // Link each application to a logged-in user
        public string? UserId { get; set; }

        public ICollection<ApplicationStatusHistory> ApplicationHistories { get; set; } = new List<ApplicationStatusHistory>();
    }
    }



