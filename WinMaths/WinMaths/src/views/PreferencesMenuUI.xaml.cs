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
using WinMaths.src.viewModels;

namespace WinMaths.src.views
{
    /// <summary>
    /// Lógica de interacción para PreferencesMenuUI.xaml
    /// </summary>
    public partial class PreferencesMenuUI : Window
    {
        private ViewModel viewModel; /* ESTO ESTA MALLLLLL hay que pasarla directamente al GraphicDephinitonUI*/

        public PreferencesMenuUI(ViewModel viewModel) /* Ojooooooo no se si pasar el viewModel por aqui o de otra manera*/
        {
            InitializeComponent();
            this.viewModel = viewModel;
        }

        private void Panel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void GraphicTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PreferencesMenu.Content = new GraphicTableUI(this.viewModel);
        }

        private void Import_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PreferencesMenu.Content = new ImportUI();
        }

        private void GraphicDephinition_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            PreferencesMenu.Content = new GraphicDephinitionUI(viewModel);
        }

        private void Export_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PreferencesMenu.Content = new ExportUI();
        }
    }
}
