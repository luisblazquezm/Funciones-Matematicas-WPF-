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
        private ViewModel viewModel; 
        private GraphicTableUI graphicTableUI;
        private GraphicDephinitionUI graphicDephinitionUI;

        public PreferencesMenuUI(ViewModel vM)
        {
            InitializeComponent();
            this.viewModel = vM;
        }

        private void Panel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Permite mover el panel sin necesidad de un estilo de ventana
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
            if (graphicTableUI == null)
                graphicTableUI = new GraphicTableUI(this.viewModel);
            
            PreferencesMenu.Content = graphicTableUI.Content;
        }

        private void GraphicDephinition_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (graphicDephinitionUI == null)
                graphicDephinitionUI = new GraphicDephinitionUI(this.viewModel);
            
            PreferencesMenu.Content = graphicDephinitionUI.Content;
        }
    }
}
