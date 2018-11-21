using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WinMaths.src.model
{
    public class Graphic
    {
        /* Datos de la clase Graphic */
        private string function;
        private string name;
        private float paramA;
        private float paramB;
        private float paramC;
        private float paramN;
        private Color graphicColor;

        /// <summary>
        /// Constructor de la clase Graphic
        /// </summary>
        public Graphic(string function, 
                       string name, 
                       float paramA, 
                       float paramB, 
                       float paramC, 
                       float paramN,
                       Color graphicColor)
        {
            this.function = function;
            this.name = name;
            this.paramA = paramA;
            this.paramB = paramB;
            this.paramC = paramC;
            this.paramN = paramN;
            this.graphicColor = graphicColor;
        }

        /// <summary>
        /// Definición de la propiedad del dato 'function'
        /// </summary>
        public string Function
        {
            get { return function; }
            set { if (value != null) function = value; }
        }

        /// <summary>
        /// Definición de la propiedad del dato 'name'
        /// </summary>
        public string Name
        {
            get { return name; }
            set { if (name != null) name = value; }
        }

        /// <summary>
        /// Definición de la propiedad 'paramA'
        /// </summary>
        public float ParamA
        {
            get { return paramA; }
            set { paramA = value; }
        }

        /// <summary>
        /// Definición de la propiedad 'paramB'
        /// </summary>
        public float ParamB
        {
            get { return paramB; }
            set { paramB = value; }
        }

        /// <summary>
        /// Definición de la propiedad 'paramC'
        /// </summary>
        public float ParamC
        {
            get { return paramC; }
            set { paramC = value; }
        }

        /// <summary>
        /// Definición de la propiedad 'paramN'
        /// </summary>
        public float ParamN
        {
            get { return paramN; }
            set { paramN = value; }
        }

        /// <summary>
        /// Definición de la propiedad 'graphicColor'
        /// </summary>
        public Color GraphicColor
        {
            get { return graphicColor; }
            set { if (value != null) graphicColor = value; }
        }
    }
}
