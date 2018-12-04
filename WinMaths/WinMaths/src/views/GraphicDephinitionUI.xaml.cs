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
using WinMaths.src.bean.function;
using WinMaths.src.viewModels;

namespace WinMaths.src.views
{
    /// <summary>
    /// Lógica de interacción para GraphicDephinitionUI.xaml
    /// </summary>
    public partial class GraphicDephinitionUI : Page
    {
        private static int lastSelectedIndex = -1;
        private ViewModel viewModel;
        private Boolean nameTextBoxFlag , paramATextBoxFlag, paramBTextBoxFlag, paramCTextBoxFlag;

        public GraphicDephinitionUI(ViewModel vM)
        {
            InitializeComponent();

            this.viewModel = vM;

            /* Inicialización Variables Globales*/
            nameTextBoxFlag = false;
            paramATextBoxFlag = true;
            paramBTextBoxFlag = true;
            paramCTextBoxFlag = true;

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

        }

        private void FunctionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int actualSelectedIndex = FunctionComboBox.SelectedIndex;

            if (lastSelectedIndex != actualSelectedIndex && lastSelectedIndex != -1)
                DeactivateGraphicDephinitionFields();

            if (SecondGradeFunction.GetFormula().Equals((String)FunctionComboBox.SelectedItem))
                ParamCWrapPanel.Visibility = Visibility.Visible;
            else
                ParamCWrapPanel.Visibility = Visibility.Hidden;

            if (actualSelectedIndex != -1) {
                lastSelectedIndex = actualSelectedIndex;
                CalculateProgressBarValue(20);
            } 
        }

        private void CheckTextBoxValues_ToEnableAddGraphic(object sender, TextChangedEventArgs e)
        {
            /* Calculo el Campo que se está modificando para aumentar el valor de la barra de progreso */
            CheckTextBoxProgress(sender);

            /* EL color se puede coger el de for defecto*/
            if (NameTextBox.Text.Length != 0 &&
                FunctionComboBox.SelectedIndex != -1 &&
                (
                (ParamATextBox.IsEnabled && ParamBTextBox.IsEnabled && !ParamCTextBox.IsVisible && ParamATextBox.Text.Length != 0 && ParamBTextBox.Text.Length != 0) ||
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

        /* No encapsulo este metodo en la clase Function porque romperia con la abstracción de la clase y todas las subclases de esta heredarian este método. */
        private String[] InitializeFunctionComboBox() 
        {
            String[] functionList = {
                CosXFunction.GetFormula(),
                SenXFunction.GetFormula(),
                ExponentialFunction.GetFormula(),
                FirstGradeFunction.GetFormula(),
                SecondGradeFunction.GetFormula(),
                FractionalFunction.GetFormula()
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

        /* No encapsulo este metodo en la clase Function porque romperia con la abstracción de la clase y todas las subclases de esta heredarian este método. */
        private Function SelectFunction(string functionName, double a, double b, double c)
        {
            if (CosXFunction.GetFormula().Equals(functionName))
                return new CosXFunction(a, b);
            else if (SenXFunction.GetFormula().Equals(functionName))
                return new SenXFunction(a, b);
            else if (ExponentialFunction.GetFormula().Equals(functionName))
                return new ExponentialFunction(a, b);
            else if (FirstGradeFunction.GetFormula().Equals(functionName))
                return new FirstGradeFunction(a, b);
            else if (SecondGradeFunction.GetFormula().Equals(functionName))
                return new SecondGradeFunction(a, b, c);
            else if (FractionalFunction.GetFormula().Equals(functionName))
                return new FractionalFunction(a, b);

            return null;
        }

        private void CheckTextBoxProgress(object sender)
        {
            if (sender == NameTextBox)
            {
                if (NameTextBox.Text.Length == 0 && !nameTextBoxFlag) {
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
                if (ParamATextBox.Text.Length == 0 && !paramATextBoxFlag) {
                    paramATextBoxFlag = true;
                    if (ParamCTextBox.IsVisible)
                        CalculateProgressBarValue(-20);
                    else
                        CalculateProgressBarValue(-30);
                } else if (ParamATextBox.Text.Length != 0 && paramATextBoxFlag) {
                    paramATextBoxFlag = false;
                    if (ParamCTextBox.IsVisible)
                        CalculateProgressBarValue(20);
                    else
                        CalculateProgressBarValue(30);
                }
                else {
                    CalculateProgressBarValue(0);
                }    
            }
            else if (sender == ParamBTextBox)
            {
                if (ParamBTextBox.Text.Length == 0 && !paramBTextBoxFlag) {
                    paramBTextBoxFlag = true;
                    if(ParamCTextBox.IsVisible)
                        CalculateProgressBarValue(-20);
                    else
                        CalculateProgressBarValue(-30);
                } else if (ParamBTextBox.Text.Length != 0 && paramBTextBoxFlag) {
                    paramBTextBoxFlag = false;
                    if (ParamCTextBox.IsVisible)
                        CalculateProgressBarValue(20);
                    else
                        CalculateProgressBarValue(30);
                } else {
                    CalculateProgressBarValue(0);
                }
            }
            else if (sender == ParamCTextBox)
            {
                if (ParamCTextBox.Text.Length == 0 && !paramCTextBoxFlag) {
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
            else
                GreenCheckIcon.Visibility = Visibility.Hidden;
        }

        private void AddGraphicToTableGrid(object sender, RoutedEventArgs e)
        {
            Graphic g = CreateNewGraphic(); 
            if (g != null) {
                FunctionComboBox.SelectedIndex = -1;
                DeactivateGraphicDephinitionFields();
                int result = viewModel.AddGraphicVM(g);
            }
                
        }

        private void DeactivateGraphicDephinitionFields()
        {
            NameTextBox.Clear();
            ParamATextBox.Clear();
            ParamBTextBox.Clear();
            ParamCTextBox.Clear();

            progressBar.Value = 0;

            nameTextBoxFlag = true;
            paramATextBoxFlag = true;
            paramBTextBoxFlag = true;
            paramCTextBoxFlag = true;

            AddGraphicButton.IsEnabled = false;
            GreenCheckIcon.Visibility = Visibility.Hidden;
            ParamCWrapPanel.Visibility = Visibility.Hidden;
        }
    }
}
