namespace DentalMVP.Models
{
    public class ServiceInsumo
    {
        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;
        public int InsumoId { get; set; }
        public Insumo Insumo { get; set; } = null!;
        public decimal Quantity { get; set; }
    }
}