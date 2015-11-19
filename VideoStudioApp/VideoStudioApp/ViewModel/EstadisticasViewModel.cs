using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VideoStudioApp.Command;
using VideoStudioApp.Helper;
using VideoStudioApp.Model;

namespace VideoStudioApp.ViewModel
{
    public class EstadisticasViewModel:ViewModelBase
    {

       public Window Home { get; set; }
       public Window CurrentWindow { get; set; }

       public EstadisticasViewModel(Window home, Window window)
        {
            CurrentWindow = window;
            Home = home;         
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
