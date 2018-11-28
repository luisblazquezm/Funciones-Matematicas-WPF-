using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using WinMaths.src.bean;

namespace WinMaths.src.utils
{
    public class FunctionRepresentationUtils
    {
        public struct FuncRect
        {
            public double XMin, YMin, XMax, YMax;
        }

        public Line[] DrawAxis(FuncRect real, FuncRect screen)
        {
            Line[] arrayOfAxis = new Line[2];
            Line axisX = new Line
            {
                Stroke = Brushes.Black,

                X1 = 0,
                X2 = screen.XMax,
                Y1 = ConvertYFromRealToPant(0, 0, screen, real),
                Y2 = ConvertYFromRealToPant(0, 0, screen, real)
            };

            Line axisY = new Line
            {
                Stroke = Brushes.Black,

                Y1 = 0,
                Y2 = screen.YMax,
                X1 = ConvertXFromRealToPant(0, 0, screen, real),
                X2 = ConvertXFromRealToPant(0, 0, screen, real)
            };

            arrayOfAxis[0] = axisX;
            arrayOfAxis[1] = axisY;

            return arrayOfAxis;
        }

        public List<Line> DrawAxisLines(FuncRect screen, FuncRect real, double actualPos, double maxLimit, double distancia, Boolean axisHorizontal)
        {
            int counter = 6;
            List<Line> listOflines = new List<Line>();
            Line line = new Line { Stroke = Brushes.Black };
            double length1, length2;

            while (actualPos < maxLimit)
            {
                // Altura de la raya de -0.5 a 0.5
                if (counter == 6)
                {
                    length1 = -0.5;
                    length2 = 0.5;
                    counter = 0;
                }
                else
                {
                    length1 = -0.25;
                    length2 = 0.25;
                }

                if (axisHorizontal)
                {
                    line.X1 = ConvertXFromRealToPant(actualPos, screen.XMin, screen, real);
                    line.X2 = ConvertXFromRealToPant(actualPos, screen.XMin, screen, real);
                    line.Y1 = ConvertYFromRealToPant(length1, screen.YMin, screen, real);
                    line.Y2 = ConvertYFromRealToPant(length2, screen.YMin, screen, real);
                }
                else
                {
                    line.X1 = ConvertXFromRealToPant(length1, screen.XMin, screen, real);
                    line.X2 = ConvertXFromRealToPant(length2, screen.XMin, screen, real);
                    line.Y1 = ConvertYFromRealToPant(actualPos, screen.YMin, screen, real);
                    line.Y2 = ConvertYFromRealToPant(actualPos, screen.YMin, screen, real);
                }

                listOflines.Add(line);

                actualPos += distancia;
                line = new Line { Stroke = Brushes.Black };
                counter++;
            }

            return listOflines;
        }

        public Polyline DrawGraphic(Graphic g, double canvasWidth, double canvasHeight)
        {
            PointCollection points = new PointCollection();
            PointCollection[] listOfPoints = new PointCollection[2];
            Polyline graphicPolyline = new Polyline();
            double xReal, yReal, xScreen, yScreen;

            FuncRect real = DeclareFuncRect(-10, 10, -10, 10);//<<<<<<<<<<<<<<<<<< SUstituir esto por valores bien
            FuncRect screen = DeclareFuncRect(0, canvasWidth, 0, canvasHeight);
            int numberOfPoints = (int)screen.XMax;

            graphicPolyline.Stroke = new SolidColorBrush(g.GraphicColor);

            for (int i = 0; i <= numberOfPoints; i++) // OJOOOOOOOOOOOOOO Aqui he cambiado el < por <= para que llegue de -10 a 10 y no de -10 a 9.66 por ejemplo
            {
                xReal = real.XMin + i * (real.XMax - real.XMin) / numberOfPoints;
                yReal = g.Function.CalculateF(xReal);

                xScreen = ConvertXFromRealToPant(xReal, screen.XMin, screen, real);
                yScreen = ConvertYFromRealToPant(yReal, screen.YMin, screen, real);

                points.Add(new Point(xScreen, yScreen));
            }

            graphicPolyline.Points = new PointCollection(points);
            return graphicPolyline;

        }

        public double ConvertXFromRealToPant(double xreal, double width, FuncRect screen, FuncRect real)
        {
            return (screen.XMax - width) * ((xreal - real.XMin) / (real.XMax - real.XMin)) + screen.XMin;
        }

        public double ConvertYFromRealToPant(double yreal, double height, FuncRect screen, FuncRect real)
        {
            return (height - screen.YMax) * ((yreal - real.YMin) / (real.YMax - real.YMin)) + screen.YMax;
        }

        public double ConvertXFromPantToReal(double xpant, double width, FuncRect screen, FuncRect real)
        {
            return (real.XMax - real.XMin) * ((xpant - screen.XMin) / (screen.XMax - screen.XMin)) + real.XMin;
        }

        public double ConvertYFromPantToReal(double ypant, double height, FuncRect screen, FuncRect real)
        {
            return (real.YMax - real.YMin) * ((ypant - screen.YMax) / (screen.YMin - screen.YMax)) + real.YMin;
        }

        public FuncRect DeclareFuncRect(double x1, double x2, double y1, double y2)
        {
            FuncRect setPoints;

            setPoints.XMin = x1;
            setPoints.YMin = y1;
            setPoints.XMax = x2;
            setPoints.YMax = y2;

            return setPoints;
        }
    }
}
