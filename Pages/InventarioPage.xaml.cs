using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DentalMVP.Models;

namespace DentalMVP.Pages
{
    public partial class InventarioPage : Page
    {
        public InventarioPage()
        {
            InitializeComponent();
            dg.ItemsSource = App.Db.Insumos.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(txtStock.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out var stock)
                && decimal.TryParse(txtMin.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out var min)
                && decimal.TryParse(txtCosto.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out var costo)
                && !string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                var ins = new Insumo { Name = txtNombre.Text.Trim(), Um = txtUM.Text.Trim(), Stock = stock, StockMin = min, Cost = costo };
                App.Db.Insumos.Add(ins);
                App.Db.SaveChanges();
                dg.ItemsSource = App.Db.Insumos.ToList();
                txtNombre.Clear(); txtUM.Clear(); txtStock.Clear(); txtMin.Clear(); txtCosto.Clear();
            }
            else MessageBox.Show("Datos inválidos");
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            App.Db.SaveChanges();
            MessageBox.Show("Cambios guardados");
        }
    }
}
