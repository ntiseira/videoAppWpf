using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VideoStudioApp.Business;
using VideoStudioApp.Command;
using VideoStudioApp.Helper;
using VideoStudioApp.Model;

namespace VideoStudioApp.ViewModel
{
    public class EstadisticasViewModel:ViewModelBase
    {

       public Window Home { get; set; }
       public Window CurrentWindow { get; set; }

       public List<ComboBoxD> ListaBrigadas { get; set; }
       public List<ComboBoxD> ListaLugares { get; set; }
       public List<ComboBoxD> ListaMunicipios { get; set; }
       public List<ComboBoxD> ListaColonias { get; set; }
       public List<ComboBoxD> ListaEdades { get; set; }
        public Reporte ReporteCurrent { get; set; }

        public void CargarCombos()
       {
           AppManager appManager = new AppManager();
           ListaLugares = appManager.GetLugares();
           ListaBrigadas = appManager.GetBrigadas();
           ListaColonias = appManager.GetColonias();
           ListaMunicipios = appManager.GetMunicipios();
       }

       public EstadisticasViewModel(Window home, Window window)
        {
            ReporteCurrent = new Reporte();

            CurrentWindow = window;
            Home = home;
           CargarCombos();
        }

       private ICommand regresarCommand;
       public ICommand RegresarCommand
       {
           get
           {
               if (regresarCommand == null)
               {
                   regresarCommand = new RelayCommand(
                       param => this.Regresar()
                   );
               }
               return regresarCommand;
           }
       }


       public void GenerarExcel()
       {


           List<Reporte> lista = new List<Reporte>();

        /*
           for (int i = 0; i < 10; i++)
           {
               ComboBoxD cbo = new ComboBoxD();
               cbo.ID = i;
               cbo.Descripcion = "lala" + i.ToString();

               lista.Add(cbo);
           }
           //datagrid.ItemsSource = lista;
           */


           ExportToExcel<Reporte, List<Reporte>> s = new ExportToExcel<Reporte, List<Reporte>>();
           s.dataToPrint = lista;
           s.GenerateReport();
       
       }

       private void Regresar()
       {
           this.CurrentWindow.Close();
           this.Home.Show();
       }


    }
}
