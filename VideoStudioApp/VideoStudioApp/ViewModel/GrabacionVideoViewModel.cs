using Microsoft.Expression.Encoder.Devices;
using Microsoft.Expression.Encoder.Live;
using NReco.VideoConverter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using VideoStudioApp.Command;
using VideoStudioApp.Model;
using VideoStudioApp.Views;
using WebcamControl;

namespace VideoStudioApp.ViewModel
{
    public  class GrabacionVideoViewModel : ViewModelBase
    {
        public Window Home { get; set; }

        public Window CurrentWindow { get; set; }

        public EncoderDevice SelectedVideo { get; set; }
        public EncoderDevice SelectedAudio { get; set; }

        public Webcam WebcamCtrl {get; set;}

        public Grabacion SelectedGrabacion { get; set; }


        public GrabacionVideoViewModel(Window home, Window currentWindow, EncoderDevice selectedAudio, EncoderDevice selectedVideo, Webcam cam, Grabacion selectedGrabacion)
        {
           

            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0E18E8"));
            ColorBtnGrabar = mySolidColorBrush;
            TextBtnGrabar = "Grabar";

            CurrentWindow = currentWindow;
            Home = home;            
            SelectedAudio = selectedAudio;
            SelectedVideo = selectedVideo;
            SelectedGrabacion = selectedGrabacion;
           
            WebcamCtrl = cam;
            CrearMarcaAgua();
            CargarVideo();
            TextTimer = "30:00";

        }

        private void CrearMarcaAgua()
        {           
            //NombreMarcaAgua
            var pathMarcaAgua = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            pathMarcaAgua += "\\" + "tigre.jpg";

            ResizeImage(pathMarcaAgua, "C:\\marcaAgua.jpg", ImageFormat.Jpeg, 0);
            this.WebcamCtrl.ImagenMarcaAgua = "C:\\marcaAgua.jpg";
        }


        private ICommand grabarCommand;
        public ICommand GrabarCommand
        {
            get
            {
                if (grabarCommand == null)
                {
                    grabarCommand = new RelayCommand(
                        param => this.Grabar()
                    );
                }
                return grabarCommand;
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            //Completo los servicios y procesos iniciales

            CurrentWindow.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                (MethodInvoker)delegate()
                {
                    TextBtnGrabar = "Grabando...";
                    ColorBtnGrabar = new SolidColorBrush(Colors.DarkRed); 
                }
                );
        }


        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // The background process is complete. First we should hide the
            // modal Progress Form to unlock the UI. The we need to inspect our
            // response to see if an error occured, a cancel was requested or
            // if we completed succesfully.

            CurrentWindow.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background,
            new Action(
            delegate()
            {
                WebcamCtrl.StartRecording();

                //Espera 20 segundoss y detiene
                Thread.Sleep(10000);
                // WebcamCtrl.StopPreview();        
                WebcamCtrl.StopRecording();
                AgregarMarcaAguaVideo();

                MainWindow main = new MainWindow();
                main.ShowDialog();
                this.Home.Close();
                
                this.CurrentWindow.Close();
                this.Dispose();
            }
            ));
        }

        private void Grabar()
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            // Kick off the Async thread
            bw.RunWorkerAsync();
                       
                    
           
           
        }



        public bool ResizeImage(string fileName, string imgFileName,
  ImageFormat format, int percent)
        {
            try
            {
                using (System.Drawing.Image img = System.Drawing.Image.FromFile(fileName))
                {
                    int width = 250;                    
                    int height = 150;

                    System.Drawing.Image thumbNail = new Bitmap(width, height, img.PixelFormat);
                    Graphics g = Graphics.FromImage(thumbNail);
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Rectangle rect = new Rectangle(0, 0, width, height);
                    g.DrawImage(img, rect);
                    thumbNail.Save(imgFileName, format);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private SolidColorBrush colorBtnGrabar;
        public SolidColorBrush ColorBtnGrabar
        {
            get
            {
                return colorBtnGrabar;
            }
            set
            {
                colorBtnGrabar = value;
                OnPropertyChanged("ColorBtnGrabar");
            }
        }



        private string textBtnGrabar;
        public string TextBtnGrabar
        {
            get
            {
                return textBtnGrabar;
            }
            set
            {
                textBtnGrabar = value;
                OnPropertyChanged("TextBtnGrabar");
            }
        }

        public void AgregarMarcaAguaVideo()
        {                  
            var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
            var variable = "C:\\marcaAgua.jpg";

            ffMpeg.Invoke("-i " + WebcamCtrl.VideoDirectory + "\\" + WebcamCtrl.NombreVideo + ".avi " + "-i " + variable + " -filter_complex \"overlay=400:330\" -codec:a copy " + WebcamCtrl.VideoDirectory + "\\" +WebcamCtrl.NombreVideo + "c.avi");
            System.IO.File.Delete(WebcamCtrl.VideoDirectory + "\\" + WebcamCtrl.NombreVideo + ".avi ");


            var pathVideoSource = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            pathVideoSource += "\\" + WebcamCtrl.NombreVideo + ".avi";
        }
        
        private string textTimer;
        public string TextTimer
        {
            get
            {
                return textTimer;
            }
            set
            {
                textTimer = value;
                OnPropertyChanged("TextTimer");
            }
        }


        public void CargarVideo()
        {
            try
            {
                WebcamCtrl.AudioDevice = SelectedAudio;
                WebcamCtrl.VideoDevice = SelectedVideo;
                
                string path = "C:\\Videos"; // your code goes here
                bool exists = System.IO.Directory.Exists(path);

                if (!exists)
                    System.IO.Directory.CreateDirectory(path);

                //' Set some properties of the Webcam control
                WebcamCtrl.VideoDirectory = "C:\\Videos";
                WebcamCtrl.ImageDirectory = "C:\\Videos";
                WebcamCtrl.FrameRate = 30;

                System.Drawing.Size size = new System.Drawing.Size(640, 480);
                WebcamCtrl.FrameSize = size;                

                WebcamCtrl.StartPreview();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }











    }
}
