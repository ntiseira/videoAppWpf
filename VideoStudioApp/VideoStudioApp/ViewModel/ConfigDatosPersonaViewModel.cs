using Microsoft.Expression.Encoder.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VideoStudioApp.Command;
using VideoStudioApp.Model;
using VideoStudioApp.Views;

namespace VideoStudioApp.ViewModel
{
    public class ConfigDatosPersonaViewModel:ViewModelBase
    {
        
        public Window Home { get; set; }
       public Window CurrentWindow { get; set; }

      
        public EncoderDevice SelectedVideo { get; set; }
        public EncoderDevice SelectedAudio { get; set; }
        public Grabacion SelectedGrabacion { get; set; }
        

        public ConfigDatosPersonaViewModel(Window home, Window current, EncoderDevice selectedAudio,
            EncoderDevice selectedVideo, Grabacion grabacion)
        {
            Home = home;
            SelectedGrabacion = grabacion;
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
            if (SelectedGrabacion.Edad == null || SelectedGrabacion.Nombre == null)
            {
                System.Windows.MessageBox.Show("Debe ingresar nombre y la edad");       
            }
            else
            {
                DateTime today = DateTime.Today;
                int age = today.Year - Convert.ToInt32(SelectedGrabacion.Edad);
                today.AddYears(-age);
                SelectedGrabacion.Edad = today.ToString();
                                     
                GrabacionVideo viewGra = new GrabacionVideo(this.Home, SelectedAudio, SelectedVideo, SelectedGrabacion);
                viewGra.ShowDialog();


                this.CurrentWindow.Close();
                this.Dispose();
            }

            
        }



    }
}
