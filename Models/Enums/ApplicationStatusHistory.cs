namespace JobApplicationTracker.Models.Enums
{
    public class ApplicationStatusHistory
    {
        public int Id { get; set; }

        public int JobApplicationId { get; set; }

        public string Status { get; set; }

        public DateTime ChangedDate { get; set; }

        public JobApplication JobApplication { get; set; }
    }
}
