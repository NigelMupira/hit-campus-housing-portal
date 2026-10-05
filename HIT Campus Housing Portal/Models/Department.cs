using System;

namespace HIT_Campus_Housing_Portal.Models
{
    public class Department
    {
        public int DeptId { get; set; }
        public int SchoolId { get; set; }
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public string SchoolCode { get; set; }
        public string SchoolName { get; set; }
    }

    public class School
    {
        public int SchoolId { get; set; }
        public string SchoolCode { get; set; }
        public string SchoolName { get; set; }
    }
}
