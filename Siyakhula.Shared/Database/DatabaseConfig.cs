using System;

namespace Siyakhula.Shared.Database
{
    public static class DatabaseConfig
    {
        public static string GetCloudConnectionString()
        {
            // Cleaned string structure to remove structural compiling confusion
            return "Server=tcp:siyakhula-ecd-db.database.windows.net,1433;Initial Catalog=SiyakhulaECD_DB;Persist Security Info=False;User ID=SiyakhulaAdmin;Password=YourSecureStudentPassword2026!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
    }
}

