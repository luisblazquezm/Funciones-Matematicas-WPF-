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

namespace WinMaths
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PreferencesMenuUI PreferencesMenuUIVar;
        //private Dictionary<Graphic, Polyline> graphicRepresentationList;    // A partir de una determinada grafica obtengo la representación de su polilinea
        /*
        private static Boolean entered = false;
        private const double ScaleRate = 1.1; // Cambiar el zoom
        private ScaleTransform scaleTransform = new ScaleTransform();      //---------> object for Scale-Transform //-------------> scaleRate that has to be Zoom for each point of Mouse_Wheel
        private bool added;
        private Point _last;
        private bool isDragged, isDragging;
        */

        public MainWindow()
        {
            InitializeComponent();
            
            this.Closed += Window_Closed;

            PreferencesMenuUIVar = new PreferencesMenuUI();
            PreferencesMenuUIVar.Closed += Window_Closed;
            PreferencesMenuUIVar.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /*
private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
{
   lienzo.Children.Clear();
   //if (entered)
       //DrawGraphic();
}

private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
{
   if (e.Delta > 0)
   {
       scaleTransform.ScaleX *= ScaleRate;
       scaleTransform.ScaleY *= ScaleRate;
   }
   else
   {
       scaleTransform.ScaleX /= ScaleRate;
       scaleTransform.ScaleY /= ScaleRate;
   }

   if (!added)
   {
       TransformGroup tg = lienzo.RenderTransform as TransformGroup;
       if (tg != null)
       {
           tg.Children.Add(scaleTransform);
           lienzo.RenderTransformOrigin = new Point(0.5, 0.5);
           added = true;
       }
   }
}

void theGrid_MouseMove(object sender, MouseEventArgs e)
{
   if (isDragged == false)
       return;

   base.OnMouseMove(e);
   if (e.LeftButton == MouseButtonState.Pressed && theGrid.IsMouseCaptured)
   {

       var pos = e.GetPosition(theGrid);
       var matrix = mt.Matrix;
       matrix.Translate(pos.X - _last.X, pos.Y - _last.Y);
       mt.Matrix = matrix;
       _last = pos;
   }

}

void theGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
{
   theGrid.ReleaseMouseCapture();
   isDragged = false;
}

void theGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
{
   theGrid.CaptureMouse();
   _last = e.GetPosition(theGrid);
   isDragged = true;
}
*/

    }
}
