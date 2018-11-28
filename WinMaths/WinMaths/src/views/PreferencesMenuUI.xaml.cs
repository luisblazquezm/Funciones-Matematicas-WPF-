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
using WinMaths.src.bean;
using WinMaths.src.bean.function;
using WinMaths.src.viewModels;

namespace WinMaths.src.views
{
    /// <summary>
    /// Lógica de interacción para PreferencesMenuUI.xaml
    /// </summary>
    public partial class PreferencesMenuUI : Window
    {
        private static int lastSelectedIndex = -1;
        private ViewModel viewModel;
        private Boolean nameTextBoxFlag, paramATextBoxFlag, paramBTextBoxFlag, paramCTextBoxFlag;

        public PreferencesMenuUI() /* Ojooooooo no se si pasar el viewModel por aqui o de otra manera*/
        {
            InitializeComponent();
            
            /* Inicialización Variables Globales*/
            nameTextBoxFlag = false;
            paramATextBoxFlag = false;
            paramBTextBoxFlag = false;
            paramCTextBoxFlag = false;

            // Gestión ComboBox de Funciones
            FunctionComboBox.ItemsSource = InitializeFunctionComboBox();
            FunctionComboBox.SelectionChanged += FunctionComboBox_SelectionChanged;

            // Gestión TextBox del nombre de la gráfica y los parámetros
            NameTextBox.TextChanged += CheckTextBoxValues_ToEnableAddGraphic;
            ParamATextBox.TextChanged += CheckTextBoxValues_ToEnableAddGraphic;
            ParamBTextBox.TextChanged += CheckTextBoxValues_ToEnableAddGraphic;
            ParamCTextBox.TextChanged += CheckTextBoxValues_ToEnableAddGraphic;

            // Gestión Botón de añadir gráfica
            AddGraphicButton.IsEnabled = false;
            AddGraphicButton.Click += AddGraphicToTableGrid;

            // Gestión Botón Verde de Progreso Completado
            GreenCheckIcon.Visibility = Visibility.Hidden;

            // Gestión WrapPanel que contiene la etiqueta y el TextBox del parametro C
            ParamCWrapPanel.Visibility = Visibility.Hidden;

            /* TABLAS */
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

            /* EXPORTAR */

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

            /* IMPORTAR */

        }

        public void SetViewModel(ViewModel vM)
        {
            this.viewModel = vM;
            TableGrid.ItemsSource = viewModel.GetCollectionOfGraphicsVM();
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
            PreferencesMenuTabControl.SelectedIndex = 1;
        }

        private void Import_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PreferencesMenuTabControl.SelectedIndex = 2;
        }

