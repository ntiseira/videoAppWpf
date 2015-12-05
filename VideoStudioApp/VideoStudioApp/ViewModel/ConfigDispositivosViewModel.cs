using Microsoft.Expression.Encoder.Devices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VideoStudioApp.Command;
using VideoStudioApp.Views;

namespace VideoStudioApp.ViewModel
{
    public class ConfigDispositivosViewModel:ViewModelBase
    {

         public Window Home { get; set; }
       public Window CurrentWindow { get; set; }

       public ConfigDispositivosViewModel( Window window)
        {
            CurrentWindow = window;           
            CargarDispositivos();
        }

       public List<EncoderDevice> AudioDevices { get; private set; }
       public List<EncoderDevice> VideoDevices { get; private set; }

       public EncoderDevice SelectedAudioDevice { get; set; }
       public EncoderDevice SelectedVideoDevice { get; set; }

        public void CargarDispositivos()
        {
            VideoDevices = EncoderDevices.FindDevices(EncoderDeviceType.Video).ToList();
             AudioDevices = EncoderDevices.FindDevices(EncoderDeviceType.Audio).ToList();

        }


        private ICommand iniciarPreviewCommand;
        public ICommand IniciarPreviewCommand
        {
            get
            {
                if (iniciarPreviewCommand == null)
                {
                    iniciarPreviewCommand = new RelayCommand(
                        param => this.IniciarPreview()
                    );
                }
                return iniciarPreviewCommand;
            }
        }

        private void IniciarPreview()
        {          
            PreviewVideo viewGra = new PreviewVideo(SelectedAudioDevice, SelectedVideoDevice);
            viewGra.ShowDialog();

            this.CurrentWindow.Close();
            //this.Dispose();
           
        }

    }
}
