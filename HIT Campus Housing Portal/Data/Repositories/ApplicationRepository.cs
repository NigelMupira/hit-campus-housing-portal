using System;
using System.Collections.Generic;
using System.Data;
using HIT_Campus_Housing_Portal.Models;
using MySql.Data.MySqlClient;

namespace HIT_Campus_Housing_Portal.Data.Repositories
{
    public class ApplicationRepository
    {
        public bool CreateApplication(Application app)
        {
            string sql = @"INSERT INTO applications 
                           (student_id, hostel_id, preferred_room_1, preferred_room_2, reason, status) 
                           VALUES 
                           (@student_id, @hostel_id, @preferred_room_1, @preferred_room_2, @reason, 'Pending');";

            int rows = DbConnectionFactory.ExecuteNonQuery(sql,
                new MySqlParameter("@student_id", app.StudentId),
                new MySqlParameter("@hostel_id", app.HostelId),
                new MySqlParameter("@preferred_room_1", app.PreferredRoom1 ?? (object)DBNull.Value),
                new MySqlParameter("@preferred_room_2", app.PreferredRoom2 ?? (object)DBNull.Value),
                new MySqlParameter("@reason", app.Reason ?? (object)DBNull.Value));

            return rows > 0;
        }

        public Application GetApplicationByStudentId(int studentId)
        {
            string sql = @"SELECT a.*, h.hostel_name, CONCAT(s.first_name, ' ', s.last_name) AS student_name, s.reg_number,
                           ra.room_id, r.room_number AS assigned_room
                           FROM applications a
                           JOIN hostels h ON a.hostel_id = h.hostel_id
                           JOIN students s ON a.student_id = s.student_id
                           LEFT JOIN room_assignments ra ON a.application_id = ra.application_id
                           LEFT JOIN rooms r ON ra.room_id = r.room_id
                           WHERE a.student_id = @student_id
                           ORDER BY a.created_at DESC
                           LIMIT 1;";

            DataTable dt = DbConnectionFactory.ExecuteTable(sql, new MySqlParameter("@student_id", studentId));
            if (dt.Rows.Count > 0)
            {
                return MapApplicationRow(dt.Rows[0]);
            }
            return null;
        }

        public List<Application> GetAllApplications(string statusFilter = null)
        {
            List<Application> list = new List<Application>();
            string sql = @"SELECT a.*, h.hostel_name, CONCAT(s.first_name, ' ', s.last_name) AS student_name, s.reg_number,
                           ra.room_id, r.room_number AS assigned_room
                           FROM applications a
                           JOIN hostels h ON a.hostel_id = h.hostel_id
                           JOIN students s ON a.student_id = s.student_id
                           LEFT JOIN room_assignments ra ON a.application_id = ra.application_id
                           LEFT JOIN rooms r ON ra.room_id = r.room_id";

            if (!string.IsNullOrEmpty(statusFilter))
            {
                sql += " WHERE a.status = @status";
            }
            sql += " ORDER BY a.created_at DESC;";

            MySqlParameter[] p = string.IsNullOrEmpty(statusFilter) ? null : new MySqlParameter[] { new MySqlParameter("@status", statusFilter) };
            DataTable dt = DbConnectionFactory.ExecuteTable(sql, p);

            foreach (DataRow row in dt.Rows)
            {
                list.Add(MapApplicationRow(row));
            }
            return list;
        }

