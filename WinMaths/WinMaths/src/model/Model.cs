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
        private ObservableCollection<Graphic> listOfGraphics;

        /// <summary>
        /// Constructor de la clase Model
        /// </summary>
        public Model()
        {
            this.ActualGraphicID = 0;
            this.listOfGraphics = new ObservableCollection<Graphic>();
        }

        /* ========================= CRUD METHODS ========================= */

        public int AddGraphic(Graphic newGraphic)
        {
            int id = GetActualGraphicId();
            newGraphic.ID = id;
            this.listOfGraphics.Add(newGraphic);
            return id;
        }

        public bool DeleteGraphic(List<Graphic> gToDelete)
        {
            foreach (Graphic g in gToDelete) {
                int id = g.ID;

                Graphic gph = GetGraphicWithID(id);

                if (gph != null) {
                    listOfGraphics.Remove(gph);
                } else {
                    return false;
                }
            }

            return true;
        }

        public bool UpdateGraphic(Graphic gToModify)
        {
            int id = gToModify.ID;

            if (gToModify != null) {

                foreach (Graphic graph in listOfGraphics){
                    if (gToModify.ID == graph.ID) {
                        listOfGraphics[listOfGraphics.IndexOf(graph)] = gToModify; 
                        return true;
                    }
                }

            } else {
                return false;
            }

            return false;
        }

        /* ========================= OTHER METHODS ========================= */

        private int GetActualGraphicId()
        {
            int actualID = this.ActualGraphicID;
            (this.ActualGraphicID)++;
            return actualID;
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

        public bool ImportList(List<Graphic> gImported)
        {
            Boolean imported = true;
            if (gImported != null)
            {
                foreach (Graphic g in gImported){

                    if (g == null) {
                        return false;
                    } else if (listOfGraphics.Contains(g) || true == IsGraphicNameRepeated(g.Name)) {
                        imported = false;
                        continue;
                    }
                        
                    g.ID = GetActualGraphicId();
                    listOfGraphics.Add(g);
                }

                return imported;
            }

            return false;
        }

        public bool IsGraphicNameRepeated(string name)
        {
            int count = 0;
            foreach (Graphic g in listOfGraphics)
            {
                if (name.Equals(g.Name))
                    ++count;
            }

            if (count >= 2)
                return true;
            else
                return false;
        }
    }
}
