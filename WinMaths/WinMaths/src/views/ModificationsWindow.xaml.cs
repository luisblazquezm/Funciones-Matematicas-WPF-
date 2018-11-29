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
using System.Windows.Shapes;
using WinMaths.src.bean;
using WinMaths.src.bean.function;
using WinMaths.src.viewModels;

namespace WinMaths.src.views
{
    /// <summary>
    /// Lógica de interacción para ModificationsWindow.xaml
    /// </summary>
    public partial class ModificationsWindow : Window
    {
        private ViewModel viewModel;
        private int idOldGraphic;

        public ModificationsWindow(ViewModel vM)
        {
            InitializeComponent();

            this.viewModel = vM;

            FunctionModComboBox.ItemsSource = InitializeFunctionComboBox();
            FunctionModComboBox.SelectionChanged += FunctionModComboBox_SelectionChanged;

            // Gestión TextBox del nombre de la gráfica y los parámetros
            NameModTextBox.TextChanged += ParametersTextBox_TextChanged;
            ParamAModTextBox.TextChanged += ParametersTextBox_TextChanged;
            ParamBModTextBox.TextChanged += ParametersTextBox_TextChanged;
            ParamCModTextBox.TextChanged += ParametersTextBox_TextChanged;

            // Gestión Botón de Confirmar Cambios
            ConfirmButton.IsEnabled = false;
            ConfirmButton.Click += ConfirmButton_Click;

            // Gestión Botón de Cancelar Modificar
            CancelButton.Click += CancelButton_Click;

            // Gestión WrapPanel que contiene la etiqueta y el TextBox del parametro C
            ParamCLabel.Visibility = Visibility.Hidden;
            ParamCModTextBox.Visibility = Visibility.Hidden;
        }

        public void SetGraphicParameters(Graphic g)
        {
            idOldGraphic = g.ID;
            NameModTextBox.Text = g.Name;
            FunctionModComboBox.SelectedItem = g.Function.Formula;
            ParamAModTextBox.Text = Convert.ToString(g.ParamA);
            ParamBModTextBox.Text = Convert.ToString(g.ParamB);
            if (SecondGradeFunction.GetFormula().Equals(g.Function.Formula)) {
                ParamCModTextBox.Visibility = Visibility.Visible;
                ParamCLabel.Visibility = Visibility.Visible;
            } else {
                ParamCModTextBox.Visibility = Visibility.Hidden;
                ParamCLabel.Visibility = Visibility.Hidden;
                ParamCModTextBox.Text = Convert.ToString(0);
            }
            ColorModColorPicker.SelectedColor = g.GraphicColor;
        }

        private String[] InitializeFunctionComboBox() // <------------------- Encapsularlo en la clase function
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

        private void FunctionModComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine("Cambiada fila en comboBox");
            DeactivateParameters();
        }

        private void DeactivateParameters()
        {
            ParamAModTextBox.Clear();
            ParamBModTextBox.Clear();
            ParamCModTextBox.Clear();

            ConfirmButton.IsEnabled = false;
            if (SecondGradeFunction.GetFormula().Equals((String)FunctionModComboBox.SelectedItem)){
                ParamCModTextBox.Visibility = Visibility.Visible;
                ParamCLabel.Visibility = Visibility.Visible;
            } else {
                ParamCModTextBox.Visibility = Visibility.Hidden;
                ParamCLabel.Visibility = Visibility.Hidden;
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            Graphic g = CreateNewGraphic(); // <------------------- Encapsularlo en la clase grafica --> un constructor
            if (g != null) {
                g.ID = idOldGraphic;
                bool result = viewModel.UpdateGraphicVM(g);
                Console.WriteLine("UPDATE {0}",result);
            }
            this.Close();

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ParametersTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ConfirmButton.IsEnabled == false && 
                NameModTextBox.Text.Length != 0 &&
                ParamAModTextBox.Text.Length != 0 &&
                ParamBModTextBox.Text.Length != 0 &&
                ( (!ParamCModTextBox.IsVisible && ParamCModTextBox.Text.Length == 0) || (ParamCModTextBox.IsVisible && ParamCModTextBox.Text.Length != 0) ) &&
                FunctionModComboBox.SelectedIndex != -1)
            {
                ConfirmButton.IsEnabled = true;
            } else {
                ConfirmButton.IsEnabled = false;
            }
                
        }

        private Graphic CreateNewGraphic()
        {
            String GraphicName = NameModTextBox.Text;
            String FunctionName = (String)FunctionModComboBox.SelectedItem;
            double paramA = double.Parse(ParamAModTextBox.Text);
            double paramB = double.Parse(ParamBModTextBox.Text);
            double paramC = 0;
            if (ParamCModTextBox.IsVisible)
                paramC = double.Parse(ParamCModTextBox.Text);
            Function f = SelectFunction(FunctionName, paramA, paramB, paramC); // <--------------------------- Encapsular en la clase funcion como constructor o como metodo
            Color GraphicColor = (Color)ColorModColorPicker.SelectedColor;

            return new Graphic(f, GraphicName, paramA, paramB, paramC, GraphicColor);
        }

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
    }
}
