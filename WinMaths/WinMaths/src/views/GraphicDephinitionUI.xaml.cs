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
using WinMaths.src.bean;
using WinMaths.src.viewModels;

namespace WinMaths.src.views
{
    /// <summary>
    /// Lógica de interacción para GraphicDephinitionUI.xaml
    /// </summary>
    public partial class GraphicDephinitionUI : Page
    {
        //enum EnumFunctions { SinFunction , CosFunction , ExpFunction , FirstDegreeFunction , SecondDegreeFunction , FractionalFunction };
        static int lastSelectedIndex = -1;
        private ViewModel viewModel;
        Boolean nameTextBoxFlag , paramATextBoxFlag, paramBTextBoxFlag, paramCTextBoxFlag;

        public GraphicDephinitionUI()
        {
            InitializeComponent();

            /* Inicialización Variables Globales*/
            this.viewModel = new ViewModel(); /* OJOooooooooooooo esto puede estar mal */
            nameTextBoxFlag = true;
            paramATextBoxFlag = true;
            paramBTextBoxFlag = true;
            paramCTextBoxFlag = true;

            // Gestión ComboBox de Funciones
            FunctionComboBox.ItemsSource = InitializeFunctionComboBox();
            FunctionComboBox.SelectionChanged += FunctionComboBox_SelectionChanged;

            // Gestión TextBox del nombre de la gráfica y los parámetros
            NameTextBox.TextChanged += TextBox_TextChanged;
            ParamATextBox.TextChanged += TextBox_TextChanged;
            ParamBTextBox.TextChanged += TextBox_TextChanged;
            ParamCTextBox.TextChanged += TextBox_TextChanged;

            // Gestión Botón de añadir gráfica
            AddGraphicButton.Click += AddGraphicToTableGrid;

        }

        private void FunctionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int actualSelectedIndex = FunctionComboBox.SelectedIndex;

            if (lastSelectedIndex != actualSelectedIndex && lastSelectedIndex != -1)
                DeactivateGraphicDephinitionFields();

            if (/*El item seleccionado incluye una c */true)
                ParamCWrapPanel.Visibility = Visibility.Visible;
            //else
                //ParamCWrapPanel.Visibility = Visibility.Hidden;

            if (actualSelectedIndex != -1) {
                lastSelectedIndex = actualSelectedIndex;
                CalculateProgressBarValue(20);
            } 
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            /* EL color se puede coger el de for defecto*/
            CheckTextBoxProgress(sender);

            if (NameTextBox.Text.Length != 0 &&
                FunctionComboBox.SelectedIndex != -1 &&
                (
                (ParamATextBox.IsEnabled && ParamBTextBox.IsEnabled && !ParamCTextBox.IsEnabled && ParamATextBox.Text.Length != 0 && ParamBTextBox.Text.Length != 0) ||
                (ParamATextBox.IsEnabled && ParamCTextBox.IsEnabled && !ParamBTextBox.IsEnabled && ParamATextBox.Text.Length != 0 && ParamCTextBox.Text.Length != 0) ||
                (ParamATextBox.IsEnabled && ParamBTextBox.IsEnabled && ParamCTextBox.IsEnabled && ParamATextBox.Text.Length != 0 && ParamBTextBox.Text.Length != 0 && ParamCTextBox.Text.Length != 0)
                ))
            {
                AddGraphicButton.IsEnabled = true;
            }
            else
            {
                AddGraphicButton.IsEnabled = false;
            }
        }

        private String[] InitializeFunctionComboBox()
        {
            /* Meter un array con los nombres de la clase de diseño */
            return null;
        }

        private Graphic CreateNewGraphic()
        {
            String GraphicName = NameTextBox.Text;
            String Function = (String)FunctionComboBox.SelectedItem; /* OJOOOOOO esto podría no estar bien*/
            double paramA = double.Parse(ParamATextBox.Text);
            double paramB = double.Parse(ParamBTextBox.Text);
            double paramC = double.Parse(ParamCTextBox.Text);
            Color GraphicColor = (Color)ColorSelectionColorPicker.SelectedColor;

            return new Graphic(Function, GraphicName, paramA, paramB, paramC, GraphicColor);
        }

        private void CheckTextBoxProgress(object sender)
        {
            if (sender == NameTextBox)
            {
                if (NameTextBox.Text.Length == 0) {
                    nameTextBoxFlag = true;
                    CalculateProgressBarValue(-20);
                } else if (NameTextBox.Text.Length != 0 && nameTextBoxFlag) {
                    nameTextBoxFlag = false;
                    CalculateProgressBarValue(20);
                } else {
                    CalculateProgressBarValue(0);
                }
            }
            else if (sender == ParamATextBox)
            {
                if (ParamATextBox.Text.Length == 0) {
                    paramATextBoxFlag = true;
                    CalculateProgressBarValue(-20);
                } else if (ParamATextBox.Text.Length != 0 && paramATextBoxFlag) {
                    paramATextBoxFlag = false;
                    CalculateProgressBarValue(20);
                }
                else {
                    CalculateProgressBarValue(0);
                }    
            }
            else if (sender == ParamBTextBox)
            {
                if (ParamBTextBox.Text.Length == 0) {
                    paramBTextBoxFlag = true;
                    CalculateProgressBarValue(-20);
                } else if (ParamBTextBox.Text.Length != 0 && paramBTextBoxFlag) {
                    paramBTextBoxFlag = false;
                    CalculateProgressBarValue(20);
                } else {
                    CalculateProgressBarValue(0);
                }
            }
            else if (sender == ParamCTextBox)
            {
                if (ParamCTextBox.Text.Length == 0) {
                    paramCTextBoxFlag = true;
                    CalculateProgressBarValue(-20);
                } else if (ParamCTextBox.Text.Length != 0 && paramCTextBoxFlag) {
                    paramCTextBoxFlag = false;
                    CalculateProgressBarValue(20);
                } else {
                    CalculateProgressBarValue(0);
                }
            }
        }

        private void CalculateProgressBarValue(double value)
        {
            if ((value > 0 && progressBar.Value >= 0) || (value < 0 && (progressBar.Value - value) >= 0))
                progressBar.Value += value;

            if (progressBar.Value == progressBar.Maximum)
                GreenCheckIcon.Visibility = Visibility.Visible;
        }

        private void AddGraphicToTableGrid(object sender, RoutedEventArgs e)
        {
            Graphic g = CreateNewGraphic();
            if (g != null)
                viewModel.AddGraphicVM(g);
        }

        private void DeactivateGraphicDephinitionFields()
        {
            NameTextBox.Clear();
            FunctionComboBox.SelectedIndex = -1;
            ParamATextBox.Clear();
            ParamBTextBox.Clear();
            ParamCTextBox.Clear();
            progressBar.Value = 0;
        }
    }
}
