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
        public List<Graphic> listOfGraphics { get; set; }

        // Constructor vacío
        public ViewModelEventArgs()
        {
            this.listOfGraphics = null;
        }

        public ViewModelEventArgs(List<Graphic> graphic)
        {
            this.listOfGraphics = graphic;
        }

        public ViewModelEventArgs(Graphic graphic)
        {
            this.listOfGraphics.Add(graphic);
        }
    }

    public delegate void ViewModelEventHandler(object sender, ViewModelEventArgs e);

    public class ViewModel
    {
        /* Elementos del Modelo */
        private Model model;

        /* Eventos de Cambio en la Propiedad */
        //public event EventHandler GraphicAdded;
        public event ViewModelEventHandler GraphicSetToDraw;
        public event ViewModelEventHandler GraphicDeleted;
        public event ViewModelEventHandler GraphicUpdated;
        public event ViewModelEventHandler ModelCleared;

        /// <summary>
        /// Constructor de la clase ViewModel
        /// </summary>
        public ViewModel()
        {
            model = new Model();
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
            OnDrawGraphic(g);
        }


        /* ========================= PROPERTY EVENT NOTIFICATION METHODS ========================= */

        /*
        protected virtual void OnGraphicAdded(Graphic g)
        {
            if (GraphicAdded != null)
                GraphicAdded(this, new ViewModelEventArgs(g));
        }
        */
        protected virtual void OnDrawGraphic(List<Graphic> g)
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

        protected virtual void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            OnGraphicUpdated((Graphic)sender);
        }

    }


}
