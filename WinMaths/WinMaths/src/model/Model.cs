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

        /* Esto podria devolver bool en vez de int*/
        public int AddGraphic(Graphic newGraphic)
        {
            int id = GetActualGraphicId();
            newGraphic.ID = id;
            this.listOfGraphics.Add(newGraphic);
            Console.WriteLine("Grafica añadida {0} ID {1}", newGraphic.Name, id);
            return id;
        }

        /* Podria crear un Dictionary<int,Graphic> para eliminar sin tener que recorrer la coleccion??? Preguntar a ana*/
        public bool DeleteGraphic(List<Graphic> gToDelete)
        {
            Console.WriteLine("NumGraficas {0}", gToDelete.Count);
            foreach (Graphic g in gToDelete) {
                int id = g.ID;

                Graphic gph = GetGraphicWithID(id);

                if (gph != null) {
                    Console.WriteLine("Grafica eliminada {0}", gph.Name);
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

            Console.WriteLine("Grafica Updated {0} Name {1}", gToModify.ID, gToModify.Name);
            if (gToModify != null) {

                foreach (Graphic graph in listOfGraphics){
                    Console.WriteLine("Grafica ID {0}", graph.ID);
                    if (gToModify.ID == graph.ID) {
                        listOfGraphics[gToModify.ID] = gToModify; 
                        return true;
                    }
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
            Console.WriteLine("ID nueva grafica {0} y actual {1}", actualID, this.ActualGraphicID);
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
            if (gImported != null)
            {
                foreach (Graphic g in gImported){
                    if (g == null)
                        return false;
                    g.ID = GetActualGraphicId();
                    Console.WriteLine("Grafica importada {0} ID {1}", g.Name, g.ID);
                    listOfGraphics.Add(g);
                }

                return true;
            }

            return false;
        }
    }
}
