using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
using WinMaths.src.utils;
using WinMaths.src.viewModels;

namespace WinMaths.src.views
{
    /// <summary>
    /// Lógica de interacción para ExportUI.xaml
    /// </summary>
    public partial class ExportUI : Page
    {
        private IOUtils IOUtilsVar;
        private RenderTargetBitmap canvasRenderization;
        private ViewModel viewModel;

        public ExportUI(ViewModel vM)
        {
            InitializeComponent();

            this.viewModel = vM;
            CreatedGraphicsTableGrid.ItemsSource = viewModel.GetCollectionOfGraphicsVM();
            // Gestión del Botón para pasar de la tabla Graficas Creadas a las Graficas a Exportar
            LeftButton.MouseDoubleClick += LeftButton_MouseDoubleClick;

            // Gestión del Botón para pasar de la tabla Graficas a Exportar a las Graficas Creadas
            RightButton.MouseDoubleClick += RightButton_MouseDoubleClick;

            // Gestión del Botón Exportar Grafica
            ExportGraphicButton.Click += ExportGraphicButton_Click;
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
