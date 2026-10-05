using System;

namespace HIT_Campus_Housing_Portal.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string RegNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } // 'M' or 'F'
        public string NationalID { get; set; }
        public int Part { get; set; }
        public int DeptId { get; set; }
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public string SchoolCode { get; set; }
        public string SchoolName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string HITMail { get; set; }
        public string Address { get; set; }
        public string GuardianName { get; set; }
        public string GuardianPhone { get; set; }
        public string GuardianEmail { get; set; }
        public string GuardianRelationship { get; set; } // 'P' or 'G'

        public string FullName => $"{FirstName} {LastName}";
    }
}
