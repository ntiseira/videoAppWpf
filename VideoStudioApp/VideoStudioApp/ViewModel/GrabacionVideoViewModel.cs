using Microsoft.Expression.Encoder.Devices;
using Microsoft.Expression.Encoder.Live;
using NReco.VideoConverter;
using System;
using System.Collections.Generic;
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
        public void AgregarMarcaAguaVideo()
        {

            // NReco.VideoConverter.FFMpegConverter wrap = new FFMpegConverter();
            //wrap.Invoke("-i c:\\test.avi -i C:\\prueba.bmp -filter_complex \"overlay=10:10\" c:\\test2.avi");

            var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
            //Convierte de formato
            //  ffMpeg.ConvertMedia("c:\\test.avi", "video.mp4", Format.mp4);
            String strAssemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            strAssemblyPath += WebcamCtrl.ImagenMarcaAgua;
           
            var pathMarcaAgua = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            pathMarcaAgua += "\\" + "tigre.jpg";
                    

            ffMpeg.Invoke("-i " + WebcamCtrl.VideoDirectory + "\\" + WebcamCtrl.NombreVideo + ".avi " + "-i C:\\tigre.jpg -filter_complex \"overlay=10:30\" -codec:a copy pruebaqueandelpm.avi");

            
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
