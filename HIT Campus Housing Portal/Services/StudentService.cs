using System;
using System.Collections.Generic;
using System.Data;
using HIT_Campus_Housing_Portal.Data.Repositories;
using HIT_Campus_Housing_Portal.Models;

namespace HIT_Campus_Housing_Portal.Services
{
    public class StudentService
    {
        private readonly StudentRepository _studentRepo = new StudentRepository();
        private readonly ApplicationRepository _appRepo = new ApplicationRepository();
        private readonly RoomRepository _roomRepo = new RoomRepository();

        public List<School> GetSchools()
        {
            return _studentRepo.GetAllSchools();
        }

        public List<Department> GetDepartmentsBySchool(int schoolId)
        {
            return _studentRepo.GetDepartmentsBySchool(schoolId);
        }

        public List<Department> GetAllDepartments()
        {
            return _studentRepo.GetAllDepartments();
        }

        public DataTable GetHostelsByGender(string gender)
        {
            return _roomRepo.GetHostelsByGender(gender);
        }

        public List<Room> GetAvailableRooms(int hostelId)
        {
            return _roomRepo.GetAvailableRoomsByHostel(hostelId);
        }

        public bool SubmitApplication(Application app, out string errorMessage)
        {
            errorMessage = string.Empty;

            var existing = _appRepo.GetApplicationByStudentId(app.StudentId);
            if (existing != null && existing.Status == "Pending")
            {
                errorMessage = "You already have an active pending accommodation application!";
                return false;
            }

            return _appRepo.CreateApplication(app);
        }

        public Application GetStudentApplication(int studentId)
        {
            return _appRepo.GetApplicationByStudentId(studentId);
        }

        public bool UpdateProfile(Student student)
        {
            return _studentRepo.UpdateStudentProfile(student);
        }

        public (int TotalRooms, int RoomsLeft, int MaleRoomsLeft, int FemaleRoomsLeft) GetRoomStats()
        {
            return _roomRepo.GetRoomStatistics();
        }
    }
}
