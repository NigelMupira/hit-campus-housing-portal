using HIT_Campus_Housing_Portal.Models;

namespace HIT_Campus_Housing_Portal.Services
{
    public static class UserSession
    {
        public static User CurrentUser { get; set; }
        public static Student CurrentStudent { get; set; }

        public static void Clear()
        {
            CurrentUser = null;
            CurrentStudent = null;
        }
    }
}
