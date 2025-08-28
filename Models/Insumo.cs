namespace DentalMVP.Models
{
    public class Insumo
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Um { get; set; } = "Unidad"; // Par, Ampolla, etc.
        public decimal Stock { get; set; }
        public decimal StockMin { get; set; }
        public decimal Cost { get; set; }
    }
}