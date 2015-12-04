using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Expression.Encoder;
using VideoStudioApp.Command;
using VideoStudioApp.Helper;
using VideoStudioApp.Views;
using System.Drawing;
using NReco.VideoConverter;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using Microsoft.Reporting.WinForms;
using VideoStudioApp.Model;


namespace VideoStudioApp.ViewModel
{
    public class MainWindowViewModel: ViewModelBase
    {

        //public Window CurrentWindow { get; set; }
        public Window Home { get; set; }

        public MainWindowViewModel(Window home)
        {
            //CurrentWindow = window;
            Home = home;
            CargarReporte();
          // Prueba();
            
        }


        public void CargarReporte()
        {

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;
            ReportViewer viewer = new ReportViewer();
            viewer.LocalReport.Refresh();
            viewer.LocalReport.ReportPath = "Report\\ReporteGrabaciones.rdlc";

            List<Reporte> rep = new List<Reporte>();
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSetVideo", rep));
          
            //Pdf
            byte[] bytes = viewer.LocalReport.Render(
                "PDF", null, out mimeType, out encoding, out filenameExtension,
                out streamids, out warnings);

            using (FileStream fs = new FileStream("prueba.pdf", FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
          

            //Excel
            byte[] bytesExcel = viewer.LocalReport.Render(
                "Excel", null, out mimeType, out encoding, out filenameExtension,
                out streamids, out warnings);

            using (FileStream fs = new FileStream("prueba.xls", FileMode.Create))
            {
                fs.Write(bytesExcel, 0, bytesExcel.Length);
            }


            //Abro los archivos
            System.Diagnostics.Process.Start("prueba.xls");
            System.Diagnostics.Process.Start("prueba.pdf");
        }




        public void Prueba()
        {
            try
            {
                var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
              
                ffMpeg.Invoke("-i c:\\NICOLAS_2015_27_11.avi -i C:\\marcaAgua.jpg -filter_complex \"overlay=350:300\" -codec:a copy pruebaadddaaa.avi");

                


                //         ffMpeg.Invoke("-i " + WebcamCtrl.VideoDirectory + "\\" + WebcamCtrl.NombreVideo + ".mp4 " + "-i " + pathMarcaAgua + " -filter_complex \"overlay=10:30\" -codec:a copy videova.mp4");




                // NReco.VideoConverter.FFMpegConverter wrap = new FFMpegConverter();
                //wrap.Invoke("-i c:\\test.avi -i C:\\prueba.bmp -filter_complex \"overlay=10:10\" c:\\test2.avi");

                //var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
                //Convierte de formato
                //  ffMpeg.ConvertMedia("c:\\test.avi", "video.mp4", Format.mp4);

              //  ffMpeg.Invoke("-i c:\\video.mp4 -i C:\\tigre.jpg -filter_complex \"[0:v][1:v]overlay=main_w-overlay_w-10:10\" -codec:a copy videova.mp4");

                // ffMpeg.Invoke("-i c:\\video.mp4 -i C:\\prueba.bmp -filter_complex \"[0:v][1:v]overlay=main_w-overlay_w-10:10\" -codec:a copy videoaa.mp4");
          
            }
            catch (Exception ex)
            {
                throw ex;
            }

        

        }

        private ICommand iniciarAdminCommand;
        public ICommand IniciarAdminCommand
        {
            get
            {
                if (iniciarAdminCommand == null)
                {
                    iniciarAdminCommand = new RelayCommand(
                        param => this.IniciarAdmin()
                    );
                }
                return iniciarAdminCommand;
            }
        }


        private void IniciarAdmin()
        {
            this.Home.Hide();
            Administrador admin = new Administrador( this.Home);
            admin.ShowDialog();
        }



        private ICommand iniciarAppCommand;
        public ICommand IniciarAppCommand
        {
            get
            {
                if (iniciarAppCommand == null)
                {
                    iniciarAppCommand = new RelayCommand(
                        param => this.IniciarApp()
                    );
                }
                return iniciarAppCommand;
            }
        }


        private void IniciarApp()
        {
            this.Home.Hide();
            ConfigDispositivos admin = new ConfigDispositivos(this.Home);
            admin.ShowDialog();
          
        }



    }
}
