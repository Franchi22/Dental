// MainWindow.xaml.cs
using System;
using System.Windows;
using System.Windows.Controls;

namespace DentalMVP
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Nav_Click(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button)?.Tag as string;
            if (string.IsNullOrWhiteSpace(tag)) return;

            switch (tag)
            {
                case "Pacientes":
                    MainFrame.Navigate(new DentalMVP.Pages.PacientesPage());
                    break;
                case "Servicios":
                    MainFrame.Navigate(new DentalMVP.Pages.ServiciosPage());
                    break;
                case "Inventario":
                    MainFrame.Navigate(new DentalMVP.Pages.InventarioPage());
                    break;
                case "Facturar":
                    MainFrame.Navigate(new DentalMVP.Pages.FacturarPage());
                    break;
                case "Odontograma":
                    MainFrame.Navigate(new DentalMVP.Pages.OdontogramaPage());
                    break;
            }
        }
    }
}
