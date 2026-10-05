using System;
using HIT_Campus_Housing_Portal.Data.Repositories;
using HIT_Campus_Housing_Portal.Models;

namespace HIT_Campus_Housing_Portal.Services
{
    public class AuthService
    {
        private readonly UserRepository _userRepo = new UserRepository();
        private readonly StudentRepository _studentRepo = new StudentRepository();

        public User Login(string username, string password, out Student studentProfile)
        {
            studentProfile = null;
            User user = _userRepo.Authenticate(username, password);

            if (user != null && user.Role == "Student")
            {
                studentProfile = _studentRepo.GetStudentByUserId(user.UserId);
            }

            return user;
        }

        public bool RegisterStudent(Student student, string password, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (_userRepo.UserExists(student.RegNumber))
            {
                errorMessage = "Registration number is already registered!";
                return false;
            }

            int userId = _userRepo.CreateUser(student.RegNumber, password, "Student");
            if (userId > 0)
            {
                student.StudentId = userId;
                bool success = _studentRepo.CreateStudent(student);
                if (!success)
                {
                    _userRepo.DeleteUser(userId);
                    errorMessage = "Failed to create student profile details.";
                    return false;
                }
                return true;
            }

            errorMessage = "Failed to create user account.";
            return false;
        }

        public bool ChangePassword(int userId, string newPassword)
        {
            return _userRepo.ChangePassword(userId, newPassword);
        }

        public bool DeleteAccount(int userId)
        {
            return _userRepo.DeleteUser(userId);
        }
    }
}
