using System;
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
using WinMaths.src.viewModels;

namespace WinMaths.src.views
{
    /// <summary>
    /// Lógica de interacción para GraphicTableUI.xaml
    /// </summary>
    public partial class GraphicTableUI : Page
    {
        private ViewModel viewModel;
        private List<Graphic> listOfGraphicsToExport;

        public GraphicTableUI(ViewModel vM)
        {
            InitializeComponent();

            this.viewModel = vM;
            listOfGraphicsToExport = null;

            TableGrid.ItemsSource = viewModel.GetCollectionOfGraphicsVM(); // Puede que se esté recargando cada vez que se instancia el pages todo el rato y esté mal
            TableGrid.SelectedCellsChanged += TableGrid_SelectedCellsChanged;

            // Gestión del Botón Dibujar
            DrawGraphicButton.Click += DrawGraphicButton_Click;
            DrawGraphicButton.IsEnabled = false;

            // Gestión del Botón Modificar
            ModifyGraphicButton.Click += ModifyGraphicButton_Click;
            ModifyGraphicButton.IsEnabled = false;

            // Gestión del Botón Eliminar
            DeleteGraphicButton.Click += DeleteGraphicButton_Click;
            DeleteGraphicButton.IsEnabled = false;

            // Gestión del contextMenu
            ExportSelectedFilesMenuOption.Click += ExportFilesMenuOption_Click;
            ExportTableMenuOption.Click += ExportFilesMenuOption_Click;
            ImportTableMenuOption.Click += ImportTableMenuOption_Click;
        }

        private void TableGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (TableGrid.SelectedIndex != -1) {
                DrawGraphicButton.IsEnabled = true;
                ModifyGraphicButton.IsEnabled = true;
                DeleteGraphicButton.IsEnabled = true;
            } else {
                DrawGraphicButton.IsEnabled = false;
                ModifyGraphicButton.IsEnabled = false;
                DeleteGraphicButton.IsEnabled = false;
            }
        }

        /*
        public void SetViewModel(ViewModel vM)
        {
            this.viewModel = vM;
            TableGrid.ItemsSource = viewModel.GetCollectionOfGraphicsVM(); // Puede que se esté recargando cada vez que se instancia el pages todo el rato y esté mal
            viewModel.GraphicUpdated += ViewModel_GraphicUpdated;
        }
        */

        private void DrawGraphicButton_Click(object sender, RoutedEventArgs e)
        {
            List<Graphic> selectedGraphicList = TableGrid.SelectedItems.Cast<Graphic>().ToList();
            if (selectedGraphicList != null)
                viewModel.DrawGraphicVM(selectedGraphicList);
        }

        private void ModifyGraphicButton_Click(object sender, RoutedEventArgs e)
        {
            Graphic oldGraph = (Graphic)TableGrid.SelectedItem;
            int idOldGraphic = oldGraph.ID;

            ModificationsWindow modificationsWindow = new ModificationsWindow(){ GraphicToModify = oldGraph };

            modificationsWindow.ShowDialog(); // Modal
            if (false == modificationsWindow.GraphicChanged)
                return;

            Graphic graphModified = modificationsWindow.GraphicToModify;

            if (graphModified != null)
            {
                graphModified.ID = idOldGraphic;
                bool resultUpdate = viewModel.UpdateGraphicVM(graphModified, oldGraph);
                Console.WriteLine("UPDATE {0}", resultUpdate);
            }
        }

        private void DeleteGraphicButton_Click(object sender, RoutedEventArgs e)
        {
            List<Graphic> selectedGraphicList = TableGrid.SelectedItems.Cast<Graphic>().ToList();
            if (selectedGraphicList != null)
                viewModel.DeleteGraphicVM(selectedGraphicList);
        }

        private void ExportFilesMenuOption_Click(object sender, RoutedEventArgs e)
        {
            if (sender == ExportSelectedFilesMenuOption)
                listOfGraphicsToExport = TableGrid.SelectedItems.Cast<Graphic>().ToList();
            else if (sender == ExportTableMenuOption)
                listOfGraphicsToExport = TableGrid.Items.Cast<Graphic>().ToList();

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "NuevoArchivo",
                Filter = "XML | *.xml | Binary | *.bin | Excel | *.xlm"
            };

            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                WriteToBinaryFile<Graphic>(dlg.FileName, listOfGraphicsToExport);
            }
        }

        /// <summary>
        /// Writes the given object instance to a binary file.
        /// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
        /// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the binary file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the binary file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToBinaryFile<T>(string filePath, List<T> objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        private void ImportTableMenuOption_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Abrir fichero",
                FileName = "NuevoArchivo",
                DefaultExt = ".xml",
                Filter = "XML | *.xml | Binary | *.bin | Excel | *.xlm",
                Multiselect = false
            };

            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                List<Graphic> g = ReadFromBinaryFile<Graphic>(dlg.FileName);
                bool ja = viewModel.ImportListVM(g);
                Console.WriteLine("Importada {0}", ja);
            }
        }

        /// <summary>
        /// Reads an object instance from a binary file.
        /// </summary>
        /// <typeparam name="T">The type of object to read from the binary file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the binary file.</returns>
        public static List<T> ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (List<T>)binaryFormatter.Deserialize(stream);
            }
        }
    }
}
