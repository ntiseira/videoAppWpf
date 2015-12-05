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
       public Window CurrentWindow { get; set; }

      
        public EncoderDevice SelectedVideo { get; set; }
        public EncoderDevice SelectedAudio { get; set; }

        public Webcam WebcamCtrl {get; set;}


        public PreviewVideoViewModel(Window current, EncoderDevice selectedAudio, EncoderDevice selectedVideo, Webcam cam)
        {
          
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
                
                WebcamCtrl.WfVisibilityImg = Visibility.Hidden;
                
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


        private ICommand cancelarCommand;
        public ICommand CancelarCommand
        {
            get
            {
                if (cancelarCommand == null)
                {
                    cancelarCommand = new RelayCommand(
                        param => this.Cancelar()
                    );
                }
                return cancelarCommand;
            }
        }

        public void Cancelar()
        {
            MainWindow main = new MainWindow();
            main.ShowDialog();
            CurrentWindow.Close();            
           // this.Dispose();
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
           
            ConfigDatosLugar viewGra = new ConfigDatosLugar(this.CurrentWindow, SelectedAudio, SelectedVideo);
            viewGra.ShowDialog();
            CurrentWindow.Close();
        //    this.Dispose();
           
        }


    }
}
