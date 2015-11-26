using Microsoft.Expression.Encoder.Devices;
using Microsoft.Expression.Encoder.Live;
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
using WebcamControl;

namespace VideoStudioApp.ViewModel
{
    public  class GrabacionVideoViewModel : ViewModelBase
    {
        public Window Home { get; set; }
        public EncoderDevice SelectedVideo { get; set; }
        public EncoderDevice SelectedAudio { get; set; }

        public Webcam WebcamCtrl {get; set;}


        public GrabacionVideoViewModel(Window home, EncoderDevice selectedAudio, EncoderDevice selectedVideo, Webcam cam)
        {
            Home = home;
            SelectedAudio = selectedAudio;
            SelectedVideo = selectedVideo;
            WebcamCtrl = cam;
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
            Thread.Sleep(20000);
            WebcamCtrl.StopPreview();                        
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
