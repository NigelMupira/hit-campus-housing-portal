using System;

namespace HIT_Campus_Housing_Portal.Models
{
    public class Application
    {
        public int ApplicationId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string RegNumber { get; set; }
        public int HostelId { get; set; }
        public string HostelName { get; set; }
        public string PreferredRoom1 { get; set; }
        public string PreferredRoom2 { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; } // "Pending", "Approved", "Rejected"
        public string AdminRemarks { get; set; }
        public string AssignedRoomNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
    }
}
