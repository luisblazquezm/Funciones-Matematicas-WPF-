using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WinMaths.src.views
{
    /// <summary>
    /// Lógica de interacción para ExportUI.xaml
    /// </summary>
    public partial class ExportUI : Page
    {
        public ExportUI()
        {
            InitializeComponent();

            // Gestión del Botón Exportar Representación
            ExportImageButton.Click += ExportImageButton_Click;

            // Gestión del ComboBox de Extensiones de Imágen
            ImageExtensionsComboBox.ItemsSource = InitializeImageExtensionsComboBox();
            ImageExtensionsComboBox.SelectionChanged += ImageExtensionsComboBox_SelectionChanged;

            // Gestión del Botón para pasar de la tabla Graficas Creadas a las Graficas a Exportar
            LeftButton.MouseDoubleClick += LeftButton_MouseDoubleClick;

            // Gestión del Botón para pasar de la tabla Graficas a Exportar a las Graficas Creadas
            RightButton.MouseDoubleClick += RightButton_MouseDoubleClick;

            // Gestión del Botón Exportar Grafica
            ExportGraphicButton.Click += ExportGraphicButton_Click;
        }


        private String[] InitializeImageExtensionsComboBox()
        {
            return null;
        }

        private void ExportImageButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ImageExtensionsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LeftButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RightButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ExportGraphicButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
