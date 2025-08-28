using System;
using System.Collections.Generic;
using System.Linq;

namespace DentalMVP.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        // MVP: NCF mockeado; luego se integra e-CF DGII
        public string Ncf { get; set; } = string.Empty; // B02-00000001 ...

        public List<InvoiceLine> Lines { get; set; } = new();
        public decimal Subtotal => Lines.Sum(l => l.LineTotal);
        public decimal Itbis => Lines.Sum(l => l.ItbisAmount);
        public decimal Total => Subtotal + Itbis;

        // Pagos simples (puede haber varios)
        public List<Payment> Payments { get; set; } = new();
        public decimal Paid => Payments.Sum(p => p.Amount);
        public decimal Balance => Total - Paid;
    }
}