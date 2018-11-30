using System;
using WinMaths.src.bean;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinMaths.src.model;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WinMaths.src.viewModels
{
    public class ViewModelEventArgs : EventArgs
    {
        /* Elementos que contendran los EventArgs */
        public List<Graphic> ListOfGraphics { get; set; }

        // Constructor vacío
        public ViewModelEventArgs()
        {
            this.ListOfGraphics = null;
        }

        public ViewModelEventArgs(List<Graphic> graphic)
        {
            this.ListOfGraphics = graphic;
        }

        // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< Esto está mal
        public ViewModelEventArgs(Graphic graphic)
        {
            List<Graphic> listOfG = new List<Graphic>();
            listOfG.AddRange(this.ListOfGraphics);
            listOfG.Add(graphic);
            this.ListOfGraphics = listOfG;
            Console.WriteLine("Longitud de la lista {0}", this.ListOfGraphics.Count);
        }
    }

    public delegate void ViewModelEventHandler(object sender, ViewModelEventArgs e);

    public struct FuncRect
    {
        public double XMin, XMax, YMin, YMax;
    }

    public class ViewModel
    {
        /* Elementos del Modelo */
        private Model model;

        /* Elementos de Limites */
        private FuncRect graphicLimits;

        /* Eventos de Cambio en la Propiedad */
        //public event ViewModelEventHandler GraphicAdded;
        public event ViewModelEventHandler GraphicSetToDraw;
        public event ViewModelEventHandler GraphicDeleted;
        public event ViewModelEventHandler GraphicUpdated;
        public event ViewModelEventHandler ModelCleared;
        public event ViewModelEventHandler GraphicRepresentationUpdated;

        /// <summary>
        /// Constructor de la clase ViewModel
        /// </summary>
        public ViewModel()
        {
            model = new Model();
            graphicLimits.XMin = -10;
            graphicLimits.XMax = 10;
            graphicLimits.YMin = -10;
            graphicLimits.YMax = 10;
        }

        /* En vez de pasarle la grafica a todos pasarle solo el ID????????????????*/
        public int AddGraphicVM(Graphic g)
        {
            int creationResult = model.AddGraphic(g);
            g.PropertyChanged += PropertyChangedHandler; // Se lanza el evento que avisa de que se ha modificado una propiedad de la clase Grafica
            //OnGraphicAdded(g);
            return creationResult;
        }

        public bool UpdateGraphicVM(Graphic g)
        {
            bool updateResult = model.UpdateGraphic(g);
            if (updateResult) {
                g.PropertyChanged += PropertyChangedHandler;
                OnGraphicUpdated(g);
            }
            
            return updateResult;
        }

        public bool DeleteGraphicVM(List<Graphic> g)
        {
            bool deletingResult = model.DeleteGraphic(g);
            if (deletingResult) {
                OnGraphicDeleted(g);
            }

            return deletingResult;
        }

        public Graphic GetActualGraphicIdVM(int id)
        {
            return model.GetGraphicWithID(id);
        }

        public void ClearModelVM()
        {
            model.ClearModel();
            ForceModelUpdated();
        }

        public ObservableCollection<Graphic> GetCollectionOfGraphicsVM()
        {
            return model.GetCollectionOfGraphics();
        }

        public List<Graphic> GetListOfGraphicsVM()
        {
            return model.GetListOfGraphics();
        }

        public void DrawGraphicVM(List<Graphic> g)
        {
            OnDrawGraphic(g, false);
        }

        public bool ImportListVM(List<Graphic> g)
        {
            return model.ImportList(g);
        }

        public FuncRect FuncRect
        {
            get { return this.graphicLimits; }
            set
            {
                this.graphicLimits = value;
                OnReloadRepresentation();
            }
        }


        /* ========================= PROPERTY EVENT NOTIFICATION METHODS ========================= */

        /*
        protected virtual void OnGraphicAdded(Graphic g)
        {
            if (GraphicAdded != null)
                GraphicAdded(this, new ViewModelEventArgs(g));
        }
        */

        protected virtual void OnDrawGraphic(List<Graphic> g, bool limit)
        {
            if (GraphicSetToDraw != null)
                GraphicSetToDraw(this, new ViewModelEventArgs(g));
        }

        protected virtual void OnGraphicDeleted(List<Graphic> g)
        {
            if (GraphicDeleted != null)
                GraphicDeleted(this, new ViewModelEventArgs(g));
        }

        protected virtual void OnGraphicUpdated(Graphic g)
        {
            if (GraphicUpdated != null)
                GraphicUpdated(this, new ViewModelEventArgs(g));
        }

        protected virtual void ForceModelUpdated()
        {
            if (ModelCleared != null)
                ModelCleared(this, new ViewModelEventArgs());
        }

        protected virtual void OnReloadRepresentation()
        {
            if (GraphicRepresentationUpdated != null)
                GraphicRepresentationUpdated(this, new ViewModelEventArgs());
        }

        protected virtual void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            OnGraphicUpdated((Graphic)sender);
        }
    }


}
