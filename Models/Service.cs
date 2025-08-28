using System.Collections.Generic;

namespace DentalMVP.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal ItbisPct { get; set; } // 0 o 18
        public bool Active { get; set; } = true;

        public List<ServiceInsumo> Insumos { get; set; } = new();
    }
}