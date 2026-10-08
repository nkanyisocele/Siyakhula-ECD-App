using System;

namespace Siyakhula.Shared.Models
{
    public class AttendanceLog
    {
        // Guid (UniqueIdentifier) to allow offline generation without database key conflicts
        public Guid LogID { get; set; } = Guid.NewGuid();
        public int ChildID { get; set; }
        public DateTime LogDate { get; set; }
        public bool IsPresent { get; set; }
        public bool SyncedToCloud { get; set; } = false;
        public DateTime TimestampRecorded { get; set; } = DateTime.Now;
    }
}
