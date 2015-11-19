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

namespace VideoStudioApp.ViewModel
{
    public class ConfigDatosPersonaViewModel:ViewModelBase
    {
        
        public Window Home { get; set; }
       public Window CurrentWindow { get; set; }

      
        public EncoderDevice SelectedVideo { get; set; }
        public EncoderDevice SelectedAudio { get; set; }


        public ConfigDatosPersonaViewModel(Window home, Window current, EncoderDevice selectedAudio, EncoderDevice selectedVideo)
        {
            Home = home;
            CurrentWindow = current;
            SelectedAudio = selectedAudio;
            SelectedVideo = selectedVideo;
        }



        private ICommand iniciarGrabacionCommand;
        public ICommand IniciarGrabacionCommand
        {
            get
            {
                if (iniciarGrabacionCommand == null)
                {
                    iniciarGrabacionCommand = new RelayCommand(
                        param => this.IniciarGrabacion()
                    );
                }
                return iniciarGrabacionCommand;
            }
        }

        private void IniciarGrabacion()
        {
            this.CurrentWindow.Close();
            GrabacionVideo viewGra = new GrabacionVideo(this.Home, SelectedAudio, SelectedVideo);
            viewGra.ShowDialog();
        }



    }
}
