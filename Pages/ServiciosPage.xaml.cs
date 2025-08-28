using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DentalMVP.Models;


namespace DentalMVP.Pages
{
    public partial class ServiciosPage : Page
    {
        public ServiciosPage()
        {
            InitializeComponent();
            dg.ItemsSource = App.Db.Services.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(txtPrecio.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out var precio)
                && !string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                var itbis = Convert.ToDecimal(((ComboBoxItem)cbItbis.SelectedItem).Content);
                var s = new Service { Name = txtNombre.Text.Trim(), Price = precio, ItbisPct = itbis };
                App.Db.Services.Add(s);
                App.Db.SaveChanges();
                dg.ItemsSource = App.Db.Services.ToList();
                txtNombre.Clear(); txtPrecio.Clear(); cbItbis.SelectedIndex = 0;
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