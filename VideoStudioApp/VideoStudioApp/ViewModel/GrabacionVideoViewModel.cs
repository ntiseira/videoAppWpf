using Microsoft.Expression.Encoder.Devices;
using Microsoft.Expression.Encoder.Live;
using NReco.VideoConverter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Threading;
using VideoStudioApp.Business;
using VideoStudioApp.Command;
using VideoStudioApp.Model;
using VideoStudioApp.Views;
using WebcamControl;

namespace VideoStudioApp.ViewModel
{
    public  class GrabacionVideoViewModel : ViewModelBase
    {
        public bool IsRecorded { get; set; }
        public bool IsAddedWaterMark { get; set; }

        public ConfigWaterMark Config { get; set; }

        public Window ConfigWinwdow { get; set; }

        public Window CurrentWindow { get; set; }

        public EncoderDevice SelectedVideo { get; set; }
        public EncoderDevice SelectedAudio { get; set; }

        public Webcam WebcamCtrl {get; set;}

        public Grabacion SelectedGrabacion { get; set; }

        private DispatcherTimer dt ;
        private Stopwatch stopWatch;
        string currentTime;

        public GrabacionVideoViewModel(Window currentWindow, EncoderDevice selectedAudio, EncoderDevice selectedVideo, Webcam cam, Grabacion selectedGrabacion)
        {

           
            
            CurrentWindow = currentWindow;
                    
            SelectedAudio = selectedAudio;
            SelectedVideo = selectedVideo;
            SelectedGrabacion = selectedGrabacion;
          
            WebcamCtrl = cam;
            InitializeVm();
            Config = new ConfigWaterMark();
            Config.VideoUrl = "C:\\Videos";
            Config.NombreVideo = WebcamCtrl.NombreVideo.ToString();
            CrearMarcaAgua();
            CargarVideo();

       
            
           // TextTimer = "30:00";

        }

        public void InitializeVm()
        {

            IsRecorded = false;
            IsAddedWaterMark = false;
            dt = new DispatcherTimer();
            stopWatch = new Stopwatch();
            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
            currentTime = string.Empty;
            WebcamCtrl.NombreVideo = SelectedGrabacion.Nombre + "_" + DateTime.Now.ToString("yyyy_dd_MM");
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF0E18E8"));
            ColorBtnGrabar = mySolidColorBrush;
            TextBtnGrabar = "Grabar";
        
        
        }


        void dt_Tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {

                if (WebcamCtrl.IsRecording == false)
                { 
                    WebcamCtrl.StopRecording();
                }

                TimeSpan ts = stopWatch.Elapsed;
                              
                if (ts.Seconds < 30)
                {
                    currentTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                    TextTimer = currentTime;
                }
                else
                {
                    if (!IsRecorded)
                    {
                        IsRecorded = true;
                        WebcamCtrl.StopRecording();
                        GuardarGrabacion();
                      
                    }
                    else
                    {
                        if (!IsAddedWaterMark)
                        {
                            IsAddedWaterMark = true;
                            
                            var thread = new Thread(() => update());
                            thread.SetApartmentState(ApartmentState.STA);
                            thread.IsBackground = true;
                            thread.Start();                          
                        }
                    
                    }

                 //   AgregarMarcaAguaVideo();

                //    this.Dispose();
                 //   this.CurrentWindow.Close();
           
                   
                 

                }
            }
        }

        public void update()
        {
           // this.CurrentWindow.Dispatcher.Invoke(() => Application.Current.MainWindow.Close());
            AgregarMarcaAguaVideo();

   

            //this.CurrentWindow.Close();
          //  this.Dispose();
           // this.CurrentWindow.Close();
        }

        private void bw_DoWork2(object sender, DoWorkEventArgs e)
        {
            AgregarMarcaAguaVideo();
            this.CurrentWindow.Close();
        }

        private void CrearMarcaAgua()
        {           
            //NombreMarcaAgua
            var pathMarcaAgua = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            pathMarcaAgua += "\\Content\\Images\\" + "MarcaDeAgua.jpg";

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


      

        private void GuardarGrabacion()
        {
            AppManager appManager = new AppManager();
            appManager.AgregarGrabacion(SelectedGrabacion.Lugar, SelectedGrabacion.Brigada, SelectedGrabacion.Colonia, SelectedGrabacion.Municipio, SelectedGrabacion.Nombre, Convert.ToDateTime(SelectedGrabacion.Edad));
        }


        private void Grabar()
        {
            WebcamCtrl.StartRecording();
            stopWatch.Start();
            dt.Start();


            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
          //  bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerCompleted += (s, e) =>
            {
                try
                {
                    if (e.Error != null)
                    {
                        // Handle failure.
                    }
                }
                finally
                {
                    // Use wkr outer instead of casting.
                    bw.Dispose();
                }
            };
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

            ffMpeg.Invoke("-i " + Config.VideoUrl + "\\" + SelectedGrabacion.Nombre + "_" + DateTime.Now.ToString("yyyy_dd_MM") + ".avi " + "-i " + variable + " -filter_complex \"overlay=400:330\" -codec:a copy " + Config.VideoUrl + "\\" + SelectedGrabacion.Nombre + "_" + DateTime.Now.ToString("yyyy_dd_MM") + "c.avi");
            System.IO.File.Delete(Config.VideoUrl + "\\" + SelectedGrabacion.Nombre + "_" + DateTime.Now.ToString("yyyy_dd_MM") + ".avi ");


            var pathVideoSource = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            pathVideoSource += "\\" + Config.NombreVideo + ".avi";

            dt.Stop();
            //dt.Dispatcher.InvokeShutdown();


            CurrentWindow.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(
            delegate()
            {

                TextTimer = "";
                TextBtnGrabar = "Grabar";
                ColorBtnGrabar = new SolidColorBrush(Colors.Blue);
                this.InitializeVm();
                CurrentWindow.Hide();
                ConfigDatosPersona config = new ConfigDatosPersona(this.CurrentWindow,SelectedVideo, SelectedAudio, SelectedGrabacion);
                config.ShowDialog();

            }));
                       
            

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
