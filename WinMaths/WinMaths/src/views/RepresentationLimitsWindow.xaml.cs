using System.Windows;

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
            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

    }
}
