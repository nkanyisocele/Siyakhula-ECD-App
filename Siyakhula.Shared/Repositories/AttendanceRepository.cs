using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Siyakhula.Shared.Models;
using Siyakhula.Shared.Database;

namespace Siyakhula.Shared.Repositories
{
    public class AttendanceRepository
    {
        private readonly string _connectionString;

        public AttendanceRepository()
        {
            _connectionString = DatabaseConfig.GetCloudConnectionString();
        }

        /// <summary>
        /// Safely uploads an attendance transaction to the cloud database.
        /// Strong parameters prevent SQL Injection attacks completely.
        /// </summary>
        public async Task<bool> UploadAttendanceToCloudAsync(AttendanceLog log)
        {
            string secureQuery = "INSERT INTO AttendanceLog (LogID, ChildID, LogDate, IsPresent, SyncedToCloud, TimestampRecorded) " +
                                 "VALUES (@LogID, @ChildID, @LogDate, @IsPresent, @Synced, @Timestamp)";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(secureQuery, connection))
                    {
                        command.Parameters.AddWithValue("@LogID", log.LogID);
                        command.Parameters.AddWithValue("@ChildID", log.ChildID);
                        command.Parameters.AddWithValue("@LogDate", log.LogDate.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@IsPresent", log.IsPresent);
                        command.Parameters.AddWithValue("@Synced", true);
                        command.Parameters.AddWithValue("@Timestamp", log.TimestampRecorded);

                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException)
            {
                // Gracefully handles network failures during load shedding or connectivity drops
                return false;
            }
        }
    }
}

