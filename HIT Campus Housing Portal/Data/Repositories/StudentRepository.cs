using System;
using System.Collections.Generic;
using System.Data;
using HIT_Campus_Housing_Portal.Models;
using MySql.Data.MySqlClient;

namespace HIT_Campus_Housing_Portal.Data.Repositories
{
    public class StudentRepository
    {
        public bool CreateStudent(Student student)
        {
            string sql = @"INSERT INTO students 
                (student_id, reg_number, first_name, last_name, dob, gender, national_id, part, dept_id, phone, email, hit_mail, address, guardian_name, guardian_phone, guardian_email, guardian_relationship)
                VALUES 
                (@student_id, @reg_number, @first_name, @last_name, @dob, @gender, @national_id, @part, @dept_id, @phone, @email, @hit_mail, @address, @guardian_name, @guardian_phone, @guardian_email, @guardian_relationship);";

            int rows = DbConnectionFactory.ExecuteNonQuery(sql,
                new MySqlParameter("@student_id", student.StudentId),
                new MySqlParameter("@reg_number", student.RegNumber),
                new MySqlParameter("@first_name", student.FirstName),
                new MySqlParameter("@last_name", student.LastName),
                new MySqlParameter("@dob", student.DateOfBirth),
                new MySqlParameter("@gender", student.Gender),
                new MySqlParameter("@national_id", student.NationalID),
                new MySqlParameter("@part", student.Part),
                new MySqlParameter("@dept_id", student.DeptId),
                new MySqlParameter("@phone", student.Phone),
                new MySqlParameter("@email", student.Email),
                new MySqlParameter("@hit_mail", student.HITMail),
                new MySqlParameter("@address", student.Address),
                new MySqlParameter("@guardian_name", student.GuardianName),
                new MySqlParameter("@guardian_phone", student.GuardianPhone),
                new MySqlParameter("@guardian_email", student.GuardianEmail),
                new MySqlParameter("@guardian_relationship", student.GuardianRelationship));

            return rows > 0;
        }

        public Student GetStudentByUserId(int userId)
        {
            string sql = @"SELECT s.*, d.dept_code, d.dept_name, sc.school_code, sc.school_name
                           FROM students s
                           JOIN departments d ON s.dept_id = d.dept_id
                           JOIN schools sc ON d.school_id = sc.school_id
                           WHERE s.student_id = @userId;";

            DataTable dt = DbConnectionFactory.ExecuteTable(sql, new MySqlParameter("@userId", userId));
            if (dt.Rows.Count > 0)
            {
                return MapStudentRow(dt.Rows[0]);
            }
            return null;
        }

        public Student GetStudentByRegNumber(string regNumber)
        {
            string sql = @"SELECT s.*, d.dept_code, d.dept_name, sc.school_code, sc.school_name
                           FROM students s
                           JOIN departments d ON s.dept_id = d.dept_id
                           JOIN schools sc ON d.school_id = sc.school_id
                           WHERE LOWER(s.reg_number) = LOWER(@regNumber);";

            DataTable dt = DbConnectionFactory.ExecuteTable(sql, new MySqlParameter("@regNumber", regNumber));
            if (dt.Rows.Count > 0)
            {
                return MapStudentRow(dt.Rows[0]);
            }
            return null;
        }

        public List<Student> GetAllStudents()
        {
            List<Student> list = new List<Student>();
            string sql = @"SELECT s.*, d.dept_code, d.dept_name, sc.school_code, sc.school_name
                           FROM students s
                           JOIN departments d ON s.dept_id = d.dept_id
                           JOIN schools sc ON d.school_id = sc.school_id
                           ORDER BY s.last_name, s.first_name;";

            DataTable dt = DbConnectionFactory.ExecuteTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(MapStudentRow(row));
            }
            return list;
        }

