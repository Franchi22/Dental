using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DentalMVP.Models;

namespace DentalMVP.Pages
{
    public partial class FacturarPage : Page
    {
        private Invoice current = new Invoice { Ncf = "B02-00000001" }; // mock NCF en MVP

        public FacturarPage()
        {
            InitializeComponent();
            cbPaciente.ItemsSource = App.Db.Patients.OrderBy(p => p.FullName).ToList();
            cbServicio.ItemsSource = App.Db.Services.Where(s => s.Active).OrderBy(s => s.Name).ToList();
            dg.ItemsSource = current.Lines;

            cbPago.ItemsSource = Enum.GetValues(typeof(PaymentMethod));
            RefreshTotals();
        }

        private void AddLine_Click(object sender, RoutedEventArgs e)
        {
            if (cbServicio.SelectedItem is Service s && decimal.TryParse(txtCant.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out var q) && q > 0)
            {
                current.Lines.Add(new InvoiceLine
                {
                    ServiceId = s.Id,
                    Service = s,
                    Quantity = q,
                    UnitPrice = s.Price,
                    ItbisPct = s.ItbisPct
                });
                dg.Items.Refresh();
                RefreshTotals();
            }
        }

        private void RefreshTotals()
        {
            tbSub.Text = current.Subtotal.ToString("N2");
            tbItbis.Text = current.Itbis.ToString("N2");
            tbTotal.Text = current.Total.ToString("N2");
        }

        private void SaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (cbPaciente.SelectedValue is int pid && current.Lines.Count > 0)
            {
                current.PatientId = pid;
                // Persistir
                App.Db.Invoices.Add(current);
                foreach (var l in current.Lines)
                {
                    // EF creará InvoiceLine al guardar
                    // Descontar inventario según receta (si existe)
                    var receta = App.Db.ServiceInsumos.Where(si => si.ServiceId == l.ServiceId).ToList();
                    foreach (var r in receta)
                    {
                        var ins = App.Db.Insumos.FirstOrDefault(i => i.Id == r.InsumoId);
                        if (ins != null)
                        {
                            ins.Stock -= r.Quantity * l.Quantity;
                        }
                    }
                }

                // Pago (opcional en MVP)
                if (decimal.TryParse(txtPaga.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out var pago) && pago > 0)
                {
                    var method = (PaymentMethod)cbPago.SelectedItem;
                    current.Payments.Add(new Payment { Amount = pago, Method = method });
                }

                App.Db.SaveChanges();

                MessageBox.Show($"Factura guardada. Total RD$ {current.Total:N2}. Balance RD$ {current.Balance:N2}");

                // preparar nueva factura
                var nextNcf = NextNcf(current.Ncf);
                current = new Invoice { Ncf = nextNcf };
                dg.ItemsSource = current.Lines;
                dg.Items.Refresh();
                txtPaga.Clear();
                RefreshTotals();
            }
            else
            {
                MessageBox.Show("Seleccione paciente y agregue al menos un servicio.");
            }
        }

        private string NextNcf(string ncf)
        {
            // MVP: asume formato "B02-00000001" → incrementa correlativo
            try
            {
                var parts = ncf.Split('-');
                var prefix = parts[0];
                var number = int.Parse(parts[1]);
                return $"{prefix}-{(number + 1).ToString("D8")}";
            }
            catch { return "B02-00000001"; }
        }
    }
}