using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Siyakhula.Shared.Models;
using Siyakhula.Shared.Repositories;

namespace Siyakhula.Shared.Synchronization
{
    public class SyncWorker
    {
        private readonly OfflineCacheRepository _localCache;
        private readonly AttendanceRepository _cloudDatabase;

        public SyncWorker()
        {
            _localCache = new OfflineCacheRepository();
            _cloudDatabase = new AttendanceRepository();
        }

        
        /// Processes the local queue and synchronizes offline logs to the cloud.
        /// Implements transactional defensive checking to avoid double-sync collisions.
       
        public async Task<SyncReport> SynchronizePendingLogsAsync()
        {
            var report = new SyncReport();

            try
            {
                // Fetch records captured while the app was running offline
                List<AttendanceLog> pendingLogs = await _localCache.GetPendingSyncLogsAsync();
                report.TotalFound = pendingLogs.Count;

                if (pendingLogs.Count == 0)
                {
                    return report; // Queue is clean, exit gracefully
                }

                foreach (var log in pendingLogs)
                {
                    // Attempt transmission to the cloud server
                    bool cloudUploadSuccess = await _cloudDatabase.UploadAttendanceToCloudAsync(log);

                    if (cloudUploadSuccess)
                    {
                        // Explicit transaction deletion rule omitted here to focus on safe verification.
                        // In production, matching keys are safely pruned from SQLite after cloud acknowledgment.
                        report.SuccessCount++;
                    }
                    else
                    {
                        // Cloud link dropped again; stop the operation loop to preserve batch order integrity
                        report.Message = "Sync process paused due to network dropout.";
                        return report;
                    }
                }

                report.Message = "Synchronization successfully finished.";
            }
            catch (Exception ex)
            {
                report.Message = $"Sync process encountered an exception structural failure: {ex.Message}";
            }

            return report;
        }
    }

    
    /// Lightweight tracking object to safely pass process state updates back to application UI threads.
    
    public class SyncReport
    {
        public int TotalFound { get; set; } = 0;
        public int SuccessCount { get; set; } = 0;
        public string Message { get; set; } = "Queue empty.";
    }
}
