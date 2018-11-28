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
using WinMaths.src.bean;
using WinMaths.src.viewModels;
using WinMaths.src.utils;

namespace WinMaths
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PreferencesMenuUI PreferencesMenuUIVar;
        private FunctionRepresentationUtils FunctionRepresentationVar;
        private ViewModel viewModel;

        FunctionRepresentationUtils.FuncRect real;
        FunctionRepresentationUtils.FuncRect screen;

        // A partir de una determinada grafica obtengo la representación de su polilinea
        private Dictionary<Graphic, Polyline> graphicRepresentationDictionary;
        private ScaleTransform scaleTransform;
        private Point _last;

        private double scaleRate;
        private bool added, isDragged;

        public MainWindow()
        {
            InitializeComponent();

            /* Variables globales */
            scaleTransform = new ScaleTransform();
            graphicRepresentationDictionary = null;
            scaleRate = 1.1;
            
            /* Evento de cierre de las ventanas */
            this.Closed += Window_Closed;

            // Instanciación de la clase FunctionRepresentationUtils
            FunctionRepresentationVar = new FunctionRepresentationUtils();

            // Gestión de la instancia del ViewModel
            viewModel = new ViewModel();
            viewModel.GraphicSetToDraw += ViewModel_GraphicSetToDraw;

            // Gestión del Grid que contiene al canvas
            GridOfCanvas.MouseMove += GridOfCanvas_MouseMove;
            GridOfCanvas.MouseLeftButtonUp += GridOfCanvas_MouseLeftButtonUp;
            GridOfCanvas.MouseLeftButtonDown += GridOfCanvas_MouseLeftButtonDown;

            // Gestión del Canvas de Representación
            this.SizeChanged += RepresentationCanvas_SizeChanged;
            RepresentationCanvas.MouseWheel += RepresentationCanvas_MouseWheel;
            RepresentationCanvas.MouseLeftButtonDown += RepresentationCanvas_MouseLeftButtonDown;

            // Gestión del Borde que contiene el canvas
            clipBorder.MouseEnter += ClipBorder_MouseEnter;
            clipBorder.MouseLeave += ClipBorder_MouseLeave;

            // Gestión de la instancia de la ventana del menú de preferencias
            PreferencesMenuUIVar = new PreferencesMenuUI();
            PreferencesMenuUIVar.SetViewModel(viewModel);
            PreferencesMenuUIVar.Closed += Window_Closed;
            PreferencesMenuUIVar.Show();
        }

        private void ViewModel_GraphicSetToDraw(object sender, ViewModelEventArgs e)
        {
            List<Graphic> listOfGraphicsToRepresent = (List<Graphic>)e.listOfGraphics;
            Polyline graphicRepresentation = new Polyline(); //<------------------------------ INstanciación innecesaria?????????????

            if(graphicRepresentationDictionary == null) // Para que no se dibuje nada en el sizeChanged
                graphicRepresentationDictionary = new Dictionary<Graphic, Polyline>();

            DrawAxisAndLines();
            if (listOfGraphicsToRepresent != null){
                foreach(Graphic g in listOfGraphicsToRepresent){

                    if (graphicRepresentationDictionary.ContainsKey(g) && g != null)
                        graphicRepresentation = graphicRepresentationDictionary[g];
                    else
                        graphicRepresentation = FunctionRepresentationVar.DrawGraphic(g, RepresentationCanvas.ActualWidth, RepresentationCanvas.ActualHeight);

                    AddPolylineToDictionary(graphicRepresentation, g);
                    RepresentationCanvas.Children.Add(graphicRepresentation);
                }
            }
        }

        private void AddPolylineToDictionary(Polyline value, Graphic key)
        {
            graphicRepresentationDictionary.Add(key, value);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void RepresentationCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RepresentationCanvas.Children.Clear();
            DrawAxisAndLines();
            if (graphicRepresentationDictionary != null) {
                List<Graphic> listOfGraphicsToRepresent = viewModel.GetListOfGraphicsVM();

                if (listOfGraphicsToRepresent != null)
                {
                    foreach (Graphic g in listOfGraphicsToRepresent)
                    {
                        RepresentationCanvas.Children.Add( graphicRepresentationDictionary[g] );
                    }
                }
            }

        }

        private void DrawAxisAndLines()
        {
            // <<>>>>>><<<<<<<<< aqui iran los limites de X e Y 
            real = FunctionRepresentationVar.DeclareFuncRect(-10, 10, -10, 10); // Real
            screen = FunctionRepresentationVar.DeclareFuncRect(0, RepresentationCanvas.ActualWidth, 0, RepresentationCanvas.ActualHeight); // Screen
            Boolean ejeHorizontal = true;
            Boolean ejeVertical = false;
            double distancia = 0.333;
            double limitX = RepresentationCanvas.ActualWidth;
            double limitY = RepresentationCanvas.ActualHeight;

            Console.WriteLine(limitX);

            // Eje X e Y
            foreach (Line l in FunctionRepresentationVar.DrawAxis(real, screen))
                RepresentationCanvas.Children.Add(l);

            // Lineas Eje X
            foreach (Line l in FunctionRepresentationVar.DrawAxisLines(screen, real, real.XMin, real.YMax, distancia, ejeHorizontal))
                RepresentationCanvas.Children.Add(l);

            // Lineas Eje Y
            foreach (Line l in FunctionRepresentationVar.DrawAxisLines(screen, real, real.XMin, real.YMax, distancia, ejeVertical))
                RepresentationCanvas.Children.Add(l);
        }

        private void RepresentationCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0){
                scaleTransform.ScaleX *= scaleRate;
                scaleTransform.ScaleY *= scaleRate;
            } else {
                scaleTransform.ScaleX /= scaleRate;
                scaleTransform.ScaleY /= scaleRate;
            }

            if (!added) {
                TransformGroup tg = (TransformGroup) RepresentationCanvas.RenderTransform;
                if (tg != null) {
                    tg.Children.Add(scaleTransform);
                    RepresentationCanvas.RenderTransformOrigin = new Point(0.5, 0.5);
                    added = true;
                }
            }
        }

        private void RepresentationCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Panel MousePanel = (Panel)sender;
            Point p = e.GetPosition(RepresentationCanvas);

            Console.WriteLine("Position Mouse Canvas X:{0} Y:{1}", p.X, p.Y);

            Console.WriteLine(FunctionRepresentationVar.ConvertXFromPantToReal(p.X, RepresentationCanvas.ActualWidth, screen, real));
            Console.WriteLine(FunctionRepresentationVar.ConvertYFromPantToReal(p.Y, RepresentationCanvas.ActualHeight, screen, real));
        }

        private void ClipBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void ClipBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Cross;
        }

        private void GridOfCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragged == false)
                return;

            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed && GridOfCanvas.IsMouseCaptured)
            {
                Point pos = e.GetPosition(GridOfCanvas);
                Matrix matrix = mt.Matrix;
                matrix.Translate(pos.X - _last.X, pos.Y - _last.Y);
                mt.Matrix = matrix;
                _last = pos;
            }

        }

        private void GridOfCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GridOfCanvas.ReleaseMouseCapture();
            isDragged = false;
        }

        private void GridOfCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GridOfCanvas.CaptureMouse();
            _last = e.GetPosition(GridOfCanvas);
            isDragged = true;
        }

    }
}
