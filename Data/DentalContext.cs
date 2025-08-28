using Microsoft.EntityFrameworkCore;
using DentalMVP.Models;
using System;
using System.Linq;

namespace DentalMVP.Data
{
    public class DentalContext : DbContext
    {
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<Insumo> Insumos => Set<Insumo>();
        public DbSet<ServiceInsumo> ServiceInsumos => Set<ServiceInsumo>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<InvoiceLine> InvoiceLines => Set<InvoiceLine>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<ToothRecord> ToothRecords => Set<ToothRecord>();
        public DbSet<User> Users => Set<User>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=dental.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceInsumo>().HasKey(si => new { si.ServiceId, si.InsumoId });
            modelBuilder.Entity<ServiceInsumo>()
                .HasOne(si => si.Service)
                .WithMany(s => s.Insumos)
                .HasForeignKey(si => si.ServiceId);
            modelBuilder.Entity<ServiceInsumo>()
                .HasOne(si => si.Insumo)
                .WithMany()
                .HasForeignKey(si => si.InsumoId);
        }

        public void SeedIfEmpty()
        {
            if (!Patients.Any())
            {
                Patients.AddRange(
                    new Patient { FullName = "Juan Pérez", Phone = "809-000-0000" },
                    new Patient { FullName = "Ana Gómez", Phone = "809-111-1111" }
                );
            }

            if (!Services.Any())
            {
                Services.AddRange(
                    new Service { Name = "Limpieza Dental", Price = 1500m, ItbisPct = 0 },
                    new Service { Name = "Resina (pieza)", Price = 2200m, ItbisPct = 0 },
                    new Service { Name = "Endodoncia", Price = 9500m, ItbisPct = 0 }
                );
            }

            if (!Insumos.Any())
            {
                Insumos.AddRange(
                    new Insumo { Name = "Guantes", Um = "Par", Stock = 200, StockMin = 50, Cost = 25 },
                    new Insumo { Name = "Resina compuesta", Um = "Jeringa", Stock = 20, StockMin = 5, Cost = 850 },
                    new Insumo { Name = "Anestesia", Um = "Ampolla", Stock = 50, StockMin = 10, Cost = 120 }
                );
            }

            if (!Users.Any())
            {
                Users.Add(new User { Username = "admin", Role = UserRole.Admin, DisplayName = "Administrador" });
            }

            SaveChanges();

            // Vincular recetas (servicio -> insumos sugeridos)
            var resina = Services.FirstOrDefault(s => s.Name.Contains("Resina"));
            var endo = Services.FirstOrDefault(s => s.Name.Contains("Endodoncia"));
            var guantes = Insumos.FirstOrDefault(i => i.Name.Contains("Guantes"));
            var anestesia = Insumos.FirstOrDefault(i => i.Name.Contains("Anestesia"));

            if (resina != null && guantes != null)
            {
                if (!ServiceInsumos.Any(si => si.ServiceId == resina.Id && si.InsumoId == guantes.Id))
                    ServiceInsumos.Add(new ServiceInsumo { ServiceId = resina.Id, InsumoId = guantes.Id, Quantity = 1 });
            }
            if (endo != null && anestesia != null)
            {
                if (!ServiceInsumos.Any(si => si.ServiceId == endo.Id && si.InsumoId == anestesia.Id))
                    ServiceInsumos.Add(new ServiceInsumo { ServiceId = endo.Id, InsumoId = anestesia.Id, Quantity = 1 });
            }

            SaveChanges();
        }
    }
}