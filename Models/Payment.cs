using System;

namespace DentalMVP.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; } = null!;
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public string? RefInfo { get; set; } // # de autorización, etc.
    }
}