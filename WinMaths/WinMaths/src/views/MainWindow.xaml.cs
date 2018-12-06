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
using System.IO;
using WinMaths.src.bean.function;

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
        private IOUtils ioUtils;

        private FuncRect real;
        private FuncRect screen;

        // A partir de una determinada grafica obtengo la representación de su polilinea (SOlo contiene las graficas representadas en el canvas)
        private Dictionary<Graphic, List<Polyline>> graphicRepresentationDictionary; 
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
            this.ioUtils = new IOUtils();

            /* Evento de cierre de las ventanas */
            this.Closed += Window_Closed;

            // Instanciación de la clase FunctionRepresentationUtils
            FunctionRepresentationVar = new FunctionRepresentationUtils();

            // Gestión de la instancia del ViewModel
            viewModel = new ViewModel();
            viewModel.GraphicSetToDraw += ViewModel_GraphicSetToDraw;
            viewModel.GraphicDeleted += ViewModel_GraphicDeleted;
            viewModel.GraphicUpdated += ViewModel_GraphicUpdated;
            viewModel.GraphicRepresentationUpdated += ViewModel_GraphicRepresentationUpdated;

            // Gestión del Grid que contiene al canvas
            GridOfCanvas.MouseMove += MouseMove_DragCanvas;
            GridOfCanvas.MouseLeftButtonUp += GridOfCanvas_MouseLeftButtonUp;
            GridOfCanvas.MouseLeftButtonDown += GridOfCanvas_MouseLeftButtonDown;

            // Gestión del Canvas de Representación
            this.SizeChanged += RepresentationCanvas_SizeChanged;
            RepresentationCanvas.MouseWheel += MouseWheel_Zoom;
            RepresentationCanvas.MouseMove += MouseMove_SetCanvasCoordinates;

            // Gestión del botón del Menú contextual para Exportar el canvas a imagen
            ExportMenuOption.Click += ExportButton_Click;

            // Gestión del botón de la ventana de Ajustes de Límites
            SettingsButton.Click += SettingsButton_Click;

            // Gestión del Borde que contiene el canvas
            clipBorder.MouseEnter += ClipBorder_MouseEnter;
            clipBorder.MouseLeave += ClipBorder_MouseLeave;

            // Gestión de la instancia de la ventana del menú de preferencias
            PreferencesMenuUIVar = new PreferencesMenuUI(viewModel);
            PreferencesMenuUIVar.Closed += Window_Closed;
            PreferencesMenuUIVar.Show();
        }

        private void ViewModel_GraphicSetToDraw(object sender, ViewModelEventArgs e)
        {
            PointCollection[] graphicRepresentation = null;
            List<Polyline> listOfLines = null;
            List<Graphic> listOfGraphicsToRepresent = (List<Graphic>)e.ListOfGraphics;
            FuncRect funcR = viewModel.FuncRect;

            RepresentationCanvas.Children.Clear();
            DrawAxisAndLines();

            if (graphicRepresentationDictionary == null) // Para que no se dibuje nada en el sizeChanged
                graphicRepresentationDictionary = new Dictionary<Graphic, List<Polyline>>();

            graphicRepresentationDictionary.Clear();

            foreach (Graphic g in listOfGraphicsToRepresent)
            {
                if (graphicRepresentationDictionary.ContainsKey(g) && g != null)
                {
                    listOfLines = graphicRepresentationDictionary[g];
                    foreach (Polyline line in listOfLines)
                        RepresentationCanvas.Children.Add(line);
                }
                else
                {
                    listOfLines = new List<Polyline>();

                    graphicRepresentation = FunctionRepresentationVar.DrawGraphic(g, RepresentationCanvas.ActualWidth, RepresentationCanvas.ActualHeight, funcR);
                    foreach (PointCollection p in graphicRepresentation)
                    {
                        Polyline line = new Polyline()
                        {
                            Points = p,
                            Stroke = new SolidColorBrush(g.GraphicColor)
                        };
                        listOfLines.Add(line);
                    }
                    Console.WriteLine("Añadida grafica {0}", g.Name);
                    graphicRepresentationDictionary.Add(g, listOfLines);

                    foreach (Polyline line in listOfLines)
                        RepresentationCanvas.Children.Add(line);

                }
            }

        }

        private void ViewModel_GraphicDeleted(object sender, ViewModelEventArgs e)
        {
            List<Graphic> listOfGraphicsToRemove = (List<Graphic>)e.ListOfGraphics;
            List<Polyline> listOfPolylines = null;

            if (graphicRepresentationDictionary != null)
            {
                foreach (Graphic g in listOfGraphicsToRemove)
                {
                    if (graphicRepresentationDictionary.ContainsKey(g) && g != null)
                    {
                        listOfPolylines = graphicRepresentationDictionary[g];
                        graphicRepresentationDictionary.Remove(g);
                        foreach (Polyline p in listOfPolylines)
                            RepresentationCanvas.Children.Remove(p);
                    }
                }
            }
            
        }

        private void ViewModel_GraphicUpdated(object sender, ViewModelEventArgs e)
        {
            List<Graphic> listOfGraphicsToUpdate = (List<Graphic>)e.ListOfGraphics; // Solo va a haber una grafica modificada

            if (graphicRepresentationDictionary != null)
            {
                DrawGraphicsInList(listOfGraphicsToUpdate);
            }
        }

        private void ViewModel_GraphicRepresentationUpdated(object sender, ViewModelEventArgs e)
        {
            List<Graphic> listOfGraphics = graphicRepresentationDictionary.Keys.ToList<Graphic>();

            graphicRepresentationDictionary.Clear();
            RepresentationCanvas.Children.Clear();
            DrawAxisAndLines();

            if (graphicRepresentationDictionary != null)
            {
                DrawGraphicsInList(listOfGraphics);
            }
        }

        private void RepresentationCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            List<Graphic> listOfGraphicsToRepresent = null;

            RepresentationCanvas.Children.Clear();
            DrawAxisAndLines();

            if (graphicRepresentationDictionary != null)
            {
                listOfGraphicsToRepresent = graphicRepresentationDictionary.Keys.ToList<Graphic>();

                graphicRepresentationDictionary.Clear();

                if (listOfGraphicsToRepresent != null)
                {
                    DrawGraphicsInList(listOfGraphicsToRepresent);
                }
            }

        }

        private void DrawGraphicsInList(List<Graphic> graphicList)
        {
            PointCollection[] graphicRepresentation = null;
            List<Polyline> listOfLines = null;
            FuncRect funcR = viewModel.FuncRect;

            foreach (Graphic g in graphicList)
            {
                listOfLines = new List<Polyline>();

                graphicRepresentation = FunctionRepresentationVar.DrawGraphic(g, RepresentationCanvas.ActualWidth, RepresentationCanvas.ActualHeight, funcR);
                foreach (PointCollection p in graphicRepresentation)
                {
                    Polyline line = new Polyline()
                    {
                        Points = p,
                        Stroke = new SolidColorBrush(g.GraphicColor)
                    };
                    listOfLines.Add(line);
                }
                graphicRepresentationDictionary.Add(g, listOfLines);

                foreach (Polyline line in listOfLines)
                    RepresentationCanvas.Children.Add(line);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void DrawAxisAndLines()
        {
            // Valores minimos y máximo reales del Canvas
            real = FunctionRepresentationVar.DeclareFuncRect(-10, 10, -10, 10);
            // Valores minimos y máximo de pantalla del Canvas
            screen = FunctionRepresentationVar.DeclareFuncRect(0, RepresentationCanvas.ActualWidth, 0, RepresentationCanvas.ActualHeight); 
            Boolean ejeHorizontal = true;
            Boolean ejeVertical = false;
            double distancia = 0.333; //0.333
            int numBlock = -6;
            int count = 5;
            double limitX = RepresentationCanvas.ActualWidth;
            double limitY = RepresentationCanvas.ActualHeight;

            // Eje X e Y
            foreach (Line l in FunctionRepresentationVar.DrawAxis(real, screen))
                RepresentationCanvas.Children.Add(l);

            // Lineas Eje X
            foreach (Line l in FunctionRepresentationVar.DrawAxisLines(screen, real, real.XMin, real.YMax, distancia, ejeHorizontal)) {
                RepresentationCanvas.Children.Add(l);
                ++count;
                if (count == 6) ++numBlock;
                count = DrawNumberLines(l, numBlock, ejeHorizontal, count);
            }

            // Numero por el que empieza desde el límite del canvas
            numBlock = -6;

            // Lineas Eje Y
            foreach (Line l in FunctionRepresentationVar.DrawAxisLines(screen, real, real.XMin, real.YMax, distancia, ejeVertical)) {
                RepresentationCanvas.Children.Add(l);
                ++count;
                if (count == 6) ++numBlock;
                count = DrawNumberLines(l, numBlock, ejeVertical, count);
            }
        }

        private int DrawNumberLines(Line l, int numBlock, Boolean eje, int count)
        {
            if (count == 6)
            {
                TextBlock tb = FunctionRepresentationVar.DrawNumberInLines(l, numBlock, eje);
                RepresentationCanvas.Children.Add(tb);
                count = 0;
            }

            return count;
        }

        private void MouseWheel_Zoom(object sender, MouseWheelEventArgs e)
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

        private void MouseMove_SetCanvasCoordinates(object sender, MouseEventArgs e)
        {
            Panel MousePanel = (Panel)sender;
            Point p = e.GetPosition(RepresentationCanvas);

            XCoordLabel.Content = FunctionRepresentationVar.ConvertXFromPantToReal(p.X, RepresentationCanvas.ActualWidth, screen, real);
            YCoordLabel.Content = FunctionRepresentationVar.ConvertYFromPantToReal(p.Y, RepresentationCanvas.ActualHeight, screen, real);
        }

        private void ClipBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void ClipBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Cross;
        }

        private void MouseMove_DragCanvas(object sender, MouseEventArgs e)
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

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "NuevaImagen",
                Filter = "JPeg Image | *.jpg | Bitmap Image | *.bmp | PNG | *.png | Gif Image | *.gif"
            };

            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                ioUtils.SaveToImage(RepresentationCanvas, dlg.FileName);
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            FuncRect funcRect = viewModel.FuncRect;

            RepresentationLimitsWindow representatitonLimits = new RepresentationLimitsWindow()
            {
                Xmin = funcRect.XMin,
                Xmax = funcRect.XMax,
                Ymin = funcRect.YMin,
                Ymax = funcRect.YMax
            };

            representatitonLimits.ShowDialog();
            if (false == representatitonLimits.DialogResult)
                return;

            funcRect.XMin = representatitonLimits.Xmin;
            funcRect.XMax = representatitonLimits.Xmax;
            funcRect.YMin = representatitonLimits.Ymin;
            funcRect.YMax = representatitonLimits.Ymax;

            viewModel.FuncRect = funcRect;
        }
    }
}
