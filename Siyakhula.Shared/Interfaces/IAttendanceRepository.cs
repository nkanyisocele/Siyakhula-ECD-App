using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Siyakhula.Shared.Models;

namespace Siyakhula.Shared.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<bool> SaveLogAsync(AttendanceLog log);
        Task<IEnumerable<AttendanceLog>> GetUnsyncedLogsAsync();
        Task<bool> MarkAsSyncedAsync(Guid logId);
    }
}
