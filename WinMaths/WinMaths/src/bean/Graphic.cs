using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WinMaths.src.bean.function;

namespace WinMaths.src.bean
{
    [Serializable]
    public struct Colour
    {
        public byte A;
        public byte R;
        public byte G;
        public byte B;

        public Colour(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public Colour(Color color)
            : this(color.A, color.R, color.G, color.B)
        {
        }

        public static implicit operator Colour(Color color)
        {
            return new Colour(color);
        }

        public static implicit operator Color(Colour colour)
        {
            return Color.FromArgb(colour.A, colour.R, colour.G, colour.B);
        }
    }


    [Serializable]
    public class Graphic : INotifyPropertyChanged
    {
        /* Datos de la clase Graphic */
        private int id;
        private Function function;
        private string name;
        private double paramA;
        private double paramB;
        private double paramC;
        private Colour graphicColor;

        /// <summary>
        /// Constructor de la clase Graphic
        /// </summary>
        public Graphic(Function function, 
                       string name, 
                       double paramA, 
                       double paramB, 
                       double paramC, 
                       Color graphicColor)
        {
            this.function = function;
            this.name = name;
            this.paramA = paramA;
            this.paramB = paramB;
            this.paramC = paramC;
            this.graphicColor = graphicColor;
        }


        /* ========================= PROPERTY METHODS ========================= */

        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }

        /// <summary>
        /// Definición de la propiedad del dato 'function'
        /// </summary>
        public Function Function
        {
            get { return function; }
            set {
                function = value;
                OnPropertyChanged("Function");
            }
        }

        public string FunctionToString
        {
            get { return function.ToString(); }
            set
            {
                
            }
        }

        /// <summary>
        /// Definición de la propiedad del dato 'name'
        /// </summary>
        public string Name
        {
            get { return name; }
            set {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Definición de la propiedad 'paramA'
        /// </summary>
        public double ParamA
        {
            get { return paramA; }
            set {
                paramA = value;
                OnPropertyChanged("ParamA");
            }
        }

        /// <summary>
        /// Definición de la propiedad 'paramB'
        /// </summary>
        public double ParamB
        {
            get { return paramB; }
            set {
                paramB = value;
                OnPropertyChanged("ParamB");
            }
        }

        /// <summary>
        /// Definición de la propiedad 'paramC'
        /// </summary>
        public double ParamC
        {
            get { return paramC; }
            set {
                paramC = value;
                OnPropertyChanged("ParamC");
            }
        }

        /// <summary>
        /// Definición de la propiedad 'graphicColor'
        /// </summary>
        
        public Color GraphicColor
        {
            get { return graphicColor; }
            set {
                graphicColor = value;
                OnPropertyChanged("GraphicColor");
            }
        }

        public static Color GetColorFromColour(Colour c)
        {
            return Color.FromArgb(c.A, c.R, c.G, c.B);
        }

        public static Colour GetColourFromColor(Color c)
        {
            return new Colour(c.A, c.R, c.G, c.B);
        }

        /* ========================= PROPERTY EVENT NOTIFICATION METHODS ========================= */

        #region INotifyPropertyChanged Members

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        // Hay que implementarla porque el origen del enlace (Graphic) no es una clase de WPF
        // Se avisa de que propiedad de la clase ha cambiado
        
        protected void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
        

    }
}
