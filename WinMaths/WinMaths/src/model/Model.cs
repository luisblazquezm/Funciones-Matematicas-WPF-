using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using WinMaths.src.bean;

namespace WinMaths.src.model
{
    public class Model
    {
        /* Elementos del Modelo */
        private int ActualGraphicID;
        private int IndexOfModifiedGraphic;
        private ObservableCollection<Graphic> listOfGraphics;
        private Dictionary<Graphic,Polyline> graphicRepresentationList;    // A partir de una determinada grafica obtengo la representación de su polilinea

        /// <summary>
        /// Constructor de la clase Model
        /// </summary>
        public Model()
        {
            this.ActualGraphicID = 0;
            this.IndexOfModifiedGraphic = 0;
            this.listOfGraphics = new ObservableCollection<Graphic>();
            this.graphicRepresentationList = new Dictionary<Graphic, Polyline>();
        }

        /* ========================= CRUD METHODS ========================= */

        /* Esto podria devolver bool en vez de int*/
        public int AddGraphic(Graphic newGraphic)
        {
            int id = GetActualGraphicId();
            newGraphic.ID = id;
            this.listOfGraphics.Add(newGraphic);
            /* Añado la poliniea de la grafica directamente aqui llamando a otro método???? */
            return id;
        }

        /* Podria crear un Dictionary<int,Graphic> para eliminar sin tener que recorrer la coleccion??? Preguntar a ana*/
        public bool DeleteGraphic(Graphic gToDelete)
        {
            int id = gToDelete.ID;

            Graphic g = GetGraphicWithID(id);

            if (g != null) {
                listOfGraphics.Remove(g);
                return true;

            } else {
                return false;
            }

        }

        public bool UpdateGraphic(Graphic gToModify)
        {
            int id = gToModify.ID;

            Graphic g = GetGraphicWithID(id);

            if (g != null) {

                /* Deleting */
                if (this.IndexOfModifiedGraphic != 0) {
                    listOfGraphics[this.IndexOfModifiedGraphic] = gToModify;
                    return true;
                }
            } else {
                return false;
            }

            return false;
        }

        /* ========================= OTHER METHODS ========================= */


        /* Esto podria estar mál -> Comprobar que hace bien los IDS*/
        private int GetActualGraphicId()
        {
            if (this.ActualGraphicID != 0)
                (this.ActualGraphicID)++;

            return this.ActualGraphicID;
        }

        public Graphic GetGraphicWithID(int id)
        {
            foreach (Graphic g in listOfGraphics)
            {
                if (id == g.ID)
                    return g;
            }

            return null;
        }

        public void ClearModel()
        {
            this.ActualGraphicID = 0;
            this.IndexOfModifiedGraphic = 0;
            this.listOfGraphics.Clear();
            this.graphicRepresentationList.Clear();
        }

        public ObservableCollection<Graphic> GetListOfGraphics()
        {
            return this.listOfGraphics;
        }

    }
}
