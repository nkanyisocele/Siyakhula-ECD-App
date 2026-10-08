using System;

namespace Siyakhula.Shared.Models
{
    public class Child
    {
        public int ChildID { get; set; }
        public int CrecheID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string GovernmentID { get; set; } // Validated 13-digit SA ID
        public string Status { get; set; } = "Active";
    }
}
