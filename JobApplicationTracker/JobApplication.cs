using System.ComponentModel.DataAnnotations;
using System;
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

        // Link each application to a logged-in user
        [Required]
        public required string UserId { get; set; } = null!;
        }
    }



