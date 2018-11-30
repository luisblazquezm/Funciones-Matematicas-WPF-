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
using WinMaths.src.bean;
using WinMaths.src.utils;
using WinMaths.src.viewModels;

namespace WinMaths.src.views
{
    /// <summary>
    /// Lógica de interacción para ExportUI.xaml
    /// </summary>
    public partial class ExportUI : Page
    {
        //private IOUtils IOUtilsVar;
        private List<Graphic> copyOfListOfGraphics, listOfGraphicsToExport;
        private ViewModel viewModel;

        public ExportUI(ViewModel vM)
        {
            InitializeComponent();

            this.viewModel = vM;
            //viewModel.GraphicAdded += ViewModel_ListOfGraphicsChanged;
            viewModel.GraphicDeleted += ViewModel_ListOfGraphicsChanged;

            copyOfListOfGraphics = null;
            listOfGraphicsToExport = null;
            Console.WriteLine("Entre en Export");
            // Gestión del Botón para pasar de la tabla Graficas Creadas a las Graficas a Exportar
            LeftButton.MouseDoubleClick += LeftButton_MouseDoubleClick;
            
            // Gestión del Botón para pasar de la tabla Graficas a Exportar a las Graficas Creadas
            RightButton.MouseDoubleClick += RightButton_MouseDoubleClick;

            // Gestión del Botón Exportar Grafica
            //ExportGraphicButton.Click += ExportGraphicButton_Click;
        }

        private void ViewModel_ListOfGraphicsChanged(object sender, ViewModelEventArgs e)/* Esto podria no estar bien, se podría clonar?????? */
        {
            copyOfListOfGraphics = viewModel.GetListOfGraphicsVM();
            CreatedGraphicsTableGrid.ItemsSource = copyOfListOfGraphics;
        }

        private void LeftButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            copyOfListOfGraphics.AddRange(GraphicsToExportTableGrid.SelectedItems.Cast<Graphic>().ToList());
        }

        private void RightButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            listOfGraphicsToExport.AddRange(CreatedGraphicsTableGrid.SelectedItems.Cast<Graphic>().ToList());
            GraphicsToExportTableGrid.ItemsSource = listOfGraphicsToExport;
        }

        

    }
}
