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


                /*
            ' Create directory for saving video files
            Dim videoPath As String = "C:\VideoClips"

            If Not Directory.Exists(videoPath) Then
                Directory.CreateDirectory(videoPath)
            End If
            ' Create directory for saving image files
            Dim imagePath As String = "C:\WebcamSnapshots"

            If Not Directory.Exists(imagePath) Then
                Directory.CreateDirectory(imagePath)
            End If

                 * */

                //' Set some properties of the Webcam control
                WebcamCtrl.VideoDirectory = "C:\\prueba";
                WebcamCtrl.ImageDirectory = "C:\\prueba";
                WebcamCtrl.FrameRate = 30;

                System.Drawing.Size size = new System.Drawing.Size(640, 480);
                WebcamCtrl.FrameSize = size;
                    //new Size(640, 480);


                WebcamCtrl.StartPreview();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }











    }
}
