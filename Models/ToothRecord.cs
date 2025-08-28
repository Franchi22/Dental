using System;

namespace DentalMVP.Models
{
    // Prototipo simple de odontograma: estado por pieza (FDI)
    public class ToothRecord
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string ToothFdi { get; set; } = "11"; // 11..48, 51..85
        public string State { get; set; } = "Sano";  // Caries/Obturación/Coro­na/etc.
        public DateTime Date { get; set; } = DateTime.Now; // último registro
    }
}