        private void GraphicDephinition_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PreferencesMenuTabControl.SelectedIndex = 0;
        }

        private void Export_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PreferencesMenuTabControl.SelectedIndex = 3;
        }

        /*==================================== GRAPHIC DEPHINITION ==================================== */

        private void FunctionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int actualSelectedIndex = FunctionComboBox.SelectedIndex;

            if (lastSelectedIndex != actualSelectedIndex && lastSelectedIndex != -1)
                DeactivateGraphicDephinitionFields();

            if (SecondGradeFunction.Formula.Equals((String)FunctionComboBox.SelectedItem))
                ParamCWrapPanel.Visibility = Visibility.Visible;
            else
                ParamCWrapPanel.Visibility = Visibility.Hidden;

            if (actualSelectedIndex != -1)
            {
                lastSelectedIndex = actualSelectedIndex;
                CalculateProgressBarValue(20);
            }
        }

        private void CheckTextBoxValues_ToEnableAddGraphic(object sender, TextChangedEventArgs e)
        {
            /* EL color se puede coger el de for defecto*/
            CheckTextBoxProgress(sender);

            if (NameTextBox.Text.Length != 0 &&
                FunctionComboBox.SelectedIndex != -1 &&
                (
                (ParamATextBox.IsEnabled && ParamBTextBox.IsEnabled && !ParamCTextBox.IsVisible && ParamATextBox.Text.Length != 0 && ParamBTextBox.Text.Length != 0) ||
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
            String[] functionList = {
                CosXFunction.Formula,
                SenXFunction.Formula,
                ExponentialFunction.Formula,
                FirstGradeFunction.Formula,
                SecondGradeFunction.Formula,
                FractionalFunction.Formula
            };

            return functionList;
        }

        private Graphic CreateNewGraphic()
        {
            String GraphicName = NameTextBox.Text;
            String FunctionName = (String)FunctionComboBox.SelectedItem;
            double paramA = double.Parse(ParamATextBox.Text);
            double paramB = double.Parse(ParamBTextBox.Text);
            double paramC = 0;
            if (ParamCTextBox.IsVisible)
                paramC = double.Parse(ParamCTextBox.Text);
            Function f = SelectFunction(FunctionName, paramA, paramB, paramC);
            Color GraphicColor = (Color)ColorSelectionColorPicker.SelectedColor;

            return new Graphic(f, GraphicName, paramA, paramB, paramC, GraphicColor);
        }

        private Function SelectFunction(string functionName, double a, double b, double c)
        {
            if (CosXFunction.Formula.Equals(functionName))
                return new CosXFunction(a, b);
            else if (SenXFunction.Formula.Equals(functionName))
                return new SenXFunction(a, b);
            else if (ExponentialFunction.Formula.Equals(functionName))
                return new ExponentialFunction(a, b);
            else if (FirstGradeFunction.Formula.Equals(functionName))
                return new FirstGradeFunction(a, b);
            else if (SecondGradeFunction.Formula.Equals(functionName))
                return new SecondGradeFunction(a, b, c);
            else if (FractionalFunction.Formula.Equals(functionName))
                return new FractionalFunction(a, b);

            return null;
        }

        private void CheckTextBoxProgress(object sender)
        {
            if (sender == NameTextBox)
            {
                if (NameTextBox.Text.Length == 0 && !nameTextBoxFlag)
                {
                    nameTextBoxFlag = true;
                    CalculateProgressBarValue(-20);
                }
                else if (NameTextBox.Text.Length != 0 && nameTextBoxFlag)
                {
                    nameTextBoxFlag = false;
                    CalculateProgressBarValue(20);
                }
                else
                {
                    CalculateProgressBarValue(0);
                }
            }
            else if (sender == ParamATextBox)
            {
                if (ParamATextBox.Text.Length == 0 && !paramATextBoxFlag)
                {
                    paramATextBoxFlag = true;
                    CalculateProgressBarValue(-20);
                }
                else if (ParamATextBox.Text.Length != 0 && paramATextBoxFlag)
                {
                    paramATextBoxFlag = false;
                    CalculateProgressBarValue(20);
                }
                else
                {
                    CalculateProgressBarValue(0);
                }
            }
            else if (sender == ParamBTextBox)
            {
                if (ParamBTextBox.Text.Length == 0 && !paramBTextBoxFlag)
                {
                    paramBTextBoxFlag = true;
                    CalculateProgressBarValue(-20);
                }
                else if (ParamBTextBox.Text.Length != 0 && paramBTextBoxFlag)
                {
                    paramBTextBoxFlag = false;
                    CalculateProgressBarValue(20);
                }
                else
                {
                    CalculateProgressBarValue(0);
                }
            }
            else if (sender == ParamCTextBox)
            {
                if (ParamCTextBox.Text.Length == 0 && !paramCTextBoxFlag)
                {
                    paramCTextBoxFlag = true;
                    CalculateProgressBarValue(-20);
                }
                else if (ParamCTextBox.Text.Length != 0 && paramCTextBoxFlag)
                {
                    paramCTextBoxFlag = false;
                    CalculateProgressBarValue(20);
                }
                else
                {
                    CalculateProgressBarValue(0);
                }
            }
        }

        private void CalculateProgressBarValue(double value)
        {
            Console.WriteLine("Valor actual {0} - {1}", progressBar.Value, value);
            if ((value > 0 && progressBar.Value >= 0) || (value < 0 && (progressBar.Value - value) >= 0))
                progressBar.Value += value;

            if (progressBar.Value == progressBar.Maximum)
                GreenCheckIcon.Visibility = Visibility.Visible;
        }

        private void AddGraphicToTableGrid(object sender, RoutedEventArgs e)
        {
            Graphic g = CreateNewGraphic();
            if (g != null)
            {
                DeactivateGraphicDephinitionFields();
                int result = viewModel.AddGraphicVM(g);
            }

        }

        private void DeactivateGraphicDephinitionFields()
        {
            NameTextBox.Clear();
            FunctionComboBox.SelectedIndex = -1;
            ParamATextBox.Clear();
            ParamBTextBox.Clear();
            ParamCTextBox.Clear();

            progressBar.Value = 0;

            nameTextBoxFlag = false;
            paramATextBoxFlag = false;
            paramBTextBoxFlag = false;
            paramCTextBoxFlag = false;

            AddGraphicButton.IsEnabled = false;
            GreenCheckIcon.Visibility = Visibility.Hidden;
            ParamCWrapPanel.Visibility = Visibility.Hidden;
        }

        /*==================================== TABLE DEPHINITION ==================================== */

        private void TableGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (TableGrid.SelectedIndex != -1)
            {
                DrawGraphicButton.IsEnabled = true;
                ModifyGraphicButton.IsEnabled = true;
                DeleteGraphicButton.IsEnabled = true;
            }
            else
            {
                DrawGraphicButton.IsEnabled = false;
                ModifyGraphicButton.IsEnabled = false;
                DeleteGraphicButton.IsEnabled = false;
            }
        }

        private void DrawGraphicButton_Click(object sender, RoutedEventArgs e)
        {
            List<Graphic> selectedGraphicList = TableGrid.SelectedItems.Cast<Graphic>().ToList();
            if (selectedGraphicList != null)
                viewModel.DrawGraphicVM(selectedGraphicList);
        }

        private void ModifyGraphicButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DeleteGraphicButton_Click(object sender, RoutedEventArgs e)
        {
            List<Graphic> selectedGraphicList = TableGrid.SelectedItems.Cast<Graphic>().ToList();
            if (selectedGraphicList != null)
                viewModel.DeleteGraphicVM(selectedGraphicList);
        }

        /*==================================== EXPORT DEPHINITION ==================================== */

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

        /*==================================== IMPORT DEPHINITION ==================================== */
    }
}
