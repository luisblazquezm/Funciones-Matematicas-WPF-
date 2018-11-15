using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WinMaths.src.views;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WinMaths
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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
            Main.Content = new GraphicTableUI();
        }

        private void Import_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new ImportUI();
        }

        private void GraphicDephinition_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new GraphicDephinitionUI();
        }

        private void Export_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new ExportUI();
        }
    }
}
