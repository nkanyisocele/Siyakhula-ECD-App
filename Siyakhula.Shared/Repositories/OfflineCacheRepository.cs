using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Siyakhula.Shared.Models;

namespace Siyakhula.Shared.Repositories
{
    public class OfflineCacheRepository
    {
        private readonly string _localDatabasePath;
        private readonly string _connectionString;

        public OfflineCacheRepository()
        {
            // Establish a persistent sandboxed local database file path for the app
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _localDatabasePath = Path.Combine(appDataFolder, "SiyakhulaOfflineCache.db");
            _connectionString = $"Data Source={_localDatabasePath}";

            InitializeLocalDatabase();
        }

        
        /// Creates the local offline table schemas on the smartphone if they don't exist yet.
        
        private void InitializeLocalDatabase()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS OfflineAttendance (
                        LogID TEXT PRIMARY KEY,
                        ChildID INTEGER NOT NULL,
                        LogDate TEXT NOT NULL,
                        IsPresent INTEGER NOT NULL,
                        TimestampRecorded TEXT NOT NULL
                    );";

                using (var command = new SqliteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

     
        /// Safely caches a child's attendance log locally when cell networks drop out.
        /// Uses structured parameters to prevent local injection tampering.
       
        public async Task<bool> SaveLogOfflineAsync(AttendanceLog log)
        {
            string insertQuery = @"INSERT INTO OfflineAttendance 
                                  (LogID, ChildID, LogDate, IsPresent, TimestampRecorded) 
                                  VALUES (@LogID, @ChildID, @LogDate, @IsPresent, @Timestamp)";

            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqliteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@LogID", log.LogID.ToString());
                        command.Parameters.AddWithValue("@ChildID", log.ChildID);
                        command.Parameters.AddWithValue("@LogDate", log.LogDate.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@IsPresent", log.IsPresent ? 1 : 0);
                        command.Parameters.AddWithValue("@Timestamp", log.TimestampRecorded.ToString("o"));

                        int rows = await command.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
            catch (Exception)
            {
                // Ensures data logging errors do not crash the mobile UI thread
                return false;
            }
        }

        
        /// Extracts all cached records that were captured offline during a network drop.
      
        public async Task<List<AttendanceLog>> GetPendingSyncLogsAsync()
        {
            var pendingLogs = new List<AttendanceLog>();
            string selectQuery = "SELECT LogID, ChildID, LogDate, IsPresent, TimestampRecorded FROM OfflineAttendance";

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqliteCommand(selectQuery, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            pendingLogs.Add(new AttendanceLog
                            {
                                LogID = Guid.Parse(reader.GetString(0)),
                                ChildID = reader.GetInt32(1),
                                LogDate = DateTime.Parse(reader.GetString(2)),
                                IsPresent = reader.GetInt32(3) == 1,
                                SyncedToCloud = false,
                                TimestampRecorded = DateTime.Parse(reader.GetString(4))
                            });
                        }
                    }
                }
            }
            return pendingLogs;
        }
    }
}
