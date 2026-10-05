using System;
using System.Collections.Generic;
using HIT_Campus_Housing_Portal.Data.Repositories;
using HIT_Campus_Housing_Portal.Models;

namespace HIT_Campus_Housing_Portal.Services
{
    public class AdminService
    {
        private readonly ApplicationRepository _appRepo = new ApplicationRepository();
        private readonly StudentRepository _studentRepo = new StudentRepository();
        private readonly RoomRepository _roomRepo = new RoomRepository();

        public List<Application> GetApplications(string statusFilter = null)
        {
            return _appRepo.GetAllApplications(statusFilter);
        }

        public bool ApproveApplication(int applicationId, string assignedRoomNumber, string remarks)
        {
            return _appRepo.ApproveApplication(applicationId, assignedRoomNumber, remarks);
        }

        public bool RejectApplication(int applicationId, string remarks)
        {
            return _appRepo.RejectApplication(applicationId, remarks);
        }

        public List<Student> GetAllStudents()
        {
            return _studentRepo.GetAllStudents();
        }

        public (int TotalRequests, int ApprovedCount, int RejectedCount, int PendingCount) GetApplicationStats()
        {
            return _appRepo.GetApplicationStatistics();
        }

        public (int TotalRooms, int RoomsLeft, int MaleRoomsLeft, int FemaleRoomsLeft) GetRoomStats()
        {
            return _roomRepo.GetRoomStatistics();
        }
    }
}
