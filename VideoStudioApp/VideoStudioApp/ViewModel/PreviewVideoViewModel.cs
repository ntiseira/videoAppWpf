using Microsoft.Expression.Encoder.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VideoStudioApp.Command;
using VideoStudioApp.Views;
using WebcamControl;

namespace VideoStudioApp.ViewModel
{
    public class PreviewVideoViewModel:ViewModelBase
    {

        
         public Window Home { get; set; }
       public Window CurrentWindow { get; set; }

      
        public EncoderDevice SelectedVideo { get; set; }
        public EncoderDevice SelectedAudio { get; set; }

        public Webcam WebcamCtrl {get; set;}


        public PreviewVideoViewModel(Window home,Window current, EncoderDevice selectedAudio, EncoderDevice selectedVideo, Webcam cam)
        {
            Home = home;
            SelectedAudio = selectedAudio;
            SelectedVideo = selectedVideo;
            CurrentWindow = current;
            WebcamCtrl = cam;
            CargarVideo();
            
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



        private ICommand iniciarConfigDatosLugarCommand;
        public ICommand IniciarConfigDatosLugarCommand
        {
            get
            {
                if (iniciarConfigDatosLugarCommand == null)
                {
                    iniciarConfigDatosLugarCommand = new RelayCommand(
                        param => this.IniciarConfigDatosLugar()
                    );
                }
                return iniciarConfigDatosLugarCommand;
            }
        }

        private void IniciarConfigDatosLugar()
        {
            this.CurrentWindow.Close();
            ConfigDatosLugar viewGra = new ConfigDatosLugar(this.Home, SelectedAudio, SelectedVideo);
            viewGra.ShowDialog();
        }


    }
}
