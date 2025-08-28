using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DentalMVP.Models;

namespace DentalMVP.Pages
{
    public partial class PacientesPage : Page
    {
        public PacientesPage()
        {
            InitializeComponent();
            dg.ItemsSource = App.Db.Patients.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                var p = new Patient
                {
                    FullName = txtNombre.Text.Trim(),
                    Phone = txtTel.Text.Trim(),
                    Email = txtEmail.Text.Trim()
                };
                App.Db.Patients.Add(p);
                App.Db.SaveChanges();
                dg.ItemsSource = App.Db.Patients.ToList();
                txtNombre.Clear(); txtTel.Clear(); txtEmail.Clear();
            }
            else MessageBox.Show("Escribe un nombre");
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            App.Db.SaveChanges();
            MessageBox.Show("Cambios guardados");
        }
    }
}