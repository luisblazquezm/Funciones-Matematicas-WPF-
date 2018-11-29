using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using WinMaths.src.bean;
using WinMaths.src.bean.function;

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

        public PointCollection[] DrawGraphic(Graphic g, double canvasWidth, double canvasHeight)
        {
            PointCollection points = new PointCollection();
            Polyline graphicPolyline = new Polyline();
            double xReal, yReal, xScreen, yScreen;

            FuncRect real = DeclareFuncRect(-10, 10, -10, 10);//<<<<<<<<<<<<<<<<<< SUstituir esto por valores bien
            FuncRect screen = DeclareFuncRect(0, canvasWidth, 0, canvasHeight);
            int numberOfPoints = (int)screen.XMax;

            int i = 0;
            int j = 0;
            int limit = 0;

            if (g.Function.Formula.Equals(ExponentialFunction.GetFormula()) && g.ParamB < 0 ||
                            g.Function.Formula.Equals(FractionalFunction.GetFormula()))
            {
                PointCollection[] listOfPoints = new PointCollection[2];
                while (j < 2)
                {

                    do
                    {
                        i++;
                        xReal = real.XMin + i * (real.XMax - real.XMin) / numberOfPoints;
                        yReal = g.Function.CalculateF(xReal);

                        xScreen = ConvertXFromRealToPant(xReal, screen.XMin, screen, real);
                        yScreen = ConvertYFromRealToPant(yReal, screen.YMin, screen, real);

                        points.Add(new Point(xScreen, yScreen));

                    } while (Convert.ToInt32(xReal) < limit);

                    listOfPoints[j] = points;
                    points = new PointCollection();
                    j++;

                    xReal += 1;
                    limit = (int)real.XMax;
                    i = (int)(-(real.XMin) * numberOfPoints / (real.XMax - real.XMin));
                }

                return listOfPoints;

            } else {

                PointCollection[] listOfPoints = new PointCollection[1];

                for (i = 0; i <= numberOfPoints; i++) // OJOOOOOOOOOOOOOO Aqui he cambiado el < por <= para que llegue de -10 a 10 y no de -10 a 9.66 por ejemplo
                {
                    xReal = real.XMin + i * (real.XMax - real.XMin) / numberOfPoints;
                    yReal = g.Function.CalculateF(xReal);

                    xScreen = ConvertXFromRealToPant(xReal, screen.XMin, screen, real);
                    yScreen = ConvertYFromRealToPant(yReal, screen.YMin, screen, real);

                    points.Add(new Point(xScreen, yScreen));
                }

                listOfPoints[0] = points;

                return listOfPoints;
            }

        }

        /*
        private PointCollection[] DrawSplitedGraphic(Graphic g, double canvasWidth, double canvasHeight)
        {
            List<PointCollection> PointCollectionList = new List<PointCollection>();
            PointCollection puntos = new PointCollection();
            Polyline polilinea = new Polyline();
            double xreal, yreal, xpant, ypant, oldXReal;
            int numpuntos;

            numpuntos = (int)screen.XMax;

            polilinea.Stroke = Brushes.Red;
            // Xreal  = de -10  a 0 y de 0 a 10

            int i = 0;
            int j = 0;
            int limit = 0;
            while (j < 2)
            {

                do
                {
                    i++;
                    xreal = real.XMin + i * (real.XMax - real.XMin) / numpuntos;
                    Console.WriteLine("Xreal {0}", xreal);
                    yreal = SwitchFunctionFromButton(xreal);

                    xpant = ConvertXFromRealToPant(xreal, pant.XMin);
                    ypant = ConvertYFromRealToPant(yreal, pant.YMin);

                    Point punto = new Point(xpant, ypant);
                    puntos.Add(punto);
                } while (Convert.ToInt32(xreal) < limit);

                PointCollectionList.Add(puntos);
                puntos = new PointCollection();

                j++;

                Console.WriteLine("Antes {0}", xreal);
                oldXReal = xreal;
                xreal += 1;
                limit = (int)real.XMax;
                Console.WriteLine("Despues {0}", xreal);
                i = (int)(-(real.XMin) * numpuntos / (real.XMax - real.XMin));
                Console.WriteLine("i {0}", i);

            }
            

            foreach (PointCollection p in PointCollectionList)
            {
                lienzo.Children.Add(new Polyline()
                {
                    Points = p,
                    Stroke = Brushes.Red
                });
            }
        }
        */
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
