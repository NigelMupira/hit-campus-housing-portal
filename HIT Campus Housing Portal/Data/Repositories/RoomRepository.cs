using System;
using System.Collections.Generic;
using System.Data;
using HIT_Campus_Housing_Portal.Models;
using MySql.Data.MySqlClient;

namespace HIT_Campus_Housing_Portal.Data.Repositories
{
    public class RoomRepository
    {
        public DataTable GetHostelsByGender(string gender)
        {
            string sql = "SELECT hostel_id, hostel_name, gender_target, capacity FROM hostels WHERE gender_target = @gender OR gender_target = 'Mixed';";
            return DbConnectionFactory.ExecuteTable(sql, new MySqlParameter("@gender", gender));
        }

        public DataTable GetAllHostels()
        {
            string sql = "SELECT hostel_id, hostel_name, gender_target, capacity FROM hostels;";
            return DbConnectionFactory.ExecuteTable(sql);
        }

        public List<Room> GetAvailableRoomsByHostel(int hostelId)
        {
            List<Room> rooms = new List<Room>();
            string sql = @"SELECT r.room_id, r.hostel_id, h.hostel_name, r.room_number, r.capacity, r.occupied_count 
                           FROM rooms r
                           JOIN hostels h ON r.hostel_id = h.hostel_id
                           WHERE r.hostel_id = @hostelId AND r.occupied_count < r.capacity
                           ORDER BY r.room_number;";

            DataTable dt = DbConnectionFactory.ExecuteTable(sql, new MySqlParameter("@hostelId", hostelId));
            foreach (DataRow row in dt.Rows)
            {
                rooms.Add(new Room
                {
                    RoomId = Convert.ToInt32(row["room_id"]),
                    HostelId = Convert.ToInt32(row["hostel_id"]),
                    HostelName = row["hostel_name"].ToString(),
                    RoomNumber = row["room_number"].ToString(),
                    Capacity = Convert.ToInt32(row["capacity"]),
                    OccupiedCount = Convert.ToInt32(row["occupied_count"])
                });
            }
            return rooms;
        }

        public (int TotalRooms, int RoomsLeft, int MaleRoomsLeft, int FemaleRoomsLeft) GetRoomStatistics()
        {
            string sqlTotal = "SELECT COUNT(*) FROM rooms;";
            string sqlLeft = "SELECT COUNT(*) FROM rooms WHERE occupied_count < capacity;";
            string sqlMaleLeft = @"SELECT COUNT(*) FROM rooms r 
                                   JOIN hostels h ON r.hostel_id = h.hostel_id 
                                   WHERE h.gender_target = 'M' AND r.occupied_count < r.capacity;";
            string sqlFemaleLeft = @"SELECT COUNT(*) FROM rooms r 
                                     JOIN hostels h ON r.hostel_id = h.hostel_id 
                                     WHERE h.gender_target = 'F' AND r.occupied_count < r.capacity;";

            int total = Convert.ToInt32(DbConnectionFactory.ExecuteScalar(sqlTotal) ?? 0);
            int left = Convert.ToInt32(DbConnectionFactory.ExecuteScalar(sqlLeft) ?? 0);
            int maleLeft = Convert.ToInt32(DbConnectionFactory.ExecuteScalar(sqlMaleLeft) ?? 0);
            int femaleLeft = Convert.ToInt32(DbConnectionFactory.ExecuteScalar(sqlFemaleLeft) ?? 0);

            return (total, left, maleLeft, femaleLeft);
        }
    }
}