        public bool ApproveApplication(int applicationId, string assignedRoomNumber, string remarks)
        {
            using (var conn = DbConnectionFactory.GetConnection())
            using (var transaction = conn.BeginTransaction())
            {
                try
                {
                    // Get application details
                    string getAppSql = "SELECT student_id, hostel_id FROM applications WHERE application_id = @appId;";
                    int studentId = 0;
                    int hostelId = 0;
                    using (var cmd = new MySqlCommand(getAppSql, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@appId", applicationId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                studentId = reader.GetInt32("student_id");
                                hostelId = reader.GetInt32("hostel_id");
                            }
                        }
                    }

                    // Find or create room
                    string getRoomSql = "SELECT room_id FROM rooms WHERE hostel_id = @hostelId AND room_number = @roomNum;";
                    int roomId = 0;
                    using (var cmd = new MySqlCommand(getRoomSql, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@hostelId", hostelId);
                        cmd.Parameters.AddWithValue("@roomNum", assignedRoomNumber);
                        object res = cmd.ExecuteScalar();
                        if (res != null && res != DBNull.Value)
                        {
                            roomId = Convert.ToInt32(res);
                        }
                        else
                        {
                            // Create room
                            string createRoomSql = "INSERT INTO rooms (hostel_id, room_number, capacity, occupied_count) VALUES (@hostelId, @roomNum, 2, 0); SELECT LAST_INSERT_ID();";
                            using (var createCmd = new MySqlCommand(createRoomSql, conn, transaction))
                            {
                                createCmd.Parameters.AddWithValue("@hostelId", hostelId);
                                createCmd.Parameters.AddWithValue("@roomNum", assignedRoomNumber);
                                roomId = Convert.ToInt32(createCmd.ExecuteScalar());
                            }
                        }
                    }

                    // Update Application status
                    string updateAppSql = "UPDATE applications SET status = 'Approved', admin_remarks = @remarks, processed_at = NOW() WHERE application_id = @appId;";
                    using (var cmd = new MySqlCommand(updateAppSql, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@remarks", remarks ?? "");
                        cmd.Parameters.AddWithValue("@appId", applicationId);
                        cmd.ExecuteNonQuery();
                    }

                    // Create Room Assignment
                    string assignSql = "INSERT INTO room_assignments (application_id, student_id, room_id) VALUES (@appId, @studentId, @roomId) ON DUPLICATE KEY UPDATE room_id = @roomId;";
                    using (var cmd = new MySqlCommand(assignSql, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@appId", applicationId);
                        cmd.Parameters.AddWithValue("@studentId", studentId);
                        cmd.Parameters.AddWithValue("@roomId", roomId);
                        cmd.ExecuteNonQuery();
                    }

                    // Increment Room occupied_count
                    string incRoomSql = "UPDATE rooms SET occupied_count = occupied_count + 1 WHERE room_id = @roomId;";
                    using (var cmd = new MySqlCommand(incRoomSql, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@roomId", roomId);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool RejectApplication(int applicationId, string remarks)
        {
            string sql = "UPDATE applications SET status = 'Rejected', admin_remarks = @remarks, processed_at = NOW() WHERE application_id = @appId;";
            int rows = DbConnectionFactory.ExecuteNonQuery(sql,
                new MySqlParameter("@remarks", remarks ?? ""),
                new MySqlParameter("@appId", applicationId));
            return rows > 0;
        }

        public (int TotalRequests, int ApprovedCount, int RejectedCount, int PendingCount) GetApplicationStatistics()
        {
            string sqlTotal = "SELECT COUNT(*) FROM applications;";
            string sqlApproved = "SELECT COUNT(*) FROM applications WHERE status = 'Approved';";
            string sqlRejected = "SELECT COUNT(*) FROM applications WHERE status = 'Rejected';";
            string sqlPending = "SELECT COUNT(*) FROM applications WHERE status = 'Pending';";

            int total = Convert.ToInt32(DbConnectionFactory.ExecuteScalar(sqlTotal) ?? 0);
            int approved = Convert.ToInt32(DbConnectionFactory.ExecuteScalar(sqlApproved) ?? 0);
            int rejected = Convert.ToInt32(DbConnectionFactory.ExecuteScalar(sqlRejected) ?? 0);
            int pending = Convert.ToInt32(DbConnectionFactory.ExecuteScalar(sqlPending) ?? 0);

            return (total, approved, rejected, pending);
        }

        private Application MapApplicationRow(DataRow row)
        {
            return new Application
            {
                ApplicationId = Convert.ToInt32(row["application_id"]),
                StudentId = Convert.ToInt32(row["student_id"]),
                StudentName = row["student_name"].ToString(),
                RegNumber = row["reg_number"].ToString(),
                HostelId = Convert.ToInt32(row["hostel_id"]),
                HostelName = row["hostel_name"].ToString(),
                PreferredRoom1 = row["preferred_room_1"].ToString(),
                PreferredRoom2 = row["preferred_room_2"].ToString(),
                Reason = row["reason"].ToString(),
                Status = row["status"].ToString(),
                AdminRemarks = row["admin_remarks"].ToString(),
                AssignedRoomNumber = row.Table.Columns.Contains("assigned_room") ? row["assigned_room"].ToString() : string.Empty,
                CreatedAt = Convert.ToDateTime(row["created_at"]),
                ProcessedAt = row["processed_at"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["processed_at"]) : null
            };
        }
    }
}
