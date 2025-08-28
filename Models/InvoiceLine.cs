namespace DentalMVP.Models
{
    public class InvoiceLine
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; } = null!;
        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;
        public decimal Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; }
        public decimal ItbisPct { get; set; }

        public decimal LineTotal => Quantity * UnitPrice;
        public decimal ItbisAmount => LineTotal * (ItbisPct / 100m);
      }
}