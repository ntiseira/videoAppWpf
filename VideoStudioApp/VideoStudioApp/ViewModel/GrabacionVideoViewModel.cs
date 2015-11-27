using Microsoft.Expression.Encoder.Devices;
using Microsoft.Expression.Encoder.Live;
using NReco.VideoConverter;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using VideoStudioApp.Command;
using VideoStudioApp.Model;
using WebcamControl;

namespace VideoStudioApp.ViewModel
{
    public  class GrabacionVideoViewModel : ViewModelBase
    {
        public Window Home { get; set; }
        public EncoderDevice SelectedVideo { get; set; }
        public EncoderDevice SelectedAudio { get; set; }

        public Webcam WebcamCtrl {get; set;}

        public Grabacion SelectedGrabacion { get; set; }


        public GrabacionVideoViewModel(Window home, EncoderDevice selectedAudio, EncoderDevice selectedVideo, Webcam cam, Grabacion selectedGrabacion)
        {
            Home = home;            
            SelectedAudio = selectedAudio;
            SelectedVideo = selectedVideo;
            SelectedGrabacion = selectedGrabacion;
           
            WebcamCtrl = cam;
            this.WebcamCtrl.ImagenMarcaAgua = "tigre.jpg";           
            CargarVideo();
            TextTimer = "30:00";

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

        private void Grabar()
        {
           
                
            WebcamCtrl.StartRecording();
            //Espera 20 segundoss y detiene
            Thread.Sleep(5000);
            WebcamCtrl.StopPreview();
            WebcamCtrl.StopRecording();
            AgregarMarcaAguaVideo();
            
           
        }



        public bool ResizeImage(string fileName, string imgFileName,
  ImageFormat format, int percent)
        {
            try
            {
                using (Image img = Image.FromFile(fileName))
                {


                    int width = 300;
                    //Convert.ToInt32(img.Width * (percent * .01));
                    int height = 200;
                    //Convert.ToInt32(img.Height * (percent * .01));


                    Image thumbNail = new Bitmap(width, height, img.PixelFormat);
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


        public void AgregarMarcaAguaVideo()
        {
            var pathMarcaAgua = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            pathMarcaAgua += "\\" + "tigre.jpg";

            ResizeImage(pathMarcaAgua, "C:\\marcaAgua.jpg", ImageFormat.Jpeg, 0);



            var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
         

            var variable = "C:\\marcaAgua.jpg";

        //    ffMpeg.ConvertMedia(WebcamCtrl.VideoDirectory + "\\" + WebcamCtrl.NombreVideo + ".avi ", "video.mp4", Format.mp4);

            ffMpeg.Invoke("-i " + WebcamCtrl.VideoDirectory + "\\" + WebcamCtrl.NombreVideo + ".avi " + "-i " + variable + " -filter_complex \"overlay=10:30\" -codec:a copy videova.avi");

            // ffMpeg.Invoke("-i c:\\video.mp4 -i C:\\prueba.bmp -filter_complex \"[0:v][1:v]overlay=main_w-overlay_w-10:10\" -codec:a copy videoaa.mp4");


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
