using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
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
           ListaEdades = GetEdades();
        }

        public List<ComboBoxD> GetEdades()
        {
            List<ComboBoxD> lista = new List<ComboBoxD>();

            DateTime now = DateTime.Now;      
            ComboBoxD item = new ComboBoxD();
            item.ID = now.Year.ToString();
            item.Descripcion = now.Year.ToString();
            lista.Add(item);

            for (int i = 1; i < 100; i++)
            {
                DateTime nowF = DateTime.Now;                
                ComboBoxD itemF = new ComboBoxD();
                itemF.ID = nowF.AddYears(-(i)).Year.ToString();
                itemF.Descripcion = nowF.AddYears(-(i)).Year.ToString();
                lista.Add(itemF);
            }

            return lista;
        
        }

       public EstadisticasViewModel(Window home, Window window)
        {
            CurrentWindow = window;
            Home = home;
           ReporteCurrent = new Reporte();
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


       private ICommand exportExcel;
       public ICommand ExportExcel
       {
           get
           {
               if (exportExcel == null)
               {
                   exportExcel = new RelayCommand(
                       param => this.ExportExcelReporte()
                   );
               }
               return exportExcel;
           }
       }

       private void ExportExcelReporte()
       {
           Warning[] warnings;
           string[] streamids;
           string mimeType;
           string encoding;
           string filenameExtension;
           ReportViewer viewer = new ReportViewer();
           viewer.LocalReport.Refresh();
           viewer.LocalReport.ReportPath = "Report\\ReporteGrabaciones.rdlc";

           
           AppManager appManager = new AppManager();
           var reporte =  appManager.GetReporte( ReporteCurrent.Edad, ReporteCurrent.Brigada,
               ReporteCurrent.Lugar, ReporteCurrent.Municipio, ReporteCurrent.Colonia, ReporteCurrent.FechaInicial, ReporteCurrent.FechaFinal);



           viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSetVideo", reporte));


           //Excel
           byte[] bytesExcel = viewer.LocalReport.Render(
               "Excel", null, out mimeType, out encoding, out filenameExtension,
               out streamids, out warnings);

           using (FileStream fs = new FileStream("Reporte.xls", FileMode.Create))
           {
               fs.Write(bytesExcel, 0, bytesExcel.Length);
           }


           //Abro los archivos
           System.Diagnostics.Process.Start("Reporte.xls");
       }


       private ICommand exportPdf;
       public ICommand ExportPdf
       {
           get
           {
               if (exportPdf == null)
               {
                   exportPdf = new RelayCommand(
                       param => this.ExportPdfReporte()
                   );
               }
               return exportPdf;
           }
       }

       private void ExportPdfReporte()
       {
           Warning[] warnings;
           string[] streamids;
           string mimeType;
           string encoding;
           string filenameExtension;
           ReportViewer viewer = new ReportViewer();
           viewer.LocalReport.Refresh();
           viewer.LocalReport.ReportPath = "Report\\ReporteGrabaciones.rdlc";


           AppManager appManager = new AppManager();

           var reporte = appManager.GetReporte(ReporteCurrent.Edad, ReporteCurrent.Brigada,
               ReporteCurrent.Lugar, ReporteCurrent.Municipio, ReporteCurrent.Colonia, ReporteCurrent.FechaInicial, ReporteCurrent.FechaFinal);


           viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSetVideo", reporte));

           //Pdf
           byte[] bytes = viewer.LocalReport.Render(
               "PDF", null, out mimeType, out encoding, out filenameExtension,
               out streamids, out warnings);

           using (FileStream fs = new FileStream("Reporte.pdf", FileMode.Create))
           {
               fs.Write(bytes, 0, bytes.Length);
           }

           System.Diagnostics.Process.Start("Reporte.pdf");
       }


     

       private void Regresar()
       {
          this.CurrentWindow.Close();
           this.Home.Show();
           
       }


    }
}
