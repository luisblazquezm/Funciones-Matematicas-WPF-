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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinMaths.src.bean;
using WinMaths.src.viewModels;

namespace WinMaths.src.views
{
    /// <summary>
    /// Lógica de interacción para GraphicTableUI.xaml
    /// </summary>
    public partial class GraphicTableUI : Page
    {
        private ViewModel viewModel;

        public GraphicTableUI(ViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;

            // Gestión del Botón Dibujar
            DrawGraphicButton.Click += DrawGraphicButton_Click;

            // Gestión del Botón Modificar
            ModifyGraphicButton.Click += ModifyGraphicButton_Click;

            // Gestión del Botón Eliminar
            DeleteGraphicButton.Click += DeleteGraphicButton_Click;

            // Gestión del DataGrid de gráficas
            TableGrid.ItemsSource = viewModel.GetListOfGraphicsVM();

            
        }

        private void DrawGraphicButton_Click(object sender, RoutedEventArgs e)
        {
            List<Graphic> selectedGraphicList = TableGrid.SelectedItems.Cast<Graphic>().ToList();
            if (selectedGraphicList != null)
                viewModel.DrawGraphic(selectedGraphicList);
        }

        private void ModifyGraphicButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DeleteGraphicButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
