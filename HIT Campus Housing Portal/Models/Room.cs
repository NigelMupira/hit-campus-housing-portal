using System;

namespace HIT_Campus_Housing_Portal.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public int HostelId { get; set; }
        public string HostelName { get; set; }
        public string RoomNumber { get; set; }
        public int Capacity { get; set; }
        public int OccupiedCount { get; set; }
        public bool IsAvailable => OccupiedCount < Capacity;
    }
}
