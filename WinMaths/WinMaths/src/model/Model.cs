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

        /// <summary>
        /// Constructor de la clase Model
        /// </summary>
        public Model()
        {
            this.ActualGraphicID = 0;
            this.IndexOfModifiedGraphic = 0;
            this.listOfGraphics = new ObservableCollection<Graphic>();
        }

        /* ========================= CRUD METHODS ========================= */

        /* Esto podria devolver bool en vez de int*/
        public int AddGraphic(Graphic newGraphic)
        {
            int id = GetActualGraphicId();
            newGraphic.ID = id;
            this.listOfGraphics.Add(newGraphic);
            return id;
        }

        /* Podria crear un Dictionary<int,Graphic> para eliminar sin tener que recorrer la coleccion??? Preguntar a ana*/
        public bool DeleteGraphic(List<Graphic> gToDelete)
        {
            foreach (Graphic g in gToDelete) {
                int id = g.ID;

                Graphic gph = GetGraphicWithID(id);

                if (gph != null) {
                    listOfGraphics.Remove(gph);
                    return true;
                } else {
                    return false;
                }
            }

            return false;
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
            int actualID = this.ActualGraphicID;
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
        }

        public ObservableCollection<Graphic> GetCollectionOfGraphics()
        {
            return this.listOfGraphics;
        }

        public List<Graphic> GetListOfGraphics()
        {
            return this.listOfGraphics.ToList<Graphic>();
        }
    }
}
