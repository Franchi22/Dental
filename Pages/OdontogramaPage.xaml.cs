using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DentalMVP.Models;

namespace DentalMVP.Pages
{
    public partial class OdontogramaPage : Page
    {
        private readonly List<string> piezas = new()
        {
            // Adultos FDI 11..18, 21..28, 31..38, 41..48
            "18","17","16","15","14","13","12","11",
            "21","22","23","24","25","26","27","28",
            "48","47","46","45","44","43","42","41",
            "31","32","33","34","35","36","37","38"
        };

        private readonly string[] estados = new[] { "Sano", "Caries", "Obturación", "Endodoncia", "Ausente" };
        private readonly Dictionary<string, ComboBox> mapa = new();

        public OdontogramaPage()
        {
            InitializeComponent();
            cbPaciente.ItemsSource = App.Db.Patients.OrderBy(p => p.FullName).ToList();
            BuildGrid();
        }

        private void BuildGrid()
        {
            grid.Children.Clear();
            mapa.Clear();
            foreach (var p in piezas)
            {
                var sp = new StackPanel { Margin = new Thickness(8) };
                sp.Children.Add(new TextBlock { Text = p, FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 0, 4) });
                var cb = new ComboBox { ItemsSource = estados, SelectedIndex = 0, Width = 140 };
                sp.Children.Add(cb);
                grid.Children.Add(sp);
                mapa[p] = cb;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (cbPaciente.SelectedValue is int pid)
            {
                foreach (var kv in mapa)
                {
                    var tooth = kv.Key;
                    var state = kv.Value.SelectedItem?.ToString() ?? "Sano";

                    // Buscar registro existente; si no, crear
                    var rec = App.Db.ToothRecords.FirstOrDefault(t => t.PatientId == pid && t.ToothFdi == tooth);
                    if (rec == null)
                    {
                        rec = new ToothRecord { PatientId = pid, ToothFdi = tooth, State = state };
                        App.Db.ToothRecords.Add(rec);
                    }
                    else
                    {
                        rec.State = state;
                    }
                }
                App.Db.SaveChanges();
                MessageBox.Show("Odontograma guardado (MVP)");
            }
            else
            {
                MessageBox.Show("Seleccione paciente");
            }
        }
    }
}
