using System;

namespace DentalMVP.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Ars { get; set; } // ARS (simple en MVP)
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}