        public bool UpdateStudentProfile(Student student)
        {
            string sql = @"UPDATE students SET
                           first_name = @first_name,
                           last_name = @last_name,
                           dob = @dob,
                           gender = @gender,
                           national_id = @national_id,
                           part = @part,
                           dept_id = @dept_id,
                           phone = @phone,
                           email = @email,
                           hit_mail = @hit_mail,
                           address = @address,
                           guardian_name = @guardian_name,
                           guardian_phone = @guardian_phone,
                           guardian_email = @guardian_email,
                           guardian_relationship = @guardian_relationship
                           WHERE student_id = @student_id;";

            int rows = DbConnectionFactory.ExecuteNonQuery(sql,
                new MySqlParameter("@first_name", student.FirstName),
                new MySqlParameter("@last_name", student.LastName),
                new MySqlParameter("@dob", student.DateOfBirth),
                new MySqlParameter("@gender", student.Gender),
                new MySqlParameter("@national_id", student.NationalID),
                new MySqlParameter("@part", student.Part),
                new MySqlParameter("@dept_id", student.DeptId),
                new MySqlParameter("@phone", student.Phone),
                new MySqlParameter("@email", student.Email),
                new MySqlParameter("@hit_mail", student.HITMail),
                new MySqlParameter("@address", student.Address),
                new MySqlParameter("@guardian_name", student.GuardianName),
                new MySqlParameter("@guardian_phone", student.GuardianPhone),
                new MySqlParameter("@guardian_email", student.GuardianEmail),
                new MySqlParameter("@guardian_relationship", student.GuardianRelationship),
                new MySqlParameter("@student_id", student.StudentId));

            return rows > 0;
        }

        public List<School> GetAllSchools()
        {
            List<School> schools = new List<School>();
            string sql = "SELECT school_id, school_code, school_name FROM schools ORDER BY school_code;";
            DataTable dt = DbConnectionFactory.ExecuteTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                schools.Add(new School
                {
                    SchoolId = Convert.ToInt32(row["school_id"]),
                    SchoolCode = row["school_code"].ToString(),
                    SchoolName = row["school_name"].ToString()
                });
            }
            return schools;
        }

        public List<Department> GetDepartmentsBySchool(int schoolId)
        {
            List<Department> list = new List<Department>();
            string sql = @"SELECT d.dept_id, d.school_id, d.dept_code, d.dept_name, sc.school_code, sc.school_name
                           FROM departments d
                           JOIN schools sc ON d.school_id = sc.school_id
                           WHERE d.school_id = @schoolId
                           ORDER BY d.dept_code;";

            DataTable dt = DbConnectionFactory.ExecuteTable(sql, new MySqlParameter("@schoolId", schoolId));
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Department
                {
                    DeptId = Convert.ToInt32(row["dept_id"]),
                    SchoolId = Convert.ToInt32(row["school_id"]),
                    DeptCode = row["dept_code"].ToString(),
                    DeptName = row["dept_name"].ToString(),
                    SchoolCode = row["school_code"].ToString(),
                    SchoolName = row["school_name"].ToString()
                });
            }
            return list;
        }

        public List<Department> GetAllDepartments()
        {
            List<Department> list = new List<Department>();
            string sql = @"SELECT d.dept_id, d.school_id, d.dept_code, d.dept_name, sc.school_code, sc.school_name
                           FROM departments d
                           JOIN schools sc ON d.school_id = sc.school_id
                           ORDER BY d.dept_code;";

            DataTable dt = DbConnectionFactory.ExecuteTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Department
                {
                    DeptId = Convert.ToInt32(row["dept_id"]),
                    SchoolId = Convert.ToInt32(row["school_id"]),
                    DeptCode = row["dept_code"].ToString(),
                    DeptName = row["dept_name"].ToString(),
                    SchoolCode = row["school_code"].ToString(),
                    SchoolName = row["school_name"].ToString()
                });
            }
            return list;
        }

        private Student MapStudentRow(DataRow row)
        {
            return new Student
            {
                StudentId = Convert.ToInt32(row["student_id"]),
                RegNumber = row["reg_number"].ToString(),
                FirstName = row["first_name"].ToString(),
                LastName = row["last_name"].ToString(),
                DateOfBirth = Convert.ToDateTime(row["dob"]),
                Gender = row["gender"].ToString(),
                NationalID = row["national_id"].ToString(),
                Part = Convert.ToInt32(row["part"]),
                DeptId = Convert.ToInt32(row["dept_id"]),
                DeptCode = row["dept_code"].ToString(),
                DeptName = row["dept_name"].ToString(),
                SchoolCode = row["school_code"].ToString(),
                SchoolName = row["school_name"].ToString(),
                Phone = row["phone"].ToString(),
                Email = row["email"].ToString(),
                HITMail = row["hit_mail"].ToString(),
                Address = row["address"].ToString(),
                GuardianName = row["guardian_name"].ToString(),
                GuardianPhone = row["guardian_phone"].ToString(),
                GuardianEmail = row["guardian_email"].ToString(),
                GuardianRelationship = row["guardian_relationship"].ToString()
            };
        }
    }
}
