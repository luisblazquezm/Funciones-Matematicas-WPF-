using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WinMaths.src.views
{
    /// <summary>
    /// Lógica de interacción para RepresentationLimitsWindow.xaml
    /// </summary>
    public partial class RepresentationLimitsWindow : Window
    {
        public double Xmin { get => double.Parse(XMinTextBox.Text); set => XMinTextBox.Text = value + ""; }
        public double Xmax { get => double.Parse(XMaxTextBox.Text); set => XMaxTextBox.Text = value + ""; }
        public double Ymin { get => double.Parse(YMinTextBox.Text); set => YMinTextBox.Text = value + ""; }
        public double Ymax { get => double.Parse(YMaxTextBox.Text); set => YMaxTextBox.Text = value + ""; }
        public Boolean LimitsChanged { get; set; }

        public RepresentationLimitsWindow()
        {
            InitializeComponent();

            // Gestión Botón de Confirmar Cambios
            ConfirmButton.Click += ConfirmButton_Click;

            // Gestión Botón de Cancelar Modificar
            CancelButton.Click += CancelButton_Click;

        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            LimitsChanged = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            LimitsChanged = false;
            this.Close();
        }

    }
}
